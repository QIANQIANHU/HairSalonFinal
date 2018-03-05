using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class CustomersController : Controller
  {
    [HttpGet("/AddCustomer")]
    public ActionResult AddCustomers()
    {
      return View("AddCustomers");
    }

    [HttpGet("/CustomerList")]
    public ActionResult CustomerList()
    {
      List<Customer> allCustomers = Customer.GetAll();
      return View(allCustomers);
    }

    [HttpPost("/AddCustomers")]
    public ActionResult CustomerAddedSuccess()
    {
      string customerName = Request.Form[("customer")];
      string stylistName = Request.Form[("assignedStylist")];
      Stylist foundStylist = Stylist.FindByStylistName(stylistName); // find a stylist by its stylist's name

      if (foundStylist.GetId() == 0) {
        return View("CustomerAddedFail");
      }

      Customer customer = new Customer(customerName, foundStylist.GetId()); // construct a new customer instance
      customer.Save(); // persist to DB
      return View("CustomerAddedSuccess", customer);
    }

    [HttpGet("/UpdateCustomer/{id}")]
    public ActionResult UpdateCustomer(int id)
    {
      Customer customer = Customer.Find(id);
      return View("UpdateCustomer", customer);
    }

    [HttpPost("/UpdateCustomer/{id}")]
    public ActionResult UpdateCustomerPost(int id)
    {
      string customerNewName = Request.Form[("new-name")];
      Customer customer = Customer.Find(id);
      customer.UpdateName(customerNewName);
      List<Customer> allCustomers = Customer.GetAll();
      return View("CustomerList", allCustomers);
    }

    [HttpGet("/DeleteCustomer/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
      Customer customer = Customer.Find(id);
      customer.Delete();
      List<Customer> allCustomers = Customer.GetAll();
      return View("CustomerList", allCustomers);
    }

    [HttpGet("/DeleteAllCustomers")]
    public ActionResult DeleteAllStylists()
    {
      Customer.DeleteAll();
      List<Customer> allCustomers = Customer.GetAll();
      return View("CustomerList", allCustomers);
    }
  }
}
