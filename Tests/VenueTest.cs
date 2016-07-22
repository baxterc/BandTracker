using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfVenuesAreTheSame()
    {
      Venue firstVenue = new Venue("Studio 54");
      Venue secondVenue = new Venue("Studio 54");

      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      Venue testVenue = new Venue("Studio 54");

      testVenue.Save();
      List<Venue> testVenues = new List<Venue>{testVenue};
      List<Venue> resultVenues = Venue.GetAll();

      Assert.Equal(testVenues, resultVenues);
    }

    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();

      Venue foundVenue = Venue.Find(testVenue.GetId());

      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();
      string newVenueName = "The China Club";

      testVenue.Update(newVenueName);
      string resultName = testVenue.GetName();

      Assert.Equal(newVenueName, resultName);
    }

    [Fact]
    public void Test_Delete_DeletesVenueFromDatabase()
    {
      Venue testVenue = new Venue("Studio 54");
      Venue testVenue2 = new Venue("The China Club");
      testVenue.Save();
      testVenue2.Save();
      List<Venue> testVenues = new List<Venue>{testVenue, testVenue2};

      testVenue.Delete();
      testVenues.Remove(testVenue);
      List<Venue> resultVenues = Venue.GetAll();

      Assert.Equal(testVenues, resultVenues);
    }

    [Fact]
    public void Test_GetBands_GetsNoBandsFromVenueWithNoBands()
    {
      Venue testVenue = new Venue("Studio 54");

      List<Band>resultBands = testVenue.GetBands();

      Assert.Equal(0, resultBands.Count);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();
      Band testBand = new Band("Kool and the Gang");
      testBand.Save();

      testVenue.AddBand(testBand);
      List<Band>testBands = new List<Band>{testBand};
      List<Band>resultBands = testVenue.GetBands();

      Assert.Equal(testBands, resultBands);
    }

    [Fact]
    public void Test_DeleteBand_DeletesBandFromVenue()
    {
      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();
      Band testBand = new Band("Kool and the Gang");
      testBand.Save();
      testVenue.AddBand(testBand);
      Band testBand2 = new Band ("The Bee Gees");
      testBand2.Save();
      testVenue.AddBand(testBand2);

      testVenue.DeleteBand(testBand);
      List<Band>testBands = new List<Band>{testBand2};
      List<Band>resultBands = testVenue.GetBands();

      Assert.Equal(testBands, resultBands);
    }
  }
}
