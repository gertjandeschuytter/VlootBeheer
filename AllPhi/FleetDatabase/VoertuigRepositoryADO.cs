using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FleetDatabase {
    public class VoertuigRepositoryADO : IVoertuigRepository {
        //fields
        private readonly string connectionString;
        //ctor
        public VoertuigRepositoryADO(string connectionString) {
            this.connectionString = connectionString;
        }
        private SqlConnection GetConnection() {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        //done
        public bool BestaatVoertuig(string Id) {
            SqlConnection conn = GetConnection();
            string query = "SELECT (*) FROM [dbo].voertuig WHERE Id=@Id";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@Id"].Value = Id;
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;else return false;
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("bestaatVoertuig", ex);
                } finally {
                    conn.Close();
                }
            }
        }
        public bool BestaatVoertuig(Voertuig voertuig) {
            SqlConnection connection = GetConnection();
            string query =
                "SELECT Count (*) FROM [dbo].voertuig"
                + "WHERE merk=@merk"
                + " AND model=@model"
                + " AND chassisNummer=@chassisNummer"
                + " AND nummerplaat=@nummerplaat"
                + " AND brandstofType=@brandstofType"
                + " AND typeWagen=@typeWagen"
                + " AND aantalDeuren=@aantalDeuren"
                + " AND kleur=@kleur";
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@chassisNummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@brandstofType", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@typeWagen", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@aantalDeuren", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@merk"].Value = voertuig.Merk;
                    command.Parameters["@model"].Value = voertuig.Model;
                    command.Parameters["@chassisNummer"].Value = voertuig.ChassisNummer;
                    command.Parameters["@nummerplaat"].Value = voertuig.NummerPlaat;
                    command.Parameters["@brandstofType"].Value = Enum.GetName(typeof(Brandstoftype_voertuig), voertuig.BrandstofType);
                    command.Parameters["@typeWagen"].Value = Enum.GetName(typeof(Typewagen), voertuig.TypeWagen);
                    command.Parameters["@aantalDeuren"].Value = voertuig.AantalDeuren;
                    command.Parameters["@kleur"].Value = voertuig.Kleur;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("bestaatVoertuig", ex);
                } finally {
                    connection.Close();
                }
            }
        }
        //nog te doen

        public IEnumerable<Voertuig> GeefVoertuigen(Bestuurder bestuurder) {
            throw new NotImplementedException();
        }
        //GeefVoertuig <
        // klopt nog niet, moeten eerst alle tabellen opvragen om te zien of er al een bestuurder is
        //public Voertuig geefVoertuig(string chassisNummer) {
        //    SqlConnection connection = GetConnection();
        //    string query = "SELECT (*) FROM [dbo].voertuig WHERE chassisNummer=@chassisNummer";
        //    using (SqlCommand command = connection.CreateCommand()) {
        //        connection.Open();
        //        try {
        //            command.Parameters.Add(new SqlParameter("@chassisNummer", SqlDbType.NVarChar));
        //            command.Parameters["@chassisNummer"].Value = chassisNummer;
        //            command.CommandText = query;

        //            SqlDataReader dataReader = command.ExecuteReader();
        //            dataReader.Read();
        //            string merkDB = (string)dataReader["merk"];
        //            string modelDB = (string)dataReader["model"];
        //            string chassisNummerDB = (string)dataReader["chassisNummer"];
        //            string nummerplaatDB = (string)dataReader["nummerplaat"];
        //            Brandstoftype brandstofTypeDB = (Brandstoftype)Enum.Parse(typeof(Brandstoftype), (string)dataReader["brandstoftype"]);
        //            Typewagen typewagenDB = (Typewagen)Enum.Parse(typeof(Typewagen), (string)dataReader["typeWagen"]);
        //            int aantalDeurenDB = (int)dataReader["aantalDeuren"];
        //            string kleurDB = (string)dataReader["kleur"];
        //            //Voertuig voertuig = new Voertuig(merkDB, modelDB, chassisNummerDB, nummerplaatDB, brandstofTypeDB, typewagenDB, aantalDeurenDB, kleurDB);

        //            dataReader.Close();

        //            //return voertuig;
        //        } catch (Exception ex) {
        //            throw new VoertuigRepositoryADOExceptions("geefVoertuig", ex);
        //        } finally {
        //            connection.Close();
        //        }
        //    }
        //}

        //public void UpdateVoertuig(Voertuig voertuig) {
        //    SqlConnection conn = GetConnection();
        //    string sql = "UPDATE [dbo].voertuig SET merk = @merk, model = @model, chassisNummer = @chassisNummer, nummerplaat = @nummerplaat, brandstofType =@brandstofType," +
        //        " typeWagen =@typeWagen, aantalDeuren=@aantalDeuren, kleur=@kleur WHERE Id = @Id";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        try {
        //            conn.Open();
        //            //nog af te werken
        //        } catch (Exception ex) {
        //            throw new BestuurderRepositoryADOException("UpdateVoertuig ", ex);
        //        } finally {
        //            conn.Close();
        //        }
        //    }
        //}
        public void VerwijderVoertuig(int Id) {
            SqlConnection conn = GetConnection();
            string sql = "DELETE FROM [dbo].voertuig WHERE Id = @Id";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("VerwijderVoertuig ", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        //public Voertuig VoegVoertuigToe(Voertuig voertuig) {
        //    SqlConnection conn = GetConnection();
        //    string sql = "INSERT INTO [dbo].voertuig (merk, model, chassisNummer,nummerplaat,brandstofType,typeWagen, aantalDeuren, kleur) " +
        //        "OUTPUT INSERTED.ID VALUES (@merk, @model, @chassisNummer, @nummerplaat, @brandstofType, @typeWagen, @aantalDeuren, @kleur)";
        //    using (SqlCommand cmd = conn.CreateCommand()) {
        //        try {
        //            conn.Open();
        //            cmd.CommandText = sql;
        //            cmd.Parameters.Add("@merk", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@model", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@chassisNummer", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@nummerplaat", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@brandstofType", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@typeWagen", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@aantalDeuren", SqlDbType.NVarChar);
        //            cmd.Parameters.Add("@kleur", SqlDbType.NVarChar);
        //            var merkDb = cmd.Parameters["@merk"].Value = voertuig.Merk;
        //            var modelDB = cmd.Parameters["@model"].Value = voertuig.Model;
        //            var chassisNummerDB =  cmd.Parameters["@chassisNummer"].Value = voertuig.ChassisNummer;
        //            var nummerplaatDB = cmd.Parameters["@nummerplaat"].Value = voertuig.NummerPlaat;
        //            var brandstofTypeDB = cmd.Parameters["@brandstofType"].Value = Enum.GetName(typeof(Brandstoftype_voertuig), voertuig.BrandstofType);
        //            var typeWagenDB = cmd.Parameters["@typeWagen"].Value = Enum.GetName(typeof(Typewagen), voertuig.TypeWagen);
        //            var aantalDeurenDB =  cmd.Parameters["@aantalDeuren"].Value = voertuig.AantalDeuren;
        //            var kleurDB = cmd.Parameters["@kleur"].Value = voertuig.Kleur;
        //            int id = (int)cmd.ExecuteScalar();
        //            Voertuig v = new Voertuig(voertuig.Merk, voertuig.Model, voertuig.ChassisNummer, voertuig.NummerPlaat, voertuig.BrandstofType, voertuig.TypeWagen,);
        //            v.ZetId(id);
        //            return v;
        //        } catch (Exception ex) {
        //            throw new VoertuigRepositoryADOExceptions("Voertuig toevoegen ", ex);
        //        } finally {
        //            conn.Close();
        //        }
        //    }
        //}

        void IVoertuigRepository.VoegVoertuigToe(Voertuig voertuig) {
            throw new NotImplementedException();
        }

        public void VerwijderVoertuig(Voertuig voertuig)
        {
            throw new NotImplementedException();
        }

        public void UpdateVoertuig(Voertuig voertuig) {
            throw new NotImplementedException();
        }
    }
}
