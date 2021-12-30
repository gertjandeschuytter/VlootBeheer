using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using BusinessLayer.Utilities;
using FleetDatabase.FleetDatabaseExceptions;
using Newtonsoft.Json;

namespace FleetDatabase {
    public class BestuurderRepositoryADO : IBestuurderRepository {
        private readonly string connectionString;

        public BestuurderRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new(connectionString);
            return connection;
        }
        public Bestuurder GeefBestuurder(int BestuurderId)
        {
            List<TypeRijbewijs> RijbewijzenLijst = new();
            Bestuurder bestuurder = null;
            Voertuig voertuig = null;
            TankKaart tankkaart = null;
            string query = "SELECT bs.*, vt.Merk, vt.Model, vt.Chassisnummer, vt.Nummerplaat, vt.Brandstoftype, vt.Wagentype," +
                " vt.Kleur, vt.Aantaldeuren, tk.Kaartnummer, tk.Geldigheidsdatum, tk.Pincode, tk.Isgeblokeerd, tk.Brandstoftype, a.Gemeente, a.Straat, a.Huisnummer, a.Postcode" +
                " FROM bestuurder bs " +
                "LEFT JOIN Voertuig vt ON vt.VoertuigId = bs.VoertuigId " +
                "LEFT JOIN Tankkaart tk ON tk.TankkaartId = bs.TankkaartId " +
                "LEFT JOIN Adres a ON a.AdresId = bs.AdresId " +
                "WHERE bs.BestuurderId = @BestuurderId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@BestuurderId"].Value = BestuurderId;
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader["BestuurderId"].GetType() != typeof(DBNull))
                    {
                        int bestuurderId = (int)reader["BestuurderId"];
                        if (!BestuurderHeeftEenOfMeerdereRijbewijzen(BestuurderId))
                        {
                            bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], RijbewijzenLijst);
                        }
                        else
                        {
                            bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], GeefTypeRijbewijzen(bestuurderId));
                        }
                        bestuurder.ZetID(BestuurderId);
                    }
                    if (reader["AdresId"].GetType() != typeof(DBNull))
                    {
                        Adres adres = new Adres((int)reader["AdresId"], (string)reader["Straat"], (string)reader["Gemeente"], (int)reader["Postcode"], (string)reader["Huisnummer"]);
                        bestuurder.ZetAdres(adres);
                    }
                    if (reader["VoertuigId"].GetType() != typeof(DBNull))
                    {
                        Brandstoftype_voertuig brandstofType = (Brandstoftype_voertuig)Enum.Parse(typeof(Brandstoftype_voertuig), (string)reader["Brandstoftype"]);
                        Typewagen wagenType = (Typewagen)Enum.Parse(typeof(Typewagen), (string)reader["WagenType"]);
                        if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] == DBNull.Value)
                        {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, null);
                        }
                        else if (reader["Kleur"] != DBNull.Value && reader["Aantaldeuren"] == DBNull.Value)
                        {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], null);
                        }
                        else if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] != DBNull.Value)
                        {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, (int)reader["Aantaldeuren"]);
                        }
                        else
                        {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], (int)reader["Aantaldeuren"]);
                        }
                        voertuig.ZetId((int)reader["VoertuigId"]);
                        bestuurder.ZetVoertuig(voertuig);
                    }
                    if (reader["TankkaartId"].GetType() != typeof(DBNull))
                    {
                        int tankkaartIdDB = (int)reader["TankkaartId"];
                        tankkaart = new TankKaart((string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], bestuurder, (bool)reader["Isgeblokeerd"], null);
                        tankkaart.ZetTankkaartId(tankkaartIdDB);
                        if (bestuurder.TankKaart == null)
                        {
                            bestuurder.ZetTankKaart(tankkaart);
                        }
                    }
                    return bestuurder;
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
        private List<TypeRijbewijs> GeefTypeRijbewijzen(int bestuurderId)
        {
            List<TypeRijbewijs> TypesInBezit = new();
            string sql = "SELECT br.BestuurderId, br.TypeRijbewijs FROM BestuurderRijbewijs br WHERE BestuurderId = @BestuurderId";
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    cmd.Parameters["@BestuurderId"].Value = bestuurderId;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (bestuurderId == (int)reader["BestuurderId"])
                        {
                            TypeRijbewijs type = (TypeRijbewijs)Enum.Parse(typeof(TypeRijbewijs), (string)reader["TypeRijbewijs"]);
                            TypesInBezit.Add(type);
                        }
                    }
                    return TypesInBezit;
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("GeefTypeRijbewijzen - error", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool BestaatBestuurder(int BestuurderId)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].Bestuurder WHERE BestuurderId=@BestuurderId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
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
        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            int BestuurderId;
            SqlTransaction trans = null;
            string query = "INSERT INTO bestuurder(Voornaam, Naam, Geboortedatum, AdresId, Rijksregisternummer, VoertuigId, TankkaartId)" +
                "OUTPUT INSERTED.BestuurderId VALUES (@Voornaam, @Naam, @Geboortedatum, @AdresId, @Rijksregisternummer, @VoertuigId, @TankkaartId);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand())
            {
                connection.Open();
                try
                {
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
                    command.Parameters["@Voornaam"].Value = bestuurder.Voornaam;
                    command.Parameters["@Naam"].Value = bestuurder.Naam;
                    command.Parameters["@Geboortedatum"].Value = bestuurder.Geboortedatum;
                    if (bestuurder.Adres == null)
                    {
                        command.Parameters["@AdresId"].Value = DBNull.Value;
                    }
                    else
                    {
                        VoegAdresToe(bestuurder.Adres);
                        command.Parameters["@AdresId"].Value = bestuurder.Adres.ID;
                    }
                    command.Parameters["@Rijksregisternummer"].Value = bestuurder.Rijksregisternummer;
                    if (bestuurder.Voertuig == null)
                    {
                        command.Parameters["@VoertuigId"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@VoertuigId"].Value = bestuurder.Voertuig.ID;
                    }
                    if (bestuurder.TankKaart == null)
                    {
                        command.Parameters["@TankkaartId"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@TankkaartId"].Value = bestuurder.TankKaart.TankkaartId;
                    }
                    BestuurderId = (int)command.ExecuteScalar();
                    bestuurder.ZetID(BestuurderId);
                    foreach (var item in bestuurder._Types)
                    {
                        command2.Parameters.Clear();
                        command2.Transaction = trans;
                        string sql2 = "INSERT INTO [dbo].BestuurderRijbewijs (BestuurderId, TypeRijbewijs) VALUES (@BestuurderId, @TypeRijbewijs)";
                        command2.CommandText = sql2;
                        command2.Parameters.AddWithValue("@BestuurderId", BestuurderId);
                        command2.Parameters.AddWithValue("@TypeRijbewijs", item.ToString());
                        command2.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public IEnumerable<Bestuurder> GeefBestuurders(string? voornaam, string? naam, string? rijksregister, DateTime? geboortedatum)
        {
            List<TypeRijbewijs> RijbewijzenLijst = new();
            List<Bestuurder> bestuurders = new();
            Bestuurder bestuurder = null;
            Voertuig voertuig = null;
            bool WHERE = true;
            bool AND = false;
            string sql = "SELECT bs.*, vt.Merk, vt.Model, vt.Chassisnummer, vt.Nummerplaat, vt.Brandstoftype, vt.Wagentype," +
                "vt.Kleur, vt.Aantaldeuren, tk.Kaartnummer, tk.Geldigheidsdatum, tk.Pincode, tk.Isgeblokeerd, tk.Brandstoftype, a.Gemeente, a.Straat, a.Huisnummer, a.Postcode" +
                " FROM bestuurder bs" +
                " LEFT JOIN Voertuig vt ON vt.VoertuigId = bs.VoertuigId" +
                " LEFT JOIN Tankkaart tk ON tk.TankkaartId = bs.TankkaartId" +
                " LEFT JOIN Adres a ON a.AdresId = bs.AdresId";
            if (!string.IsNullOrEmpty(voornaam))
            {
                if (WHERE)
                {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND)
                {
                    sql += " AND ";
                }
                else
                {
                    AND = true;
                }
                sql += "bs.voornaam = @voornaam";
            }
            if (!string.IsNullOrEmpty(naam))
            {
                if (WHERE)
                {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND)
                {
                    sql += " AND ";
                }
                else
                {
                    AND = true;
                }
                sql += "bs.naam = @naam";
            }
            if (!string.IsNullOrEmpty(rijksregister))
            {
                if (WHERE)
                {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND)
                {
                    sql += " AND ";
                }
                else
                {
                    AND = false;
                }
                sql += "bs.rijksregisternummer = @rijksregisternummer";
            }
            if (geboortedatum.HasValue)
            {
                if (WHERE)
                {
                    sql += " WHERE ";
                }
                if (AND)
                {
                    sql += " AND ";
                }
                sql += "bs.geboortedatum=@geboortedatum";
            }
            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                try
                {
                    if (!string.IsNullOrEmpty(voornaam))
                    {
                        cmd.Parameters.AddWithValue("@voornaam", voornaam);
                    }
                    if (!string.IsNullOrEmpty(naam))
                    {
                        cmd.Parameters.AddWithValue("@naam", naam);
                    }
                    if (!string.IsNullOrEmpty(rijksregister))
                    {
                        cmd.Parameters.AddWithValue("@rijksregisternummer", rijksregister);
                    }
                    if (geboortedatum != null)
                    {
                        cmd.Parameters.AddWithValue("@geboortedatum", geboortedatum);
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["BestuurderId"].GetType() != typeof(DBNull))
                        {
                            var datum = (DateTime)reader["Geboortedatum"];
                            var verkorteDatum = datum.Date;
                            int bestuurderId = (int)reader["BestuurderId"];
                            if (!BestuurderHeeftEenOfMeerdereRijbewijzen(bestuurderId))
                            {
                                bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], verkorteDatum, (string)reader["Rijksregisternummer"], RijbewijzenLijst);
                            }
                            else
                            {
                                bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], GeefTypeRijbewijzen(bestuurderId));
                            }
                            bestuurder.ZetID(bestuurderId);
                        }
                        if (reader["AdresId"].GetType() != typeof(DBNull))
                        {
                            Adres adres = new Adres((int)reader["AdresId"], (string)reader["Straat"], (string)reader["Gemeente"], (int)reader["Postcode"], (string)reader["Huisnummer"]);
                            bestuurder.ZetAdres(adres);
                        }
                        if (reader["VoertuigId"].GetType() != typeof(DBNull))
                        {
                            Brandstoftype_voertuig brandstofType = (Brandstoftype_voertuig)Enum.Parse(typeof(Brandstoftype_voertuig), (string)reader["Brandstoftype"]);
                            Typewagen wagenType = (Typewagen)Enum.Parse(typeof(Typewagen), (string)reader["WagenType"]);
                            if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] == DBNull.Value)
                            {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, null);
                            }
                            else if (reader["Kleur"] != DBNull.Value && reader["Aantaldeuren"] == DBNull.Value)
                            {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], null);
                            }
                            else if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] != DBNull.Value)
                            {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, (int)reader["Aantaldeuren"]);
                            }
                            else
                            {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], (int)reader["Aantaldeuren"]);
                            }
                            voertuig.ZetId((int)reader["VoertuigId"]);
                            bestuurder.ZetVoertuig(voertuig);
                        }
                        if ((reader["TankkaartId"].GetType() != typeof(DBNull)))
                        {
                            int tankkaartIdDB = (int)reader["TankkaartId"];
                            TankKaart tankKaart = new TankKaart((string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], bestuurder, (bool)reader["Isgeblokeerd"], null);
                            tankKaart.ZetTankkaartId(tankkaartIdDB);
                            bestuurder.ZetTankKaart(tankKaart);
                        }
                        bestuurders.Add(bestuurder);
                    }
                    return bestuurders;
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("BestellingWeergeven - " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private bool BestuurderHeeftEenOfMeerdereRijbewijzen(int bestuurderId)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].BestuurderRijbewijs WHERE bestuurderId=@bestuurderId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@bestuurderId", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@bestuurderId"].Value = bestuurderId;
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
        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            Bestuurder bestuurderDB = GeefBestuurder(bestuurder.BestuurderId);
            string querydeleteAdres = "DELETE t1 FROM Fleet.[dbo].Adres t1 JOIN Fleet.[dbo].Bestuurder t2 ON t1.AdresId = t2.AdresId WHERE t2.BestuurderId=@BestuurderId";
            string querydeleteVoertuig = "DELETE t1 FROM Fleet.[dbo].Voertuig t1 JOIN Fleet.[dbo].Bestuurder t2 ON t1.VoertuigId = t2.VoertuigId WHERE t2.BestuurderId=@BestuurderId";
            string querydeleteTankkaart = "DELETE t1 FROM Fleet.[dbo].Tankkaart t1 JOIN Fleet.[dbo].Bestuurder t2 ON t1.TankkaartId = t2.TankkaartId WHERE t2.BestuurderId=@BestuurderId";
            string querydeleteBestuurder = "DELETE FROM Bestuurder WHERE BestuurderId=@BestuurderId";
            string querydeleteTypes = "DELETE FROM Fleet.[dbo].BestuurderRijbewijs WHERE BestuurderId=@BestuurderId";
            SqlConnection connection = GetConnection();
            if (bestuurderDB.Adres != null)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                        command.CommandText = querydeleteAdres;
                        command.Parameters["@BestuurderId"].Value = bestuurder.BestuurderId;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestuurderRepositoryADOException("Adres kon niet verwijderd worden" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            if (bestuurderDB.Voertuig != null)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                        command.CommandText = querydeleteVoertuig;
                        command.Parameters["@BestuurderId"].Value = bestuurder.BestuurderId;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestuurderRepositoryADOException("Voertuig kon niet verwijderd worden" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            if (bestuurderDB.TankKaart != null)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                        command.CommandText = querydeleteTankkaart;
                        command.Parameters["@BestuurderId"].Value = bestuurder.BestuurderId;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestuurderRepositoryADOException("Tankkaart kon niet verwijderd worden" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            if (bestuurderDB._Types.Count > 0)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                        command.CommandText = querydeleteTypes;
                        command.Parameters["@BestuurderId"].Value = bestuurder.BestuurderId;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestuurderRepositoryADOException("Tankkaart kon niet verwijderd worden" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            if (bestuurderDB != null)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                        command.CommandText = querydeleteBestuurder;
                        command.Parameters["@BestuurderId"].Value = bestuurder.BestuurderId;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new BestuurderRepositoryADOException("Bestuurder kon niet verwijderd worden want bestuurder bestaat niet" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void WijzigBestuurder(Bestuurder nieuweBestuurder)
        {
            Bestuurder bestuurderdb = GeefBestuurder(nieuweBestuurder.BestuurderId);
            SqlTransaction trans = null;
            string sql1 = "UPDATE [dbo].Bestuurder SET Voornaam = @Voornaam," +
                "Naam = @Naam, Geboortedatum = @Geboortedatum," +
                "Rijksregisternummer = @Rijksregisternummer " +
                "WHERE BestuurderId = @BestuurderId";
            string sql2 = "INSERT INTO [dbo].BestuurderRijbewijs (BestuurderId, TypeRijbewijs) VALUES (@BestuurderId, @TypeRijbewijs)";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    trans = connection.BeginTransaction();
                    command.Transaction = trans;
                    command2.Transaction = trans;
                    command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Geboortedatum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@Rijksregisternummer", SqlDbType.NVarChar));
                    command.CommandText = sql1;
                    command.Parameters["@BestuurderId"].Value = nieuweBestuurder.BestuurderId;
                    command.Parameters["@Naam"].Value = nieuweBestuurder.Naam;
                    command.Parameters["@Voornaam"].Value = nieuweBestuurder.Voornaam;
                    command.Parameters["@Geboortedatum"].Value = nieuweBestuurder.Geboortedatum;
                    command.Parameters["@Rijksregisternummer"].Value = nieuweBestuurder.Rijksregisternummer;
                    command.ExecuteNonQuery();
                    List<TypeRijbewijs> lijst = GeefTypeRijbewijzen(nieuweBestuurder.BestuurderId);
                    foreach (var item in bestuurderdb._Types)
                    {
                        if (!lijst.Contains(item))
                        {
                            command2.CommandText = sql2;
                            command2.Parameters.AddWithValue("@BestuurderId", nieuweBestuurder.BestuurderId);
                            command2.Parameters.AddWithValue("@TypeRijbewijs", item.ToString());
                            command2.ExecuteNonQuery();
                        }
                    }
                    //UPDATE ADRES
                    if (bestuurderdb.Adres != nieuweBestuurder.Adres && nieuweBestuurder.Adres != null)
                    {
                        UpdateAdresBestuurder(nieuweBestuurder);
                    }
                    if (bestuurderdb.Voertuig != nieuweBestuurder.Voertuig && nieuweBestuurder.Voertuig != null)
                    {
                        UpdateVoertuigBestuurder(nieuweBestuurder);
                    }
                    if (bestuurderdb.TankKaart != nieuweBestuurder.TankKaart && nieuweBestuurder.TankKaart != null)
                    {
                        UpdateTankkaartBestuurder(nieuweBestuurder);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - WijzigBestuurder: Er liep iets mis -> ", ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
        private void VoegAdresToe(Adres adres)
        {
            int adresId;
            string query = "INSERT INTO adres(Straat, Huisnummer, Postcode, Gemeente)" +
                "OUTPUT INSERTED.AdresId VALUES (@Straat, @Huisnummer, @Postcode, @Gemeente);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@Straat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Huisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Postcode", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Gemeente", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@Straat"].Value = adres.Straat;
                    command.Parameters["@Huisnummer"].Value = adres.Nummer;
                    command.Parameters["@Postcode"].Value = adres.Postcode;
                    command.Parameters["@Gemeente"].Value = adres.Stad;
                    adresId = (int)command.ExecuteScalar();
                    adres.ZetAdresId(adresId);
                }
                catch (Exception ex)
                {
                    throw new("BestuurderRepositoryADO - VoegAdresToe: Er liep iets mis -> ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void UpdateVoertuigBestuurder(Bestuurder nieuwebestuurder)
        {
            string sqlUpdate = "UPDATE Voertuig SET Merk=@Merk,Model=@Model,Chassisnummer=@Chassisnummer,Nummerplaat=@Nummerplaat" +
                ",Brandstoftype=@Brandstoftype,Wagentype=@Wagentype,Kleur=@Kleur,Aantaldeuren=@Aantaldeuren WHERE VoertuigId=@VoertuigId";
            SqlConnection connection = GetConnection();
            //Update
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Merk", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Model", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Chassisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Nummerplaat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Brandstoftype", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Wagentype", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Kleur", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Aantaldeuren", SqlDbType.Int));
                    command.CommandText = sqlUpdate;
                    command.Parameters["@VoertuigId"].Value = nieuwebestuurder.Voertuig.ID;
                    command.Parameters["@Merk"].Value = nieuwebestuurder.Voertuig.Merk;
                    command.Parameters["@Model"].Value = nieuwebestuurder.Voertuig.Model;
                    command.Parameters["@Chassisnummer"].Value = nieuwebestuurder.Voertuig.ChassisNummer;
                    command.Parameters["@Nummerplaat"].Value = nieuwebestuurder.Voertuig.NummerPlaat;
                    command.Parameters["@Brandstoftype"].Value = nieuwebestuurder.Voertuig.BrandstofType;
                    command.Parameters["@Wagentype"].Value = nieuwebestuurder.Voertuig.TypeWagen;
                    if (nieuwebestuurder.Voertuig.Kleur == null)
                    {
                        command.Parameters["@Kleur"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@Kleur"].Value = nieuwebestuurder.Voertuig.Kleur;
                    }
                    if (nieuwebestuurder.Voertuig.AantalDeuren == 0)
                    {
                        command.Parameters["@AantalDeuren"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@AantalDeuren"].Value = nieuwebestuurder.Voertuig.AantalDeuren;
                    }
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("WijzigAdresBestuurder - UpdateAdres " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void UpdateAdresBestuurder(Bestuurder nieuweBestuurder)
        {
            string sqlUpdate = "UPDATE Adres SET Straat=@Straat,Huisnummer=@Huisnummer,Gemeente=@Gemeente,Postcode=@Postcode WHERE AdresId=@AdresId";
            SqlConnection connection = GetConnection();
            //Update
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Postcode", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Gemeente", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Huisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Straat", SqlDbType.NVarChar));
                    command.CommandText = sqlUpdate;
                    command.Parameters["@AdresId"].Value = nieuweBestuurder.Adres.ID;
                    command.Parameters["@Postcode"].Value = nieuweBestuurder.Adres.Postcode;
                    command.Parameters["@Gemeente"].Value = nieuweBestuurder.Adres.Stad;
                    command.Parameters["@Huisnummer"].Value = nieuweBestuurder.Adres.Nummer;
                    command.Parameters["@Straat"].Value = nieuweBestuurder.Adres.Straat;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("WijzigAdresBestuurder - UpdateAdres " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void UpdateTankkaartBestuurder(Bestuurder nieuweBestuurder)
        {
            string sqlUpdate = "UPDATE Tankkaart SET Kaartnummer=@Kaartnummer,Geldigheidsdatum=@Geldigheidsdatum,Pincode=@Pincode,Isgeblokeerd=@Isgeblokeerd, Brandstoftype=@Brandstoftype WHERE TankkaartId=@TankkaartId";
            SqlConnection connection = GetConnection();
            //Update
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@Kaartnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Geldigheidsdatum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@Pincode", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@Isgeblokeerd", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@Brandstoftype", SqlDbType.NVarChar));
                    command.CommandText = sqlUpdate;
                    if (nieuweBestuurder.TankKaart.Pincode == null)
                    {
                        command.Parameters["@Pincode"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@Pincode"].Value = nieuweBestuurder.TankKaart.Pincode;
                    }
                    command.Parameters["@Kaartnummer"].Value = nieuweBestuurder.TankKaart.KaartNr;
                    command.Parameters["@Geldigheidsdatum"].Value = nieuweBestuurder.TankKaart.Geldigheidsdatum;
                    command.Parameters["@Pincode"].Value = nieuweBestuurder.TankKaart.Pincode;
                    //if (nieuweBestuurder.TankKaart.Bestuurder.ID == 0) {
                    //    command.Parameters["@BestuurderId"].Value = DBNull.Value;
                    //} else {
                    //    command.Parameters["@BestuurderId"].Value = nieuweBestuurder.TankKaart.Bestuurder.ID;
                    //}
                    command.Parameters["@Isgeblokeerd"].Value = nieuweBestuurder.TankKaart.Geblokkeerd;
                    if (!nieuweBestuurder.TankKaart.Brandstoftype.HasValue)
                    {
                        command.Parameters["@Brandstoftype"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@Brandstoftype"].Value = nieuweBestuurder.TankKaart.Brandstoftype;
                    }
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestuurderRepositoryADOException("WijzigAdresBestuurder - UpdateAdres " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
