using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Specialty
    {
        private string _name;
        private int _id;

        public Specialty (string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public override bool Equals(System.Object otherspecialty
        )
        {
          if (!(otherspecialty is Specialty))
          {
            return false;
          }
          else
          {
             Specialty newSpecialty = (Specialty) otherspecialty;
             bool nameEquality = this.GetName() == newSpecialty.GetName();
             return nameEquality;
           }
        }

        public override int GetHashCode()
        {
             return this.GetId().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@nameValue);";

            MySqlParameter customerName = new MySqlParameter();
            customerName.ParameterName = "@nameValue";
            customerName.Value = this._name;
            cmd.Parameters.Add(customerName);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int specialtyIdRes = rdr.GetInt32(0);
              string SpecialtyNameRes = rdr.GetString(1);
              Specialty newSpecialty = new Specialty(SpecialtyNameRes, specialtyIdRes);
              allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialtyIdRes = 0;
            string SpecialtyNameRes = "";

            while(rdr.Read())
            {
              specialtyIdRes = rdr.GetInt32(0);
              SpecialtyNameRes = rdr.GetString(1);
            }

            Specialty newSpecialty = new Specialty(SpecialtyNameRes, specialtyIdRes);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSpecialty;
        }

        // public void UpdateName(string newName)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"UPDATE customers SET name = @newName WHERE id = @searchId;";
        //
        //     MySqlParameter searchId = new MySqlParameter();
        //     searchId.ParameterName = "@searchId";
        //     searchId.Value = _id;
        //     cmd.Parameters.Add(searchId);
        //
        //     MySqlParameter description = new MySqlParameter();
        //     description.ParameterName = "@newName";
        //     description.Value = newName;
        //     cmd.Parameters.Add(description);
        //
        //     cmd.ExecuteNonQuery();
        //     _name = newName;
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }
        //
        // public static List<Customer> FindByStylistId(int stylistId)
        // {
        //     List<Customer> allCustomers = new List<Customer> {};
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM customers WHERE stylist_id = (@stylistSearchId);";
        //
        //     MySqlParameter stylistIdParam = new MySqlParameter();
        //     stylistIdParam.ParameterName = "@stylistSearchId";
        //     stylistIdParam.Value = stylistId;
        //     cmd.Parameters.Add(stylistIdParam);
        //
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     while(rdr.Read())
        //     {
        //       int customerIdRes = rdr.GetInt32(0);
        //       string customerNameRes = rdr.GetString(1);
        //       int stylistIdRes = rdr.GetInt32(2);
        //       Customer newCustomer = new Customer(customerNameRes, stylistIdRes, customerIdRes);
        //       allCustomers.Add(newCustomer);
        //     }
        //
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //
        //     return allCustomers;
        // }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";

            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@specialtyId";
            specialty_id.Value = _id;
            cmd.Parameters.Add(specialty_id);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylist_id FROM stylists_specialties WHERE specialty_id = @specialtyId;";

            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@specialtyId";
            specialtyIdParameter.Value = _id;
            cmd.Parameters.Add(specialtyIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<int> stylistsIds = new List<int> {};
            while(rdr.Read())
            {
                int stylistsId = rdr.GetInt32(0);
                stylistsIds.Add(stylistsId);
            }
            rdr.Dispose();

            List<Stylist> stylists = new List<Stylist> {};
            foreach (int stylistsId in stylistsIds)
            {
                var categoryQuery = conn.CreateCommand() as MySqlCommand;
                categoryQuery.CommandText = @"SELECT * FROM stylists WHERE id = @stylistId;";

                MySqlParameter stylistIdParameter = new MySqlParameter();
                stylistIdParameter.ParameterName = "@stylistId";
                stylistIdParameter.Value = stylistsId;
                categoryQuery.Parameters.Add(stylistIdParameter);

                var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
                while(categoryQueryRdr.Read())
                {
                    int thisStylistId = categoryQueryRdr.GetInt32(0);
                    string stylistName = categoryQueryRdr.GetString(1);
                    Stylist foundStylist = new Stylist(stylistName, thisStylistId);
                    stylists.Add(foundStylist);
                }
                categoryQueryRdr.Dispose();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialities;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialities WHERE id = @searchId; DELETE FROM stylists_specialties WHERE specialty_id = @searchId;";

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
    }
}
