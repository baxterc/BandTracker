using System.Collections.Generic;
using System;
using Nancy;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };

      Get["/venues/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("bands", bandsAtVenue);
        return View["venue.cshtml", model];
      };

      Get["/venues/add"] = _ => {
        return View["venue_add.cshtml"];
      };

      Post["/venues/add"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue_name"]);
        newVenue.Save();
        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Band> bandsAtVenue = newVenue.GetBands();
        model.Add("venue", newVenue);
        model.Add("bands", bandsAtVenue);
        return View["venue.cshtml", newVenue];
      };

      Get["/venues/update/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_update.cshtml", selectedVenue];
      };

      Patch["/venues/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Update(Request.Form["venue_name"]);
        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("bands", bandsAtVenue);
        return View["venue.cshtml", model];
      };

      Get["/venues/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_delete_confirm.cshtml", selectedVenue];
      };

      Delete["/venues"] = _ => {
        Venue deletedVenue = Venue.Find(Request.Form["venue_id"]);
        deletedVenue.Delete();
        return View["venue_deleted.cshtml"];
      };




      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };

      Get["/bands/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> venuesPlayed = selectedBand.GetVenues();
        model.Add("band", selectedBand);
        model.Add("venues", venuesPlayed);
        return View["band.cshtml", model];
      };

      Get["/bands/add"] = _ => {
        return View["band_add.cshtml"];
      };

      Post["/bands/add"] = _ => {
        Band newBand = new Band(Request.Form["band_name"]);
        newBand.Save();

        Dictionary<string,object> model = new Dictionary<string,object>();
        List<Venue> venuesPlayed = newBand.GetVenues();
        model.Add("band", newBand);
        model.Add("venues", venuesPlayed);
        return View["band.cshtml", model];
      };
    }
  }
}
