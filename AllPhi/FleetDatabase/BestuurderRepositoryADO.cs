using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;

namespace FleetDatabase {
    public class BestuurderRepositoryADO : IBestuurderRepository {
        private readonly string connectionString;

        public BestuurderRepositoryADO(string connectionString) {
            this.connectionString = connectionString;
        }

        private SqlConnection GetConnection() {
            SqlConnection connection = new(connectionString);
            return connection;
        }

        public Bestuurder GeefBestuurder(int BestuurderId) {
            Bestuurder bestuurder = null;
            bool bestuurderGevonden = false;
            string query = "SELECT bs.*, vt.Merk, vt.Model, vt.Chassisnummer, vt.Nummerplaat, vt.Brandstoftype, vt.Wagentype," +
                " vt.Kleur, vt.Aantaldeuren, tk.Kaartnummer, tk.Geldigheidsdatum, tk.Pincode, tk.Isgeblokeerd, a.Gemeente, a.Straat, a.Huisnummer, a.Postcode" +
                " FROM bestuurder bs " +
                "LEFT JOIN Voertuig vt ON vt.VoertuigId = bs.VoertuigId " +
                "LEFT JOIN Tankkaart tk ON tk.TankkaartId = bs.TankkaartId " +
                "LEFT JOIN Adres a ON a.AdresId = bs.AdresId " +
                "WHERE bs.BestuurderId = @BestuurderId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@BestuurderId"].Value = BestuurderId;
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (bestuurderGevonden == false) {
                        bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], GeefTypeRijbewijzen(BestuurderId));
                        bestuurderGevonden = true;
                    }
                    if (!(reader["AdresId"].GetType() == typeof(DBNull))) {

                    }

                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                } finally {
                    command.Dispose();
                    connection.Close();
                }
            }
            if (bestuurderOutput != null) {
                return bestuurderOutput;
            } else {
                throw new BestuurderRepositoryADOException("Bestuurder niet gevonden");
            }
        }

        private List<TypeRijbewijs> GeefTypeRijbewijzen(int bestuurderId) {
            List<TypeRijbewijs> TypesInBezit = new();
            string sql = "SELECT br.BestuurderId, br.TypeRijbewijs FROM BestuurderRijbewijs br WHERE BestuurderId = @BestuurderId";
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        if (bestuurderId == (int)reader["BestuurderId"]) {
                            TypeRijbewijs type = (TypeRijbewijs)Enum.Parse(typeof(TypeRijbewijs), (string)reader["TypeRijbewijs"]);
                            TypesInBezit.Add(type);
                        }
                    }
                    return TypesInBezit;
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("GeefTypeRijbewijzen - error", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public List<Bestuurder> GeefAlleBestuurders() {
            List<Bestuurder> bestuurders = new();
            SqlDataReader reader;
            string query = "SELECT * FROM bestuurder";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.CommandText = query;
                    reader = command.ExecuteReader();
                    while (reader.Read()) {
                        bestuurders.Add((Bestuurder)reader.GetValue(0));
                    }
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - GeefAlleBestuurders: Er liep iets mis -> ", ex);
                } finally {
                    command.Dispose();
                    connection.Close();
                }
            }
            return bestuurders;
        }

        public IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, Adres adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t) {
            throw new NotImplementedException();
        }

        public bool BestaatBestuurder(Bestuurder bestuurder) {
            throw new NotImplementedException();
        }

        public bool BestaatBestuurder(int BestuurderId)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT * FROM [dbo].Bestuurder WHERE BestuurderId=@BestuurderId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@BestuurderId"].Value = BestuurderId;
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                }
                catch (Exception ex)
                {
                    throw new VoertuigRepositoryADOExceptions("bestaatVoertuig", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderBestuurder(Bestuurder bestuurder) {
            string query = "REMOVE FROM bestuurder WHERE ID = @id";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@id"].Value = bestuurder.ID;
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO: VerwijderBestuurder - Er liep iets mis ->", ex);
                } finally {
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        public void VoegBestuurderToe(Bestuurder bestuurder) {
            int BestuurderId;
            SqlTransaction trans = null;
            string query = "INSERT INTO bestuurder(Voornaam, Naam, Geboortedatum, AdresId, Rijksregisternummer, VoertuigId, TankkaartId)" +
                "OUTPUT INSERTED.BestuurderId VALUES (@Voornaam, @Naam, @Geboortedatum, @AdresId, @Rijksregisternummer, @VoertuigId, @TankkaartId);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand()) {
                connection.Open();
                try {
                    trans = connection.BeginTransaction();
                    command.Transaction = trans;
                    command.Parameters.Add(new SqlParameter("@Voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Geboortedatum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Rijksregisternummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@Voornaam"].Value = bestuurder.VoorNaam;
                    command.Parameters["@Naam"].Value = bestuurder.Naam;
                    command.Parameters["@Geboortedatum"].Value = bestuurder.GeboorteDatum;
                    if (bestuurder.Adres == null) {
                        command.Parameters["@AdresId"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@AdresId"].Value = bestuurder.Adres.ID;
                    }
                    command.Parameters["@Rijksregisternummer"].Value = bestuurder.RijksRegisterNr;
                    if (bestuurder.Voertuig == null) {
                        command.Parameters["@VoertuigId"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@VoertuigId"].Value = bestuurder.Voertuig.ID;
                    }
                    if (bestuurder.TankKaart == null) {
                        command.Parameters["@TankkaartId"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@TankkaartId"].Value = bestuurder.TankKaart.KaartNr;
                    }
                    BestuurderId = (int)command.ExecuteScalar();
                    bestuurder.ZetID(BestuurderId);
                    foreach (var item in bestuurder._Types) {
                        string sql2 = "INSERT INTO [dbo].BestuurderRijbewijs (BestuurderId, TypeRijbewijs) VALUES (@BestuurderId, @TypeRijbewijs)";
                        command2.Transaction = trans;
                        command2.CommandText = sql2;
                        command2.Parameters.AddWithValue("@BestuurderId", BestuurderId);
                        command2.Parameters.AddWithValue("@TypeRijbewijs", item.ToString());
                        command2.ExecuteNonQuery();
                    }
                    trans.Commit();
                } catch (Exception ex) {
                    trans.Rollback();
                    throw new("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public void WijzigBestuurder(Bestuurder bestuurder) {
            string query = "UPDATE [dbo].Bestuurder SET Voornaam = @Voornaam," +
                "Naam = @Naam, Geboortedatum = @Geboortedatum, AdresId = @AdresId," +
                "Rijksregisternummer = @Rijksregisternummer, " +
                "VoertuigId = @VoertuigId, TankkaartId = @TankkaartId" +
                "WHERE BestuurderId = @BestuurderId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Geboortedatum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@Rijksregisternummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));

                    command.CommandText = query;

                    command.Parameters["@BestuurderId"].Value = bestuurder.ID;
                    command.Parameters["@Naam"].Value = bestuurder.Naam;
                    command.Parameters["@Voornaam"].Value = bestuurder.VoorNaam;
                    command.Parameters["@AdresId"].Value = bestuurder.Adres.ID;
                    command.Parameters["@Geboortedatum"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@Rijksregisternummer"].Value = bestuurder.RijksRegisterNr;
                    command.Parameters["@VoertuigId"].Value = bestuurder.Voertuig.ID;
                    command.Parameters["@TankkaartId"].Value = bestuurder.TankKaart.KaartNr;

                    command.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - WijzigBestuurder: Er liep iets mis -> ", ex);
                } finally {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
