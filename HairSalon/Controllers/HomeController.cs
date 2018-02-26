using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/StylistList")]
    public ActionResult StylistList()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/InsertStylist")]
    public ActionResult InsertStylist()
    {
      return View("AddStylists");
    }

    [HttpPost("/AddStylists")]
    public ActionResult AddStylists()
    {
      string stylistName = Request.Form[("stylist")];
      Stylist stylist = new Stylist(stylistName);
      Console.WriteLine("New stylist name is: " + stylist.GetName());
      stylist.Save(); //save to DB
      List<Stylist> allStylists = Stylist.GetAll(); //retrieve stylists from DB again
      return View("StylistList", allStylists);
    }

    [HttpGet("/AddCustomers")]
    public ActionResult AddCustomers()
    {
      return View("AddCustomers");
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

    [HttpGet("/StylistDetails/{id}")]
    public ActionResult StylistDetails(int id)
    {
      Stylist stylist = Stylist.Find(id);
      List<Customer> customers = Customer.FindByStylistId(stylist.GetId());
      stylist.SetCustomers(customers);
      return View("StylistDetails", stylist);
    }

    // [HttpPost("/Result")]
    // public ActionResult Result()
    // {
    //   string word = Request.Form[("keyWord")];
    //   string sentence = Request.Form[("sentence")];
    //   WordCheckerVariable wordCheckerVariable = new WordCheckerVariable(word, sentence);
    //   return View("Result", wordCheckerVariable);
    // }
  }
}
