using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            SqlConnection connection = GetConnection();
            string query = "INSERT INTO [dbo].tankkaart (kaartnummer, geldigheidsdatum, pincode, bestuurder, geblokkeerd) " +
                "VALUES (@kaartnummer, @geldigheidsdatum, @pincode, @bestuurder, @geblokkeerd)";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@kaartnummer", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@geldigheidsdatum", SqlDbType.DateTime);
                    cmd.Parameters.Add("@pincode", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@bestuurder", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@geblokkeerd", SqlDbType.TinyInt);
                    var kaartnummerDb = cmd.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
                    var geldigheidsdatumDB = cmd.Parameters["@geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    var pincodeDB = cmd.Parameters["@pincode"].Value = tankkaart.Pincode;
                    var bestuurderDB = cmd.Parameters["@bestuurder"].Value = tankkaart.Bestuurder;
                    var geblokkeerdDB = cmd.Parameters["@geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    cmd.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
                    cmd.Parameters["@geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = tankkaart.Pincode;
                    cmd.Parameters["@bestuurder"].Value = tankkaart.Bestuurder;
                    cmd.Parameters["@geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("VoegTankkaartToe ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
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
                    command.Parameters.Add(new SqlParameter("@kaartnummer", SqlDbType.NVarChar));
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
            string query = "UPDATE tankkaart SET geldigheidsdatum=@geldigheidsdatum, pincode=@pincode, bestuurder=@bestuurder, geblokkeerd=@geblokkeerd WHERE kaartnummer=@kaartnummer";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    cmd.Parameters.Add("@kaartnummer", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@geldigheidsdatum", SqlDbType.DateTime);
                    cmd.Parameters.Add("@pincode", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@bestuurder", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@geblokkeerd", SqlDbType.TinyInt);
                    cmd.CommandText = query;
                    cmd.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
                    cmd.Parameters["@geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    cmd.Parameters["@pincode"].Value = tankkaart.Pincode;
                    cmd.Parameters["@bestuurder"].Value = tankkaart.Bestuurder;
                    cmd.Parameters["@geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("UpdateTankkaart", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public TankKaart GeefTankkaart(string kaartNr)
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT * FROM dbo.tankkaart WHERE kaartnummer = @kaartnummer+";
            using(SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@kaartnummer";
                paramId.DbType = DbType.String;
                paramId.Value = kaartNr;
                cmd.Parameters.Add(paramId);
                connection.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    TankKaart tankkaart = new TankKaart((string)reader["kaartnummer"], (DateTime)reader["geldigheidsdatum"], (string)reader["pincode"], bestuurder, (bool)reader["geblokkeerd"]);

                }
            }
        }

        public IReadOnlyList<TankKaart> GeefTankkaarten(string kaartnr, DateTime geldigheidsdatum, string pincode, Bestuurder bestuurder, bool geblokkeerd)
        {
            throw new NotImplementedException();
        }
    }
}
