# BandTracker

#### Band / Venue Tracker for Epicodus, 7/22/2016

#### By Charlie Baxter

## Description
This site uses a database to show what bands have played at what venue.  Since a band can play at many venues and a venue can have many bands associated with it, the database uses a join table to store these many-to-many relations.  Join statements allow the user to see what bands have played at what venue, and what venues a given band has played at.  These relationships can be entered in by the users.  Individual bands and venues themselves can be created, read, updated and deleted.  The main page includes a search field for bands and venues in addition to pages which simply list all bands and all venues.

## Setup
* Clone this repository
* Install a C# framework such as Mono, which is available from [www.mono-project.com](http://www.mono-project.com/)
* Install Microsoft's SQL Server Management Studio, available from [this link](https://msdn.microsoft.com/en-us/library/mt238290.aspx).
* Navigate to the project folder using your terminal/shell and run the command "dnu restore" to load dependencies.
* Open the SQL Server Management Studio and open the file "band_tracker.sql" in the project folder.
* Click "Execute", located next to a red exclamation point.
* Do the same for the file "band_tracker_test.sql" to be able to run XUnit tests.
* Back in your command prompt, type the command "dnx kestrel" to load the project.  The following message should appear:   
Hosting environment: Production   
Now listening on: http://localhost:5004   
Application started. Press Ctrl+C to shut down.
* In your web browser, type in http://localhost:5004
* XUnit testing can be done by typing "dnx test" in the the command prompt while in the directory you set up for this project.
* Once finished, type control-C in your terminal or shell.

## Technologies Used
* C#
* SQL
* Nancy
* Razor
* XUnit
* HTML

## Known Bugs
* None

## Features to add:
* Automatically convert user input for venues and bands to grammatically correct title case (some of the code for this is commented out on lines 77 - 107 in Venue.cs)
* Create a Concert object that includes a date property rather than just joining the bands and venues together.
* Needs styling to be easier to look at!

## Contact & Support
If you run into any issues with this page, have any questions, ideas, or concerns, feel free to email me at charlie.r.baxter@gmail.com.

## Legal
Copyright (c) 2016 Charlie Baxter.  This software is licensed under the MIT License.
