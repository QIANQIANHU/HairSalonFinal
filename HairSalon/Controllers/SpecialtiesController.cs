using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/SpecialtyList")]
    public ActionResult SpecialtyList()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/InsertSpecialty")]
    public ActionResult InsertSpecialty()
    {
      return View("AddSpecialties");
    }

    [HttpPost("/AddSpecialty")]
    public ActionResult AddSpecialty()
    {
      string specialtyName = Request.Form[("specialty")];
      Specialty specialty = new Specialty(specialtyName);
      specialty.Save(); //save to DB
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View("SpecialtyList", allSpecialties);
    }

    [HttpGet("/SpecialtyDetails/{id}")]
    public ActionResult SpecialtyDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Specialty specialty = Specialty.Find(id);
      List<Stylist> specialtyStylists = specialty.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("selectedSpeciality", specialty);
      model.Add("specialtyStylists", specialtyStylists);
      model.Add("allStylists", allStylists);

      return View("SpecialtyDetails", model);
    }

    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult Addspecialty(int specialtyId)
    {
        Specialty specialty = Specialty.Find(specialtyId);
        Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylsit-id"]));
        specialty.AddStylist(stylist);
        return RedirectToAction("SpecialtyDetails",  new { id = specialtyId });
    }

    [HttpGet("/DeleteAllSpecialties")]
    public ActionResult DeleteAllSpecialties()
    {
      Specialty.DeleteAll();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View("SpecialtyList", allSpecialties);
    }
  }
}
