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
        
        public void VoegVoertuigToe(Voertuig voertuig) {
                int voertuigId;
                string query = "INSERT INTO voertuig(Merk, Model, Chassisnummer, Nummerplaat, Brandstoftype, Wagentype, Kleur, Aantaldeuren)" +
                    "OUTPUT INSERTED.VoertuigId VALUES (@Merk, @Model, @Chassisnummer, @Nummerplaat, @Brandstoftype, @Wagentype, @Kleur, @Aantaldeuren);";

            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@Merk", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Model", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Chassisnummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Nummerplaat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Brandstoftype", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Wagentype", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Kleur", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Aantaldeuren", SqlDbType.Int));
                    command.CommandText = query;
                    command.Parameters["@Merk"].Value = voertuig.Merk;
                    command.Parameters["@Model"].Value = voertuig.Model;
                    command.Parameters["@Chassisnummer"].Value = voertuig.ChassisNummer;
                    command.Parameters["@Nummerplaat"].Value = voertuig.NummerPlaat;
                    command.Parameters["@Brandstoftype"].Value = voertuig.BrandstofType;
                    command.Parameters["@Wagentype"].Value = voertuig.TypeWagen;
                    if (voertuig.Kleur == null) {
                        command.Parameters["@Kleur"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@Kleur"].Value = voertuig.Kleur;
                    }
                    if (voertuig.AantalDeuren == 0) {
                        command.Parameters["@AantalDeuren"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@AantalDeuren"].Value = voertuig.AantalDeuren;
                    }
                    voertuigId = (int)command.ExecuteScalar();
                    voertuig.ZetId(voertuigId);
                } catch (Exception ex) {
                    throw new("VoertuigRepositoryADO - GeefVoertuigen: Er liep iets mis -> ", ex);
                } finally {
                    connection.Close();
                }
            }
        }
        public bool BestaatVoertuig(int VoertuigId) {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].voertuig WHERE VoertuigId=@VoertuigId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.NVarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@VoertuigId"].Value = VoertuigId;
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("bestaatVoertuig", ex);
                } finally {
                    conn.Close();
                }
            }
        }
        public void UpdateVoertuig(Voertuig voertuig) {
            string sqlUpdate = "UPDATE Voertuig SET Merk=@Merk,Model=@Model,Chassisnummer=@Chassisnummer,Nummerplaat=@Nummerplaat" +
            ",Brandstoftype=@Brandstoftype,Wagentype=@Wagentype,Kleur=@Kleur,Aantaldeuren=@Aantaldeuren WHERE VoertuigId=@VoertuigId";
            SqlConnection connection = GetConnection();
            //Update
            using (SqlCommand command = connection.CreateCommand()) {
                try {
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
                    command.Parameters["@VoertuigId"].Value = voertuig.ID;
                    command.Parameters["@Merk"].Value = voertuig.Merk;
                    command.Parameters["@Model"].Value = voertuig.Model;
                    command.Parameters["@Chassisnummer"].Value = voertuig.ChassisNummer;
                    command.Parameters["@Nummerplaat"].Value = voertuig.NummerPlaat;
                    command.Parameters["@Brandstoftype"].Value = voertuig.BrandstofType;
                    command.Parameters["@Wagentype"].Value = voertuig.TypeWagen;
                    if (voertuig.Kleur == null) {
                        command.Parameters["@Kleur"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@Kleur"].Value = voertuig.Kleur;
                    }
                    if (voertuig.AantalDeuren == 0) {
                        command.Parameters["@AantalDeuren"].Value = DBNull.Value;
                    } else {
                        command.Parameters["@AantalDeuren"].Value = voertuig.AantalDeuren;
                    }
                    command.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new BestuurderRepositoryADOException("WijzigAdresBestuurder - UpdateAdres " + ex.Message);
                } finally {
                    connection.Close();
                }
            }
        }
        public void VerwijderVoertuig(Voertuig voertuig) {
            string querydeleteVoertuig = "DELETE FROM Fleet.[dbo].Voertuig WHERE VoertuigId=@VoertuigId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.Int));
                    command.CommandText = querydeleteVoertuig;
                    command.Parameters["@VoertuigId"].Value = voertuig.ID;
                    command.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("Voertuig kon niet verwijderd worden" + ex.Message);
                } finally {
                    connection.Close();
                }
            }
        }

        public IReadOnlyList<Voertuig> GeefVoertuigen(string? merk, string? model, string? chassisnummer, string? nummerplaat, string? brandstoftype, string? wagentype, string? kleur, int? aantaldeuren) {
            List<TypeRijbewijs> RijbewijzenLijst = new();
            List<Voertuig> Voertuigen = new();
            Bestuurder bestuurder = null;
            Voertuig voertuig = null;
            bool WHERE = true;
            bool AND = false;
            string sql = "SELECT vs.*, bs.Naam, bs.Voornaam, bs.Geboortedatum,bs.Rijksregisternummer, bs.AdresId,a.Gemeente," +
                "a.Huisnummer, a.Postcode, a.Straat, tk.Geldigheidsdatum, tk.Isgeblokeerd, tk.Kaartnummer, tk.Pincode, tk.TankkaartId " +
                "FROM Fleet.[dbo].Voertuig vs " +
                "LEFT JOIN Fleet.[dbo].Bestuurder bs ON bs.VoertuigId = vs.VoertuigId " +
                "LEFT JOIN Fleet.[dbo].Tankkaart tk ON bs.TankkaartId = tk.TankkaartId " +
                "LEFT JOIN Fleet.[dbo].Adres a ON bs.AdresId = a.AdresId ";
            if (!string.IsNullOrEmpty(merk)) {
                if (WHERE) {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND) {
                    sql += " AND ";
                } else {
                    AND = true;
                }
                sql += "Merk = @Merk";
            }
            if (!string.IsNullOrEmpty(model)) {
                if (WHERE) {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND) {
                    sql += " AND ";
                } else {
                    AND = true;
                }
                sql += "Model = @Model";
            }
            if (!string.IsNullOrEmpty(brandstoftype)) {
                if (WHERE) {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND) {
                    sql += " AND ";
                } else {
                    AND = true;
                }
                sql += "vs.Brandstoftype = @Brandstoftype";
            }
            if (!string.IsNullOrEmpty(wagentype)) {
                if (WHERE) {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND) {
                    sql += " AND ";
                } else {
                    AND = true;
                }
                sql += "Wagentype = @Wagentype";
            }
            if (!string.IsNullOrEmpty(kleur)) {
                if (WHERE) {
                    sql += " WHERE ";
                    WHERE = false;
                }
                if (AND) {
                    sql += " AND ";
                } else {
                    AND = true;
                }
                sql += "Kleur = @Kleur";
            }
            if (aantaldeuren.HasValue) {
                if (WHERE) {
                    sql += " WHERE ";
                }
                if (AND) {
                    sql += " AND ";
                }
                sql += "wagentype=@wagentype";
            }
            if (!string.IsNullOrEmpty(chassisnummer))
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
                sql += "Chassisnummer = @chassisnummer";

            }
            if (!string.IsNullOrEmpty(nummerplaat))
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
                sql += "Nummerplaat = @nummerplaat";

            }

            SqlConnection conn = GetConnection();
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.CommandText = sql;
                try {
                    conn.Open();
                    if (!string.IsNullOrEmpty(merk)) cmd.Parameters.AddWithValue("@merk", merk);
                    if (!string.IsNullOrEmpty(model)) cmd.Parameters.AddWithValue("@model", model);
                    if (!string.IsNullOrEmpty(brandstoftype)) cmd.Parameters.AddWithValue("@Brandstoftype", brandstoftype);
                    if (!string.IsNullOrEmpty(wagentype)) cmd.Parameters.AddWithValue("@wagentype", wagentype);
                    if (!string.IsNullOrEmpty(kleur)) cmd.Parameters.AddWithValue("@kleur", kleur);
                    if (aantaldeuren.HasValue) cmd.Parameters.AddWithValue("@aantaldeuren", aantaldeuren);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int bestuurderId = (int)reader["BestuurderId"];
                        if (reader["VoertuigId"].GetType() != typeof(DBNull)) {
                            Brandstoftype_voertuig brandstofType = (Brandstoftype_voertuig)Enum.Parse(typeof(Brandstoftype_voertuig), (string)reader["Brandstoftype"]);
                            Typewagen wagenType = (Typewagen)Enum.Parse(typeof(Typewagen), (string)reader["WagenType"]);
                            if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] == DBNull.Value) {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, null);
                            } else if (reader["Kleur"] != DBNull.Value && reader["Aantaldeuren"] == DBNull.Value) {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], null);
                            } else if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] != DBNull.Value) {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, (int)reader["Aantaldeuren"]);
                            } else {
                                voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], (int)reader["Aantaldeuren"]);
                            }
                            voertuig.ZetId((int)reader["VoertuigId"]);
                        }
                        if (reader["BestuurderId"].GetType() != typeof(DBNull)) {
                            if (!BestuurderHeeftEenOfMeerdereRijbewijzen(bestuurderId)) {
                                bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], RijbewijzenLijst);
                            } else {
                                bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], GeefTypeRijbewijzenVanBestuurderInVoertuig(bestuurderId));
                            }
                            voertuig.ZetBestuurder(bestuurder);
                        }
                        if (reader["AdresId"].GetType() != typeof(DBNull)) {
                            Adres adres = new Adres((int)reader["AdresId"], (string)reader["Straat"], (string)reader["Gemeente"], (int)reader["Postcode"], (string)reader["Huisnummer"]);
                            voertuig.Bestuurder.ZetAdres(adres);
                        }
                        if ((reader["TankkaartId"].GetType() != typeof(DBNull))) {
                            int tankkaartIdDB = (int)reader["TankkaartId"];
                            TankKaart tankKaart = new TankKaart(tankkaartIdDB, (string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], bestuurder, (bool)reader["Isgeblokeerd"], null);
                            voertuig.Bestuurder.ZetTankKaart(tankKaart);
                        }
                        Voertuigen.Add(voertuig);
                    }
                    return Voertuigen;
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("VoertuigRepositoryADOExceptions - GeefVoertuigen - Er liep iets mis - " + ex.Message);
                } finally {
                    conn.Close();
                }
            }
        }
        public Voertuig geefVoertuig(int voertuigId) {
            List<TypeRijbewijs> RijbewijzenLijst = new();
            Bestuurder bestuurder = null;
            Voertuig voertuig = null;
            string sql = "SELECT vs.*, bs.Naam, bs.Voornaam, bs.Geboortedatum,bs.Rijksregisternummer, bs.AdresId,a.Gemeente," +
                "a.Huisnummer, a.Postcode, a.Straat, tk.Geldigheidsdatum, tk.Isgeblokeerd, tk.Kaartnummer, tk.Pincode, tk.TankkaartId " +
                "FROM Fleet.[dbo].Voertuig vs " +
                "LEFT JOIN Fleet.[dbo].Bestuurder bs ON bs.BestuurderId = vs.BestuurderId " +
                "LEFT JOIN Fleet.[dbo].Tankkaart tk ON bs.TankkaartId = tk.TankkaartId " +
                "LEFT JOIN Fleet.[dbo].Adres a ON bs.AdresId = a.AdresId " +
                "WHERE vs.VoertuigId = @VoertuigId";
            SqlConnection connection = GetConnection();
            using (SqlCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.Parameters.Add(new SqlParameter("@VoertuigId", SqlDbType.Int));
                    command.CommandText = sql;
                    command.Parameters["@VoertuigId"].Value = voertuigId;
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader["VoertuigId"].GetType() != typeof(DBNull)) {
                        Brandstoftype_voertuig brandstofType = (Brandstoftype_voertuig)Enum.Parse(typeof(Brandstoftype_voertuig), (string)reader["Brandstoftype"]);
                        Typewagen wagenType = (Typewagen)Enum.Parse(typeof(Typewagen), (string)reader["WagenType"]);
                        if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] == DBNull.Value) {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, null);
                        } else if (reader["Kleur"] != DBNull.Value && reader["Aantaldeuren"] == DBNull.Value) {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], null);
                        } else if (reader["Kleur"] == DBNull.Value && reader["Aantaldeuren"] != DBNull.Value) {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, null, (int)reader["Aantaldeuren"]);
                        } else {
                            voertuig = new Voertuig((string)reader["Merk"], (string)reader["Model"], (string)reader["Chassisnummer"], (string)reader["Nummerplaat"], brandstofType, wagenType, (string)reader["Kleur"], (int)reader["Aantaldeuren"]);
                        }
                        voertuig.ZetId((int)reader["VoertuigId"]);
                    }
                    if (reader["BestuurderId"].GetType() != typeof(DBNull)) {
                        int bestuurderId = (int)reader["BestuurderId"];
                        if (!BestuurderHeeftEenOfMeerdereRijbewijzen(bestuurderId)) {
                            bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], RijbewijzenLijst);
                        } else {
                            bestuurder = new Bestuurder((string)reader["Naam"], (string)reader["Voornaam"], (DateTime)reader["Geboortedatum"], (string)reader["Rijksregisternummer"], GeefTypeRijbewijzenVanBestuurderInVoertuig(bestuurderId));
                        }
                        bestuurder.ZetID(bestuurderId);
                        voertuig.ZetBestuurder(bestuurder);
                    }
                    if (reader["AdresId"].GetType() != typeof(DBNull)) {
                        Adres adres = new Adres((int)reader["AdresId"], (string)reader["Straat"], (string)reader["Gemeente"], (int)reader["Postcode"], (string)reader["Huisnummer"]);
                        voertuig.Bestuurder.ZetAdres(adres);
                    }
                    if ((reader["TankkaartId"].GetType() != typeof(DBNull))) {
                        int tankkaartIdDB = (int)reader["TankkaartId"];
                        TankKaart tankKaart = new TankKaart(tankkaartIdDB, (string)reader["Kaartnummer"], (DateTime)reader["Geldigheidsdatum"], (string)reader["Pincode"], bestuurder, (bool)reader["Isgeblokeerd"], null);
                        tankKaart.ZetTankkaartId(tankkaartIdDB);
                        voertuig.Bestuurder.ZetTankKaart(tankKaart);
                    }
                    return voertuig;
                } catch (Exception ex) {
                    throw new VoertuigRepositoryADOExceptions("VoertuigRepositoryException - Geefvoertuig: Er liep iets mis -> ", ex);
                } finally {
                    connection.Close();
                }
            }

        }
        public bool BestaatVoertuig(Voertuig Voertuig)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].voertuig WHERE Merk = @merk, Model = @model, BrandstofType = @brandstof, TypeWagen = @wagen, Chassisnummer = @chassisnummer, Nummerplaat = @nummerplaat, Kleur = @kleur, AantalDeuren = @aantalDeuren";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@brandstof", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@wagen", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@chassisnummer", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@aantalDeuren", SqlDbType.Int));

                    cmd.CommandText = query;
                    cmd.Parameters["@merk"].Value = Voertuig.Merk;
                    cmd.Parameters["@model"].Value = Voertuig.Model;
                    cmd.Parameters["@brandstof"].Value = Voertuig.BrandstofType.ToString();
                    cmd.Parameters["@wagen"].Value = Voertuig.TypeWagen.ToString();
                    cmd.Parameters["@chassisnummer"].Value = Voertuig.ChassisNummer;
                    cmd.Parameters["@nummerplaat"].Value = Voertuig.NummerPlaat;
                    cmd.Parameters["@kleur"].Value = Voertuig.Kleur;
                    cmd.Parameters["@aantalDeuren"].Value = Voertuig.AantalDeuren;

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

        private bool BestuurderHeeftEenOfMeerdereRijbewijzen(int bestuurderId)
        {
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM [dbo].BestuurderRijbewijs WHERE bestuurderId=@bestuurderId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
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
        private List<TypeRijbewijs> GeefTypeRijbewijzenVanBestuurderInVoertuig(int bestuurderId)
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
    }
}