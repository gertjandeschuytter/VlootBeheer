using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FleetDatabase {
    public class VoertuigRepositoryADO : IVoertuigRepository {
        private static string connectionString = @"Data Source=DESKTOP-R7T8D5F\SQLEXPRESS;Initial Catalog=Adres;Integrated Security=True";

        public bool BestaatVoertuig(string chassisnummer) {
            bool allesOk = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM voertuig WHERE ChassisNummer=@ChassisNummer";
            using (SqlCommand command = connection.CreateCommand()) {
                command.CommandText = query;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@ChassisNummer";
                paramId.DbType = DbType.String;
                paramId.Value = voertuigChassisNummer;
                command.Parameters.Add(paramId);
                connection.Open();
                try {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        allesOk = true;
                } catch (Exception) {

                    throw;
                }
            }
            return allesOk;
        }

        public Voertuig Geefvoertuig(string chassisNummer) {
            throw new NotImplementedException();
        }

        public void UpdateVoertuig(Voertuig voertuig) {
            throw new NotImplementedException();
        }

        public void VerwijderVoertuig(Voertuig voertuig) {
            throw new NotImplementedException();
        }

        public void VoegVoertuigToe(Voertuig voertuig) {
            throw new NotImplementedException();
        }
    }
}
