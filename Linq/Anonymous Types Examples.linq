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

// Using Naviagational Properties and Anonymouse data set (Collections)

//  Reference: Student Note/Demo/ERestaurants/Linq - Query and Method
//    Syntax/C# Expression

//  Method Syntax
//  Find all albums release in the 90's (1990 - 2000)
//  Order the albums by ascending years and then alphabetically
//  by album title
//  Diplay the year, title, artist name and release date

//  Concerns:	a) not all proerties of the album are to be display
//				b) the order of the properties are to be display
//					in a different sequence than the definiton of
//					the properties of the entity
//				c) The artist name is not on the album table but
//					onthe artist table

//	Solution:	Use an anonymous data set.
//	The anonymouse instance is defined within the select by
//	 in the declared fields (properties)
//	The order of the fields on the instance is defined during the
//	 specification of your code.  (How you list the fields is how
//	 they will be display).

Albums

	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.Select(x => new
	{
		Year = x.ReleaseYear,
		Title = x.Title,
		Artist = x.Artist.Name,
		Label = x.ReleaseLabel
	})
	.OrderBy(x => x.Year)
	.ThenBy(x => x.Title)







