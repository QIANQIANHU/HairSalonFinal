using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Customer
    {
        private string _name;
        private int _id;
        private int _stylistId;

        public Customer (string name, int stylistId, int id = 0)
        {
            _name = name;
            _id = id;
            _stylistId = stylistId;
        }

        public override bool Equals(System.Object otherCustomer
        )
        {
          if (!(otherCustomer is Customer))
          {
            return false;
          }
          else
          {
             Customer newCustomer = (Customer) otherCustomer;
             // Console.WriteLine(this.GetName());
             // Console.WriteLine(newCustomer.GetName());
             // Console.WriteLine(this.GetStylistId());
             // Console.WriteLine(newCustomer.GetStylistId());
             bool nameEquality = this.GetName() == newCustomer.GetName();
             bool stylistIdEquality = this.GetStylistId() == newCustomer.GetStylistId();
             return (nameEquality && stylistIdEquality);
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

        public int GetStylistId()
        {
            return _stylistId;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO customers (name, stylist_id) VALUES (@nameValue, @stylist_idValue);";

            MySqlParameter customerName = new MySqlParameter();
            customerName.ParameterName = "@nameValue";
            customerName.Value = this._name;
            cmd.Parameters.Add(customerName);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_idValue";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Customer> GetAll()
        {
            List<Customer> allCustomers = new List<Customer> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int customerIdRes = rdr.GetInt32(0);
              string customerNameRes = rdr.GetString(1);
              int stylistIdRes = rdr.GetInt32(2);
              Customer newCustomer = new Customer(customerNameRes, stylistIdRes, customerIdRes);
              allCustomers.Add(newCustomer);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCustomers;
        }

        public static Customer Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int customerIdRes = 0;
            string customerNameRes = "";
            int stylistIdRes = 0;

            while(rdr.Read())
            {
              customerIdRes = rdr.GetInt32(0);
              customerNameRes = rdr.GetString(1);
              stylistIdRes = rdr.GetInt32(2);
            }

            Customer newCustomer = new Customer(customerNameRes, stylistIdRes, customerIdRes);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCustomer;
        }

        public void UpdateName(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE customers SET name = @newName WHERE id = @searchId;";

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

        public static List<Customer> FindByStylistId(int stylistId)
        {
            List<Customer> allCustomers = new List<Customer> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM customers WHERE stylist_id = (@stylistSearchId);";

            MySqlParameter stylistIdParam = new MySqlParameter();
            stylistIdParam.ParameterName = "@stylistSearchId";
            stylistIdParam.Value = stylistId;
            cmd.Parameters.Add(stylistIdParam);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int customerIdRes = rdr.GetInt32(0);
              string customerNameRes = rdr.GetString(1);
              int stylistIdRes = rdr.GetInt32(2);
              Customer newCustomer = new Customer(customerNameRes, stylistIdRes, customerIdRes);
              allCustomers.Add(newCustomer);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allCustomers;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM customers;";
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
            cmd.CommandText = @"DELETE FROM customers WHERE id = @searchId;";

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
