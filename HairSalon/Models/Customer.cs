using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Customer
    {
        private string _description;
        private int _id;
        private int _categoryId;

        public Customer
        (string description, int categoryId, int id = 0)
        {
            _description = description;
            _categoryId = categoryId;
            _id = id;
        }

        public override bool Equals(System.Object otherCustomer
        )
        {
          if (!(otherCustomer
           is Customer
           ))
          {
            return false;
          }
          else
          {
             Customer
              newCustomer
               = (Customer
               ) otherCustomer
               ;
             bool idEquality = this.GetId() == newCustomer
             .GetId();
             bool descriptionEquality = this.GetDescription() == newCustomer
             .GetDescription();
             bool categoryEquality = this.GetCategoryId() == newCustomer
             .GetCategoryId();
             return (idEquality && descriptionEquality && categoryEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetDescription().GetHashCode();
        }

        public string GetDescription()
        {
            return _description;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetCategoryId()
        {
            return _categoryId;
        }
        // public void Save()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"INSERT INTO items (description, category_id) VALUES (@description, @category_id);";
        //
        //     MySqlParameter description = new MySqlParameter();
        //     description.ParameterName = "@description";
        //     description.Value = this._description;
        //     cmd.Parameters.Add(description);
        //
        //     MySqlParameter categoryId = new MySqlParameter();
        //     categoryId.ParameterName = "@category_id";
        //     categoryId.Value = this._categoryId;
        //     cmd.Parameters.Add(categoryId);
        //
        //
        //     cmd.ExecuteNonQuery();
        //     _id = (int) cmd.LastInsertedId;
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }

        // public static List<Customer> GetAll()
        // {
        //     List<Customer> allCustomers = new List<Customer> {};
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM items;";
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     while(rdr.Read())
        //     {
        //       int itemId = rdr.GetInt32(0);
        //       string itemDescription = rdr.GetString(1);
        //       int itemCategoryId = rdr.GetInt32(2);
        //       Customer
        //        newCustomer
        //         = new Customer
        //         (itemDescription, itemCategoryId, itemId);
        //       allCustomer
        //       s.Add(newCustomer
        //       );
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return allCustomer
        //     s;
        // }

        // public static Customer
        //  Find(int id)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM items WHERE id = (@searchId);";
        //
        //     MySqlParameter searchId = new MySqlParameter();
        //     searchId.ParameterName = "@searchId";
        //     searchId.Value = id;
        //     cmd.Parameters.Add(searchId);
        //
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     int itemId = 0;
        //     string itemName = "";
        //     int itemCategoryId = 0;
        //
        //     while(rdr.Read())
        //     {
        //       itemId = rdr.GetInt32(0);
        //       itemName = rdr.GetString(1);
        //       itemCategoryId = rdr.GetInt32(2);
        //     }
        //     Customer
        //      newCustomer
        //       = new Customer
        //       (itemName, itemCategoryId, itemId);
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return newCustomer
        //     ;
        // }

        // public static void DeleteAll()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"DELETE FROM items;";
        //     cmd.ExecuteNonQuery();
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }
    }
}
