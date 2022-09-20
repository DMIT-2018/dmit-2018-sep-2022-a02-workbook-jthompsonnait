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
//	b)	By a set of properties (anonymous dataset key) groupname.Key,PropertyName
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







