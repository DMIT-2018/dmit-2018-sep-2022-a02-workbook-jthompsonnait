<Query Kind="Statements">
  <Connection>
    <ID>8f6c621e-f256-443a-94b4-98581ddcde18</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook2018</Database>
  </Connection>
</Query>

//  Unions()
//  Since Linq is converted into SQL, one would expect that the 
//	 SQL Union rule mustbe the same in Linq
//  Purpose:  Concatenating multiple results into one collection
//  Synatx (queryA).Union(queryB).Union(query...)
//  Rules:
//	number of columns are the same
//  column datatypes must match
//  ordering shoud be done as a method after the last union

//  List the stats (count, cost, average track length) of albums on tracks
//  NOTE:  For cost and average, one will need an instance in tracks to do 
//			the aggregation
//  Concern:  What if the album does not have any recored tracks.
//  Albums with no tracks on the database will have a count however,
//		cost and average length wil be 0 (no instances to aggregate)

//	Solution:  Create two queries, ine handling no tracks and one handling
//				albums with tracks.  The we UNION the two results.

//  NOTE:  If you hard coding numeric fields, the query with the hard code
//				values MUST be the first query

Albums
	.Where(x => x.Tracks.Count() == 0)
	.Select(x => new
	{
		Title = x.Title,
		TotalTrack = x.Tracks.Count(),  // 0
		TotalCost = 0.00m,
		AverageLength = 0.0
	})
.Union(Albums
			.Where(x => x.Tracks.Count() > 0)
			.Select(x => new
			{
				Title = x.Title,
				TotalTrack = x.Tracks.Count(),  // 0
				TotalCost = x.Tracks.Sum(t => t.UnitPrice),
				AverageLength = x.Tracks.Average(t => t.Milliseconds /1000)
			}))
			.OrderBy(t => t.TotalTrack)
.Dump()
;
