<Query Kind="Expression">
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

//  Using the Ternary Operator

//  Condition(s) ? True value : false value
//  Both the true value and false value MUST resolve to a SINGLE piece
//  of data (a single value).
//	Compare to the if statement.

//  if {condtion(s))}
//  True path (?)
//  False path (:)

// bool IsOldEnoughToDrink = (age >=18) ? true : false;

//  Just like the conditional statement which can have nested logic,
//	 the true value and false value can have nested ternary operations
//	 as long as the final results resolved to a SINGLE value.

//  List all albums by release label.  Any albums with no label
//	 should be indicated as Unknown.
//	Diplay Title, Label, Artist Name and Release Year.

//	Understand problem.
//		Collection: Albums
//		Select data set:  anonymous data set.
//		Ordering:  Release Label
//		Label:	Either Unknown or release label.

//	Design
Albums
	.Select(x => new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear
	})
		.OrderBy(x => x.Label)

//  HOMEWORK!!!!!!!

//  List all albums showing the Title, Artist Name, Year and
//  decade of release.
//  (Oldies, 70s, 80s, 90s or Modern)
//  Order by decades.

Albums
	.Select(x => new
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
		Decade = x.ReleaseYear < 1970? "Oldies" :
				x.ReleaseYear < 1980 ? "70s":
				x.ReleaseYear < 1990 ? "80s" :
				x.ReleaseYear < 2000 ? "90s" : "Modern"
	}).OrderBy(x => x.Year)
	.ThenBy(x => x.Decade)
	.Dump()

//  if < 1970
//  Oldies
// else
// (< 1980
//  70s
// else
//  (< 1990
//  80s
//  else
//  (< 2000
//  90s
//  else
//  Modern)))

//  sum of all cost < selling price ? profit : loss
//  Show item #, profit/loss
