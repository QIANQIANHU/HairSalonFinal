using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Stylist
    {
        private string _name;
        private int _id;
        private List<Customer> _customers;

        public Stylist(string name, int id = 0)
        {
            _name = name;
            _id = id;
            _customers = new List<Customer> {};
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                return this.GetId().Equals(newStylist.GetId()) && this.GetName().Equals(newStylist.GetName());
            }
        } //for future test to compare two instance

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        } //for future test to compare two instance

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetCustomers(List<Customer> customers) {
          _customers = customers;
        }

        public List<Customer> GetCustomers() {
          return _customers;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@nameValue);"; // @name comes from views (via controller)

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@nameValue";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        /*
          this method is used to retrieve all the stylists from DB
        */
        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int StylistId = rdr.GetInt32(0);
              string StylistName = rdr.GetString(1);
              Stylist newStylist = new Stylist(StylistName, StylistId);
              allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int StylistId = 0;
            string StylistName = "";

            while(rdr.Read())
            {
              StylistId = rdr.GetInt32(0);
              StylistName = rdr.GetString(1);
            }

            Stylist newStylist = new Stylist(StylistName, StylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }

        public static Stylist FindByStylistName(string stylistName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE name = (@stylistNameValue);";

            MySqlParameter stylistNameParam = new MySqlParameter();
            stylistNameParam.ParameterName = "@stylistNameValue";
            stylistNameParam.Value = stylistName;
            cmd.Parameters.Add(stylistNameParam);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistIdRes = 0;
            string stylistNameRes = "";

            while(rdr.Read())
            {
              stylistIdRes = rdr.GetInt32(0);
              stylistNameRes = rdr.GetString(1);
            }

            Stylist newStylist = new Stylist(stylistNameRes, stylistIdRes);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }

        public void UpdateName(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newName";
            description.Value = newName;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _name = newName;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Addspecialty(Specialty newSpecialty)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";

            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@specialtyId";
            specialty_id.Value = newSpecialty.GetId();
            cmd.Parameters.Add(specialty_id);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = _id;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Specialty> GetSpecialties()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialty_id FROM stylists_specialties WHERE stylist_id = @stylistId;";

            MySqlParameter itemIdParameter = new MySqlParameter();
            itemIdParameter.ParameterName = "@stylistId";
            itemIdParameter.Value = _id;
            cmd.Parameters.Add(itemIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> specialtyIds = new List<int> {};
            while(rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                specialtyIds.Add(specialtyId);
            }
            rdr.Dispose();

            List<Specialty> specialties = new List<Specialty> {};
            foreach (int specialtyId in specialtyIds)
            {
                var categoryQuery = conn.CreateCommand() as MySqlCommand;
                categoryQuery.CommandText = @"SELECT * FROM Specialties WHERE id = @specialtyId;";

                MySqlParameter categoryIdParameter = new MySqlParameter();
                categoryIdParameter.ParameterName = "@specialtyId";
                categoryIdParameter.Value = specialtyId;
                categoryQuery.Parameters.Add(categoryIdParameter);

                var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
                while(categoryQueryRdr.Read())
                {
                    int thisspecialtyId = categoryQueryRdr.GetInt32(0);
                    string specialtyName = categoryQueryRdr.GetString(1);
                    Specialty foundSpecialty = new Specialty(specialtyName, thisspecialtyId);
                    specialties.Add(foundSpecialty);
                }
                categoryQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return specialties;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @searchId; DELETE FROM stylists_specialties WHERE stylist_id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
