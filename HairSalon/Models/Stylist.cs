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

        // public override bool Equals(System.Object otherStylist)
        // {
        //     if (!(otherStylist is Stylist))
        //     {
        //         return false;
        //     }
        //     else
        //     {
        //         Stylist newStylist = (Stylist) otherStylist;
        //         return this.GetId().Equals(newStylist.GetId());
        //     }
        // } //for future test to compare two instance
        //
        // public override int GetHashCode()
        // {
        //     return this.GetId().GetHashCode();
        // }//for future test to compare two instance

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

        // public static void DeleteAll()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"DELETE FROM categories;";
        //     cmd.ExecuteNonQuery();
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }
    }
}
