using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;

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
            SqlConnection connection = new(connectionString);
            return connection;
        }

        public Bestuurder GeefBestuurder(int id)
        {
            Bestuurder bestuurderOutput = null;
            SqlDataReader reader;
            string query = "SELECT * FROM bestuurder WHERE ID = @id;";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@id"].Value = id;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bestuurderOutput = (Bestuurder)reader.GetValue(0);
                    }
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
            if (bestuurderOutput != null)
            {
                return bestuurderOutput;
            }
            else
            {
                throw new BestuurderRepositoryADOException("Bestuurder niet gevonden");
            }
        }

        public List<Bestuurder> GeefAlleBestuurders()
        {
            List<Bestuurder> bestuurders = new();
            SqlDataReader reader;
            string query = "SELECT * FROM bestuurder";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.CommandText = query;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bestuurders.Add((Bestuurder)reader.GetValue(0));
                    }
                }
                catch(Exception ex)
                {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - GeefAlleBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return bestuurders;
        }

        public IEnumerable<Bestuurder> GeefBestuurders(string naam, string voornaam, Adres adres, DateTime datum, string rijksregister, List<TypeRijbewijs> types, Voertuig v, TankKaart t)
        {
            throw new NotImplementedException();
        }

        public bool HeeftBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }

        public void VerwijderBestuurder(Bestuurder bestuurder)
        {
            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            string query = "INSERT INTO bestuurder(ID, voornaam, naam, geboortedatum, adresID, rijksregister, rijbewijslijstID, voertuigID, tankkaartnummer)OUTPUT INSERTED.ID VALUES (@voornaam, @naam, @datum, @adres, @rijksregister, @types, @v, @t);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@adres", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@types", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@v", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@t", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@voornaam"].Value = bestuurder.VoorNaam;
                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.Parameters["@datum"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@adres"].Value = bestuurder.Adres.ID;
                    command.Parameters["@rijksregister"].Value = bestuurder.RijksRegisterNr;
                    command.Parameters["@rijbewijs"].Value = bestuurder.ID;
                    command.Parameters["@v"].Value = bestuurder.Voertuig.ID;
                    command.Parameters["@t"].Value = bestuurder.TankKaart.KaartNr;
                    int ID = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new("BestuurderRepositoryADO - GeefBestuurders: Er liep iets mis -> ", ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        public void WijzigBestuurder(Bestuurder bestuurder)
        {
            string query = "UPDATE bestuurder SET voornaam = @voornaam," +
                "naam = @naam, geboortedatum = @geboorte, adresID = @adresID," +
                "rijksregister = @rijksregister, rijbewijstlijstID = @typeID," +
                "voertuigID = @voertuigID, tankkaartnummer = @tankkaartNr" +
                "WHERE ID = @id";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@adresID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@geboorte", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@rijksregister", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@typeID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@voertuigID", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@tankkaartNr", SqlDbType.Int));

                    command.CommandText = query;

                    command.Parameters["@id"].Value = bestuurder.ID;
                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.Parameters["@voornaam"].Value = bestuurder.VoorNaam;
                    command.Parameters["@adresID"].Value = bestuurder.Adres.ID;
                    command.Parameters["@geboorte"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@rijksregister"].Value = bestuurder.RijksRegisterNr;
                    command.Parameters["@typeID"].Value = bestuurder.ID;
                    command.Parameters["@voertuigID"].Value = bestuurder.Voertuig.ID;
                    command.Parameters["@tankkaartNr"].Value = bestuurder.TankKaart.KaartNr;

                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new BestuurderRepositoryADOException("BestuurderRepositoryADO - WijzigBestuurder: Er liep iets mis -> ", ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
