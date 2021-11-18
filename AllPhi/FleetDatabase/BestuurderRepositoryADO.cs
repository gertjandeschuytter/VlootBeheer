using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;

namespace FleetDatabase
{
    public class BestuurderRepositoryADO : IBestuurderRepository
    {
        private readonly string connectionString;

        public BestuurderRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public Bestuurder GeefBestuurder(int id)
        {
            string query = "SELECT * FROM bestuurder WHERE ID = @id;";

            throw new NotImplementedException();
        }

        public IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, Adres adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t)
        {
            List<Bestuurder> bestuurders = new();
            string query = "SELECT * FROM dbo.bestuurder"
                + "WHERE voornaam = @voornaam"
                + "AND naam = @naam"
                + "AND geboortedatum = @datum"
                + "AND rijksregister = @rijksregister"
                + "AND Voertuig = @v"
                + "AND tankkaart = @t"
                + "IER MOET NEN JOIN;";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@types", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@v", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@t", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@voornaam"].Value = voornaam;
                    command.Parameters["@naam"].Value = naam;
                    command.Parameters["@datum"].Value = datum;
                    command.Parameters["@adres"].Value = adres;
                    command.Parameters["@rijksregister"].Value = rijksregister;
                    command.Parameters["@rijbewijs"].Value = types;
                    command.Parameters["@v"].Value = v.ID;
                    command.Parameters["@t"].Value = t.KaartNr;

                    Adres adres_null = null;
                    Voertuig v_null = null;
                    TankKaart t_null = null;

                    Adres adresdb;
                    Voertuig voertuigdb;
                    TankKaart tankkaartdb;

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        string voornaamdb = (string)sqlDataReader["voornaam"];
                        string naamdb = (string)sqlDataReader["naam"];
                        DateTime datumdb = (DateTime)sqlDataReader["geboortedatum"];
                        if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("adres")))
                        {
                            adresdb = new((string)sqlDataReader["straat"], (string)sqlDataReader["stad"], (string)sqlDataReader["postcode"], (int)sqlDataReader["nummer"]);
                        }
                        else
                        {
                            adresdb = adres_null;
                        }
                        string rijksregisterdb = (string)sqlDataReader["rijksregister"];
                        if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("voertuig")))
                        {
                            voertuigdb = new();
                        }
                        else
                        {
                            voertuigdb = v_null;
                        }
                        if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("voertuig")))
                        {
                            tankkaartdb = new();
                        }
                        else
                        {
                            tankkaartdb = t_null;
                        }

                        Bestuurder b = new Bestuurder(naamdb, voornaamdb, adresdb, datumdb, rijksregisterdb, voertuigdb, tankkaartdb);
                        bestuurders.Add(b);
                    }

                    return bestuurders;
                }
                catch (Exception ex)
                {
                    throw new("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool HeeftBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            string query = "INSERT INTO bestuurder(ID, voornaam, naam, geboortedatum, adres, rijksregister, rijbewijs, Voertuig, tankkaart) VALUES (@id, @voornaam, @naam, @datum, @adres, @rijksregister, @types, @v, @t);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@types", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@v", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@t", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@voornaam"].Value = bestuurder.VoorNaam;
                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.Parameters["@datum"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@adres"].Value = bestuurder.Adres;
                    command.Parameters["@rijksregister"].Value = bestuurder.RijksRegisterNr;
                    command.Parameters["@rijbewijs"].Value = bestuurder.Types;
                    command.Parameters["@v"].Value = bestuurder.Voertuig;
                    command.Parameters["@t"].Value = bestuurder.TankKaart;

                }
                catch (Exception ex)
                {
                    throw new("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            string query = "";

            throw new NotImplementedException();
        }
    }
}
