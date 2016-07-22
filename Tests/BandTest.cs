using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Band.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfBandsAreTheSame()
    {
      Band firstBand = new Band("Wizard People");
      Band secondBand = new Band("Wizard People");

      Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Test_Save_SavesBandToDatabase()
    {
      Band testBand = new Band("Wizard People");

      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      Band testBand = new Band("Wizard People");
      testBand.Save();

      Band foundBand = Band.Find(testBand.GetId());

      Assert.Equal(testBand, foundBand);
    }

    [Fact]
    public void Test_Update_UpdatesBandInDatabase()
    {
      Band testBand = new Band("Wizard People");
      testBand.Save();
      string newBandName = "Dear Readers";

      testBand.Update(newBandName);

      Assert.Equal(newBandName, testBand.GetName());
    }

    [Fact]
    public void Test_Delete_RemovesBandFromDatabase()
    {
      Band testBand = new Band("Wizard People");
      testBand.Save();
      Band testBand2 = new Band("Dear Readers");
      testBand2.Save();
      List<Band> testBands = new List<Band>{testBand, testBand2};

      testBand.Delete();
      testBands.Remove(testBand);
      List<Band> resultBands = Band.GetAll();

      Assert.Equal(testBands, resultBands);
    }

    [Fact]
    public void Test_GetVenues_GetsNoVenuesFromBandWithNoVenues()
    {
      Band testBand = new Band("Wizard People");

      List<Venue> resultVenues = testBand.GetVenues();

      Assert.Equal(0, resultVenues.Count);
    }

    [Fact]
    public void Test_AddVenue_AddsVenueToBand()
    {
      Band testBand = new Band("Wizard People");
      testBand.Save();
      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();
      List<Venue> testVenues = new List<Venue>{};

      testBand.AddVenue(testVenue);
      testVenues.Add(testVenue);
      List<Venue> resultVenues = testBand.GetVenues();

      Assert.Equal(testVenues, resultVenues);
    }
    [Fact]
    public void Test_DeleteVenue_DeletesVenueFromBand()
    {
      List<Venue> testVenues = new List<Venue>{};
      Band testBand = new Band("Wizard People");
      testBand.Save();

      Venue testVenue = new Venue("Studio 54");
      testVenue.Save();
      testVenues.Add(testVenue);
      testBand.AddVenue(testVenue);

      Venue testVenue2 = new Venue("The China Club");
      testVenue2.Save();
      testVenues.Add(testVenue2);
      testBand.AddVenue(testVenue2);

      testBand.DeleteVenue(testVenue);
      testVenues.Remove(testVenue);
      List<Venue> resultVenues = testBand.GetVenues();

      Assert.Equal(testVenues, resultVenues);
    }
  }
}
