using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetDatabase
{
    public class TankkaartRepositoryADO : ITankkaartRepository
    {
        private readonly string connectionString;

        public TankkaartRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        //Nodig of niet?
        //public bool BestaatTankaart(string kaartNr)
        //{
        //    SqlConnection conn = GetConnection();
        //    string query = "SELECT (*) FROM [dbo].tankkaart WHERE kaartnummer=@kaartnummer";
        //    using (SqlCommand cmd = conn.CreateCommand())
        //    {
        //        try
        //        {
        //            cmd.Parameters.Add(new SqlParameter("@kaartnummer", SqlDbType.NVarChar));
        //            cmd.CommandText = query;
        //            cmd.Parameters["@kaartnummer"].Value = kaartNr;
        //            int n = (int)cmd.ExecuteScalar();
        //            if (n > 0) return true;
        //            else return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new TankkaartRepositoryADOException("bestaatTankkaart", ex);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        public bool BestaatTankkaart(TankKaart tankkaart)
        {
            SqlConnection connection = GetConnection();
            string query =
                "SELECT Count (*) FROM [dbo].tankkaart"
                + "WHERE kaartnummer=@kaartnummer"
                + " AND geldigheidsdatum=@geldigheidsdatum"
                + " AND pincode=@pincode"
                + " AND bestuurder=@bestuurder"
                + " AND geblokkeerd=@geblokkeerd";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@kaartnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@bestuurder", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.TinyInt));
                    command.CommandText = query;
                    command.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
                    command.Parameters["@geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    command.Parameters["@pincode"].Value = tankkaart.Pincode;
                    command.Parameters["@bestuurder"].Value = tankkaart.Bestuurder;
                    command.Parameters["@geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("BestaatTankkaart", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VoegTankkaartToe(TankKaart tankkaart)
        {
            throw new NotImplementedException();
        }

        public void VerwijderTankkaart(TankKaart tankkaart)
        {
            SqlConnection connection = GetConnection();
            string query = "DELETE FROM dbo.tankkaart WHERE kaartnummer=@kaartnummer";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@kaartnummer", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("VerwijderTankkaart", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateTankkaart(TankKaart tankkaart)
        {
            SqlConnection connection = GetConnection();
            string query = "UPDATE tankkaart SET kaartnummer=@kaartnummer, geldigheidsdatum=@geldigheidsdatum, pincode=@pincode, bestuurder=@bestuurder, geblokkeerd=@geblokkeerd";

        }

        public TankKaart GeefTankkaart(string kaartNr)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TankKaart> GeefTankkaarten(string kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd)
        {
            throw new NotImplementedException();
        }
    }
}
