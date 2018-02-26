using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{

    [TestClass]
    public class CustomerTests : IDisposable
    {
        public CustomerTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=qianqian_hu_test;";
        }

        // clean up all the data before running the next test
        public void Dispose()
        {
          Customer.DeleteAll();
          Stylist.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameDescription_Customer()
        {
          //Arrange, Act
          Customer firstCustomer = new Customer("Mow the lawn", 1);
          Customer secondCustomer = new Customer("Mow the lawn", 1);

          //Assert
          Assert.AreEqual(firstCustomer, secondCustomer);
        }

        // Todo: enable the following tests later
        // [TestMethod]
        // public void Save_SavesCustomerToDatabase_ItemList()
        // {
        //   //Arrange
        //   Customer testCustomer = new Customer("Mow the lawn", 1);
        //   List<Customer> expectedResult = new List<Customer>{testCustomer};
        //
        //   //save to DB
        //   testCustomer.Save();
        //
        //   //retrive from DB
        //   List<Customer> actualResult = Customer.FindByStylistId(1);
        //
        //   //Assert
        //   CollectionAssert.AreEqual(expectedResult, actualResult);
        // }

       // [TestMethod]
       //  public void Save_DatabaseAssignsIdToObject_Id()
       //  {
       //    //Arrange
       //    Customer testCustomer= new Customer("Mow the lawn", 1);
       //    testCustomer.Save();
       //
       //    //Act
       //    Item savedItem = Item.GetAll()[0];
       //
       //    int result = savedItem.GetId();
       //    int testId = testItem.GetId();
       //
       //    //Assert
       //    Assert.AreEqual(testId, result);
       //  }

       //  [TestMethod]
       //  public void Find_FindsItemInDatabase_Item()
       //  {
       //    //Arrange
       //    Item testItem = new Item("Mow the lawn", 1);
       //    testItem.Save();
       //
       //    //Act
       //    Item foundItem = Item.Find(testItem.GetId());
       //
       //    //Assert
       //    Assert.AreEqual(testItem, foundItem);
       //  }
    }
}
