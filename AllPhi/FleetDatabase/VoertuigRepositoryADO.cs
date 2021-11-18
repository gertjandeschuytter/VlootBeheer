using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetDatabase {
    public class VoertuigRepositoryADO : IVoertuigRepository {
        //fields
        private readonly string connectionString;
        //ctor
        public VoetbaltruitjeRepositoryADO(string connectionString) {
            this.connectionString = connectionString;
        }
        private SqlConnection GetConnection() {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatVoertuig(string chassisNummer) {
            
        }

        public bool BestaatVoertuig(Voertuig voertuig) {
            SqlConnection connection = GetConnection();
            string query =
                "SELECT Count (*) FROM dbo.voertuig"
                + "WHERE Ploeg=@ploeg"
                + "AND UitThuis=@uitthuis"
                + "AND competitie=@competitie"
                + "AND kledingmaat=@kledingmaat"
                + "AND versie=@versie"
                + "AND seizoen=@seizoen";
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@ploeg", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@competitie", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@UitThuis", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@kledingmaat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@seizoen", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@versie", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@ploeg"].Value = truitje.Club.Ploeg;
                    command.Parameters["@competitie"].Value = truitje.Club.Competitie;
                    if (truitje.ClubSet.Thuis) command.Parameters["@uitthuis"].Value = "Thuis";
                    else command.Parameters["@uitthuis"].Value = "Uit";
                    command.Parameters["@kledingmaat"].Value = Enum.GetName(typeof(Kledingmaat), truitje.Kledingmaat);
                    command.Parameters["@versie"].Value = truitje.ClubSet.Versie;
                    command.Parameters["@seizoen"].Value = truitje.Seizoen;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new VoetbaltruitjeRepositoryADOExceptions("BestaatVoetbaltruitje", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Voertuig> GeefVoertuigen(Bestuurder bestuurder) {
            throw new NotImplementedException();
        }
        public Voertuig geefVoertuig(Bestuurder bestuurder) {
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
