using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
      public StylistTests()
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
     public void GetAll_StylistsEmptyAtFirst_0()
     {
       //Arrange, Act
       int result = Stylist.GetAll().Count; // read from DB, expect nothing returned

       //Assert
       Assert.AreEqual(0, result);
     }

      [TestMethod]
      public void Equals_ReturnsTrueForSameName_Stylist()
      {
        //Arrange, Act
        Stylist firstStylist = new Stylist("Household chores");
        Stylist secondStylist = new Stylist("Household chores");

        //Assert
        Assert.AreEqual(firstStylist, secondStylist);
      }

      [TestMethod]
      public void Save_SavesStylistToDatabase_StylistList()
      {
        //Arrange
        Stylist testStylist = new Stylist("Household chores");
        List<Stylist> expectedList = new List<Stylist>{testStylist};

        //Save to DB
        testStylist.Save();
        //Retrieve from DB
        List<Stylist> actualResult = Stylist.GetAll();

        //Assert
        CollectionAssert.AreEqual(actualResult, expectedList);
      }

    // Todo: Enable these 2 tests later
    //  [TestMethod]
    //  public void Save_DatabaseAssignsIdToCategory_Id()
    //  {
    //    //Arrange
    //    Category testCategory = new Category("Household chores");
    //    testCategory.Save();
    //
    //    //Act
    //    Category savedCategory = Category.GetAll()[0];
    //
    //    int result = savedCategory.GetId();
    //    int testId = testCategory.GetId();
    //
    //    //Assert
    //    Assert.AreEqual(testId, result);
    // }
    //
    //
    // [TestMethod]
    // public void Find_FindsCategoryInDatabase_Category()
    // {
    //   //Arrange
    //   Category testCategory = new Category("Household chores");
    //   testCategory.Save();
    //
    //   //Act
    //   Category foundCategory = Category.Find(testCategory.GetId());
    //
    //   //Assert
    //   Assert.AreEqual(testCategory, foundCategory);
    // }

  }
}
