using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
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
      stylist.Save(); //save to DB
      List<Stylist> allStylists = Stylist.GetAll(); //retrieve stylists from DB again
      return View("StylistList", allStylists);
    }

    [HttpGet("/StylistDetails/{id}")]
    public ActionResult StylistDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Stylist stylist = Stylist.Find(id);

      List<Customer> customers = Customer.FindByStylistId(stylist.GetId());

      model.Add("allCustomers", customers);
      List<Specialty> stylistSpecialties = stylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      //
      model.Add("selectedStylist", stylist);
      model.Add("stylistSpecialties", stylistSpecialties);
      model.Add("allSpecialties", allSpecialties);

      return View("StylistDetails", model);
    }

    [HttpGet("/UpdateStylist/{id}")]
    public ActionResult UpdateStylist(int id)
    {
      Stylist stylist = Stylist.Find(id);
      return View("UpdateStylist", stylist);
    }

    [HttpPost("/UpdateStylist/{id}")]
    public ActionResult UpdateStylistPost(int id)
    {
      string stylistNewName = Request.Form[("new-name")];
      Stylist stylist = Stylist.Find(id);
      stylist.UpdateName(stylistNewName);
      List<Stylist> allStylists = Stylist.GetAll();
      return View("StylistList", allStylists);
    }

    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult Addspecialty(int stylistId)
    {
        Stylist stylist = Stylist.Find(stylistId);
        Specialty specialty = Specialty.Find(Int32.Parse(Request.Form["specilty-id"]));
        stylist.Addspecialty(specialty);
        return RedirectToAction("StylistDetails",  new { id = stylistId });
    }

    [HttpGet("/DeleteStylist/{id}")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist stylist = Stylist.Find(id);
      stylist.Delete();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("StylistList", allStylists);
    }

    [HttpGet("/DeleteAllStylists")]
    public ActionResult DeleteAllStylists()
    {
      Stylist.DeleteAll();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("StylistList", allStylists);
    }
  }
}
