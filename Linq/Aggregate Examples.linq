<Query Kind="Program">
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

void Main()
{
	//  Aggregates
	//	.Count()	COunt the number of instances in a collection
	//  .Sum(x => ...)	Sum(totals) a numeric field (numeric expression) in a collection
	//  .Min(x => ...)  Find the minimum value of a collections of vales for a field
	//  .Max(x => ...)  Find the maximum value of a collections of vales for a field
	//  .Average(x => ...)  Find the average value of a collections of vales for a field
	
	//  IMPORTANT!!!
	//  Aggregates work ONLY on a collection of values for a particular field (expressions)
	//  Aggreagate DO NOT WORK on a single row (not the same as a collection with one row)
	
	//  Query Syntaxx
	//  (From ...
	//  ...
	//  Select expression).aggreate();
	
	//  Method Syntax
	//  collection.aggregate(x => expression) Sum, Min, Max, Average
	//  NOTE:  Count() does NOT use an expression
	//  collection.Select(x => expression).Aggreate();
	
	//  You can use multiple aggregates on a single column.
	//  collection.sum(x => expression).Min(x => expression)
	
	//  Find the average play time (length) of tracks in our music collections
	
	//  Thought Process:
	//  average is an aggregate.
	//  What is the collection:  a track is a member of the tracks table.
	//  What the expression:  Field is in milliseconds represending the tracks
	//   length (play time)
	
	//  Query
	(from x in Tracks
	select x.Milliseconds).Average().Dump();
	
	//  Method
	Tracks.Average(x => x.Milliseconds).Dump();
	Tracks.Select(x => x.Milliseconds).Average();
	
	//  HOMEWORK!!!!!!!!!!!!!!!!!!!!!!
	//  List all albums of the 60s
	//  Album Title, Artist, various aggregates for the albums containing tracks
	
	// For each album, show number of tracks, the longest playing track,
	//  the shortest playing track, the total price of all tracks and the 
	//  average playing length of the album tracks.
	
	//  Hint:  Albums has two navigation properties
	//		   Artist "points to" the single parent record.
	//         Tracks "points to" the collection of child records {tracks]
	//           of the albums.
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}

