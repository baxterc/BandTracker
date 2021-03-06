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
        List<Band> allBands = Band.GetAll();
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("allBands", allBands);
        model.Add("venueBands", bandsAtVenue);
        return View["venue.cshtml", model];
      };

      Get["/venues/search"] = _ => {
        string searchString = Request.Query["venue_search"];
        List<Venue> resultVenues = Venue.Search(searchString);
        return View["venue_results.cshtml", resultVenues];
      };

      Get["/venues/add"] = _ => {
        return View["venue_add.cshtml"];
      };

      Post["/venues/add"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue_name"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };

      Get["/venues/update/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_update.cshtml", selectedVenue];
      };

      Patch["/venues/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> allBands = Band.GetAll();
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("allBands", allBands);
        model.Add("venueBands", bandsAtVenue);
        return View["venue.cshtml", model];
      };

      Post["/venues/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Band selectedBand = Band.Find(Request.Form["add_new_band"]);
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.AddBand(selectedBand);
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("allBands", allBands);
        model.Add("venueBands", bandsAtVenue);

        return View["venue.cshtml", model];
      };

      Get["/venues/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_delete_confirm.cshtml", selectedVenue];
      };

      Delete["/venues"] = _ => { // delete a venue
        Venue deletedVenue = Venue.Find(Request.Form["venue_id"]);
        deletedVenue.Delete();
        return View["venue_deleted.cshtml"];
      };

      Delete["/venues/{id}"] = parameters => { //delete a band from a venue
        Dictionary<string,object> model = new Dictionary<string,object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        Band selectedBand = Band.Find(Request.Form["band_id"]);
        selectedVenue.DeleteBand(selectedBand);
        List<Band> bandsAtVenue = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("venue", selectedVenue);
        model.Add("allBands", allBands);
        model.Add("venueBands", bandsAtVenue);

        return View["venue.cshtml", model];
      };


      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };

      Get["/bands/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> venuesPlayed = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venuesPlayed", venuesPlayed);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };

      Get["/bands/search"] = _ => {
        string searchString = Request.Query["band_search"];
        List<Band> resultBands = Band.Search(searchString);
        return View["band_results.cshtml", resultBands];
      };

      Get["/bands/add"] = _ => {
        return View["band_add.cshtml"];
      };

      Post["/bands/add"] = _ => {
        Band newBand = new Band(Request.Form["band_name"]);
        newBand.Save();
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };

      Get["/bands/update/{id}"] = parameters => {
        Band selectedBand = Band.Find(parameters.id);
        return View["band_update.cshtml", selectedBand];
      };

      Patch["/bands/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Band selectedBand = Band.Find(parameters.id);
        selectedBand.Update(Request.Form["band_name"]);
        List<Venue> venuesPlayed = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venuesPlayed", venuesPlayed);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };

      Post["/bands/{id}"] = parameters => {
        Dictionary<string,object> model = new Dictionary<string,object>();
        Band selectedBand = Band.Find(parameters.id);
        Venue selectedVenue = Venue.Find(Request.Form["add_new_venue"]);
        selectedBand.AddVenue(selectedVenue);
        List<Venue> venuesPlayed = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venuesPlayed", venuesPlayed);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };

      Get["/bands/delete/{id}"] = parameters => {
        Band selectedBand = Band.Find(parameters.id);
        return View["band_delete_confirm.cshtml", selectedBand];
      };

      Delete["/bands"] = _ => { // delete a band
        Band deletedBand = Band.Find(Request.Form["band_id"]);
        deletedBand.Delete();
        return View["band_deleted.cshtml"];
      };

      Delete["/bands/{id}"] = parameters => { //delete a venue from a band
        Dictionary<string,object> model = new Dictionary<string,object>();
        Venue selectedVenue = Venue.Find(Request.Form["venue_id"]);
        Band selectedBand = Band.Find(parameters.id);
        selectedBand.DeleteVenue(selectedVenue);
        List<Venue> venuesPlayed = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("venuesPlayed", venuesPlayed);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };
    }
  }
}
