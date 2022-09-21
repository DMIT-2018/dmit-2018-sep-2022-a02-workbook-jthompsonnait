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

//  Grouping

//  When you create a group, it builds two (2) components
//		a)  Key component (group by criteria values)
//			 reference this component using the group name.Key[.Property]
//   		 (propertu, column, attribute and values)
//		b)	The data of the group (instances of the original collection) -> mini group.

//  Ways to group
//  a)  By a single property (column, field, attribute, value)  groupname.Key
//	b)	By a set of properties (anonymous key set) groupname.Key,PropertyName
//	c) By using an entity (x.nav property).  ***  Try to avoid ***

//  Concept Processing
//  We start with "pile" of data (original collection)
//  Specify the grouping criteria (value(s))
//	Result of the group operation will be to "place the data into smaller piles"
//	 (mini collections).  The piles are dependent on the grouping criterial value(s)
//  The grouping criterial (property (ies), column, etc) become the key.
//  The individual instances are "the data in the smaller piles"
//  The entire individual instances of the original collection is placed in the smaller piles

//  Manipulation of each of the "smaller piles" is now possible with your Linq commands.

//  Grouping is differnt than Ordering
//  Ordering is the re-sequencing of a collections for display.
//  Grouping re-organizes a collection into separate, usually smaller collections for processing.

Albums
	.OrderBy( x=> x.ReleaseYear);


//  Display albums grouped by ReleaseYear.
//  NOT one display of albums but displays of album for a specified value (ReleaseYear)
//  Explicit request to breakup the display into desired "piles" (collections)

Albums
	.GroupBy(x => x.ReleaseYear)

//  Query Syntax
from a in Albums
group a by a.ReleaseYear

//  processing on the created group of the Groupby method
Albums
	.GroupBy(x => x.ReleaseYear)  //  This method returns a collection of "mini collections"
	.Select(eachgPile => new
	{
		Year = eachgPile.Key,
		NumberOfAlbums = eachgPile.Count()  //  processing of "mini-collection" data
	});  //  The Select is procesing each mini collections one at a time

//  Query Syntax
//  Using this syntax, you MUST specify the name you wish to use to refer to the
//   grouped (mini collections) collections
//  After coding your group command, you MUST (are restricted to) use the name
//   you have given your group collection.

from a in Albums
//orderby a.ReleaseYear:  Would be valid because "a" is in context
//orderby eachgPile.Key:  Would not be valid because grouping not specified yet.  
group a by a.ReleaseYear into eachgPile
//  orderby a.ReleaseYear:  Would be invalid because "a" is out of context, the group name is seachgPile
orderby eachgPile.Key//:  Would be valid because "eachgPIle" is currently in context and Has your year.
select new {
	Year = eachgPile.Key, //  Key Component
	NumberofAlbums = eachgPile.Count()  //  processing of "mini-collection" data
};

//  Use a mltiple set of criteria (properties) to for the group
//   also include a nested query to report on the "mini-collection" (smaller piles)
//   of the grouped data.

//  Display albums group by release label, release year.
//  Display the release year and the numbers of albums.  
//  List only the years with 2 or mor albums release
//  For each album, display the title, year of release and count of tracks,.

//  Original collectin (large pile of data:  Albums
//  Filtering cannot be decided until the groups are created.
//  Grouping:  ReleaseLabel, ReleaseYear (anonymous key set: object)
//  Now filtering can be done on the group:  group.Count >= 2
//  Report the year and number of albums
//  Nested query to report details per album: Title, Year, # of tracks.

Albums
	.GroupBy(a => new { a.ReleaseLabel, a.ReleaseYear})//  Creating anonymous key set
	.Where(eachgPile => eachgPile.Count() >=2)
	.OrderBy(eachgPile => eachgPile.Key.ReleaseLabel)
	.Select(eachgPile => new 
	{
		Label = eachgPile.Key.ReleaseLabel,
		Year = eachgPile.Key.ReleaseYear,
		NumberOfAlbums = eachgPile.Count(),
		AlbumsGroupItem = eachgPile//  small pile (mini collection)
							.Select(eachgPileInstance => new
							{
								Title = eachgPileInstance.Title,
								Year = eachgPileInstance.ReleaseYear,
								NumberOfTracks = eachgPileInstance.Tracks.Count()//  Broken
							})
	})



