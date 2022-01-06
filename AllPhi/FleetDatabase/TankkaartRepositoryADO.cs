using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FleetDatabase.FleetDatabaseExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FleetDatabase {
    public class TankkaartRepositoryADO : ITankkaartRepository {
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

        //public bool BestaatTankkaart(TankKaart tankkaart) //Done
        //{
        //    SqlConnection connection = GetConnection();
        //    string query =
        //        "SELECT Count (*) FROM [dbo].tankkaart"
        //        + "WHERE kaartnummer=@kaartnummer"
        //        + " AND geldigheidsdatum=@geldigheidsdatum"
        //        + " AND pincode=@pincode"
        //        + " AND bestuurder=@bestuurder"
        //        + " AND geblokkeerd=@geblokkeerd"
        //        + " AND brandstofType=@brandstofType";
        //    using (SqlCommand command = connection.CreateCommand())
        //    {
        //        connection.Open();
        //        try
        //        {
        //            command.Parameters.Add(new SqlParameter("@kaartnummer", SqlDbType.NVarChar));
        //            command.Parameters.Add(new SqlParameter("@geldigheidsdatum", SqlDbType.DateTime));
        //            command.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
        //            command.Parameters.Add(new SqlParameter("@bestuurder", SqlDbType.NVarChar));
        //            command.Parameters.Add(new SqlParameter("@geblokkeerd", SqlDbType.TinyInt));
        //            command.CommandText = query;
        //            command.Parameters["@kaartnummer"].Value = tankkaart.KaartNr;
        //            command.Parameters["@geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
        //            command.Parameters["@pincode"].Value = tankkaart.Pincode;
        //            command.Parameters["@bestuurder"].Value = tankkaart.Bestuurder;
        //            command.Parameters["@geblokkeerd"].Value = tankkaart.Geblokkeerd;
        //            int n = (int)command.ExecuteScalar();
        //            if (n > 0) return true;
        //            else return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new TankkaartRepositoryADOException("BestaatTankkaart", ex);
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
        public bool BestaatTankkaart(int tankkaartId) //Done
        {
            SqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].Tankkaart WHERE TankkaartId=@TankkaartId";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@TankkaartId"].Value = tankkaartId;
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
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
        public void VerwijderTankkaart(TankKaart tankkaart) //Done
        {
            SqlConnection connection = GetConnection();
            string query = "DELETE FROM dbo.Tankkaart WHERE TankkaartId=@TankkaartId";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@TankkaartId"].Value = tankkaart.TankkaartId;
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
        public void VoegTankkaartToe(TankKaart tankkaart)
        {
            SqlConnection connection = GetConnection();
            int tankkaartId;
            string query = "INSERT INTO [dbo].Tankkaart (Kaartnummer, Geldigheidsdatum, Pincode, BestuurderId, Isgeblokeerd) " +
                "OUTPUT INSERTED.TankkaartId VALUES(@Kaartnummer, @Geldigheidsdatum, @Pincode, @BestuurderId, @Geblokkeerd)";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@Kaartnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Geldigheidsdatum", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@Pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Geblokkeerd", SqlDbType.Bit));
                    cmd.CommandText = query;
                    cmd.Parameters["@Kaartnummer"].Value = tankkaart.KaartNr;
                    cmd.Parameters["@Geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    cmd.Parameters["@Geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    if (tankkaart.Pincode == null)
                    {
                        cmd.Parameters["@Pincode"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters["@Pincode"].Value = tankkaart.Pincode;
                    }
                    if (tankkaart.Bestuurder == null)
                    {
                        cmd.Parameters["@BestuurderId"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters["@BestuurderId"].Value = tankkaart.Bestuurder.BestuurderId;
                    }
                    tankkaartId = (int)cmd.ExecuteScalar();
                    tankkaart.ZetTankkaartId(tankkaartId);
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
        public void UpdateTankkaart(TankKaart tankkaart) //Done
        {
            SqlConnection connection = GetConnection();
            string query = "UPDATE tankkaart SET Kaartnummer=@Kaartnummer, Geldigheidsdatum=@Geldigheidsdatum, Pincode=@Pincode, BestuurderId=@BestuurderId, Isgeblokeerd=@Geblokkeerd WHERE TankkaartId=@TankkaartId";
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Kaartnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Geldigheidsdatum", SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@Pincode", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@BestuurderId", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Geblokkeerd", SqlDbType.TinyInt));
                    cmd.CommandText = query;
                    cmd.Parameters["@TankkaartId"].Value = tankkaart.TankkaartId;
                    cmd.Parameters["@Kaartnummer"].Value = tankkaart.KaartNr;
                    cmd.Parameters["@Geldigheidsdatum"].Value = tankkaart.Geldigheidsdatum;
                    cmd.Parameters["@Geblokkeerd"].Value = tankkaart.Geblokkeerd;
                    if (tankkaart.Pincode == null)
                    {
                        cmd.Parameters["@Pincode"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters["@Pincode"].Value = tankkaart.Pincode;
                    }
                    if (tankkaart.Bestuurder == null)
                    {
                        cmd.Parameters["@BestuurderId"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters["@BestuurderId"].Value = tankkaart.Bestuurder.BestuurderId;
                    }
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

        public TankKaart GeefTankkaart(int tankkaartId)
        {
            Bestuurder bestuurder = null;
            TankKaart tankkaart = null;
            string sql = "SELECT tk.*, bs.Voornaam, bs.Naam, bs.Geboortedatum, bs.Rijksregisternummer," +
                        " a.AdresId, a.Straat, a.Huisnummer, a.Gemeente, a.Postcode" +
                        " FROM Fleet.[dbo].Tankkaart tk" +
                        " LEFT JOIN Fleet.[dbo].Bestuurder bs ON bs.BestuurderId = tk.BestuurderId" +
                        " LEFT JOIN Fleet.[dbo].Adres a ON a.AdresId = bs.AdresId" +
                        " WHERE tk.TankkaartId = @TankkaartId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@TankkaartId", SqlDbType.Int));
                    command.CommandText = sql;
                    command.Parameters["@TankkaartId"].Value = tankkaartId;
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if ((reader["TankkaartId"].GetType() != typeof(DBNull)))
                    {
                        int tankkaartIdDB = (int)reader["TankkaartId"];
                        tankkaart = new TankKaart((string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], null, (bool)reader["Isgeblokeerd"], null);
                        tankkaart.ZetTankkaartId(tankkaartId);
                        if (reader["Brandstoftype"] != DBNull.Value)
                        {
                            Brandstoftype_tankkaart brandstofType = (Brandstoftype_tankkaart)Enum.Parse(typeof(Brandstoftype_tankkaart), (string)reader["Brandstoftype"]);
                            tankkaart.ZetBrandstoftype(brandstofType);
                        }
                    }
                    if (reader["BestuurderId"].GetType() != typeof(DBNull))
                    {
                        int bestuurderId = (int)reader["BestuurderId"];
                        BestuurderRepositoryADO repo = new BestuurderRepositoryADO(connectionString);
                        bestuurder = repo.GeefBestuurder(bestuurderId);
                        tankkaart.ZetBestuurder(bestuurder);
                        if (tankkaart.Bestuurder.TankKaart == null)
                        {
                            tankkaart.Bestuurder.ZetTankKaart(tankkaart);
                        }
                    }
                    return tankkaart;
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("TankkaartRepositoryADO - GeefTankkaart: ", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<TankKaart> ZoekTankkaarten(string? kaartnr, DateTime? geldigheidsdatum, string? pincode, Brandstoftype_tankkaart? brandstoftype, bool? geblokkeerd)
        {
            List<TankKaart> tankkaarten = new();
            TankKaart tankkaart = null;
            Bestuurder bestuurder = null;
            bool WHERE = true;
            bool AND = false;
            string sql = "SELECT tk.*, bs.Voornaam, bs.Naam, bs.Geboortedatum, bs.Rijksregisternummer," +
                        " a.AdresId, a.Straat, a.Huisnummer, a.Gemeente, a.Postcode" +
                        " FROM Fleet.[dbo].Tankkaart tk" +
                        " LEFT JOIN Fleet.[dbo].Bestuurder bs ON bs.BestuurderId = tk.BestuurderId" +
                        " LEFT JOIN Fleet.[dbo].Adres a ON a.AdresId = bs.AdresId";
            if (!string.IsNullOrEmpty(kaartnr))
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
                sql += "Kaartnummer = @Kaartnummer";
            }
            if (geldigheidsdatum.HasValue)
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
                sql += "Geldigheidsdatum = @Geldigheidsdatum";
            }
            if (!string.IsNullOrEmpty(pincode))
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
                sql += "Pincode = @Pincode";
            }
            if (brandstoftype != null)
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
                sql += "Brandstoftype = @Brandstoftype";
            }
            if (geblokkeerd.HasValue)
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
                sql += "Isgeblokeerd = @Isgeblokeerd";
            }
            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                try
                {
                    connection.Open();
                    if (!string.IsNullOrEmpty(kaartnr)) cmd.Parameters.AddWithValue("Kaartnummer", kaartnr);
                    if (geldigheidsdatum.HasValue) cmd.Parameters.AddWithValue("Geldigheidsdatum", geldigheidsdatum);
                    if (!string.IsNullOrEmpty(pincode)) cmd.Parameters.AddWithValue("Pincode", pincode);
                    if (brandstoftype != null) cmd.Parameters.AddWithValue("Brandstoftype", brandstoftype.ToString());
                    if (geblokkeerd.HasValue) cmd.Parameters.AddWithValue("Isgeblokeerd", geblokkeerd);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if ((reader["TankkaartId"].GetType() != typeof(DBNull)))
                        {
                            int tankkaartIdDB = (int)reader["TankkaartId"];
                            tankkaart = new TankKaart((string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], null, (bool)reader["Isgeblokeerd"], null);
                            tankkaart.ZetTankkaartId(tankkaartIdDB);
                            if (reader["Brandstoftype"] != DBNull.Value)
                            {
                                Brandstoftype_tankkaart brandstofType = (Brandstoftype_tankkaart)Enum.Parse(typeof(Brandstoftype_tankkaart), (string)reader["Brandstoftype"]);
                                tankkaart.ZetBrandstoftype(brandstofType);
                            }
                        }
                        if (reader["BestuurderId"].GetType() != typeof(DBNull))
                        {
                            int bestuurderId = (int)reader["BestuurderId"];
                            BestuurderRepositoryADO repo = new BestuurderRepositoryADO(connectionString);
                            bestuurder = repo.GeefBestuurder(bestuurderId);
                            tankkaart.ZetBestuurder(bestuurder);
                            if (tankkaart.Bestuurder.TankKaart == null)
                            {
                                tankkaart.Bestuurder.ZetTankKaart(tankkaart);
                            }
                        }
                        tankkaarten.Add(tankkaart);
                    }
                    return tankkaarten;
                }
                catch (Exception ex)
                {
                    throw new TankkaartRepositoryADOException("TankkaartRepositoryADOExceptions - GeefTankkaarten - Er liep iets mis - " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
