using Microsoft.AspNetCore.Mvc;
// using HairSalon.Models;
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
      return View();
    }

    [HttpGet("/AddStylists")]
    public ActionResult AddStylists()
    {
      return View("AddStylists");
    }

    [HttpGet("/AddCustomers")]
    public ActionResult AddCustomers()
    {
      return View("AddCustomers");
    }

    [HttpGet("/StylistDetails")]
    public ActionResult StylistDetails()
    {
      return View("StylistDetails");
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
