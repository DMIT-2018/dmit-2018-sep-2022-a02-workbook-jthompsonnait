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

//  Where Clause

//  Filter clause
//  the conditions are setup as you would in C#
//  Beware that Linqpad may NOT like some C# syntax (DateTime)
//  Beware that Linq is converted to SQL which might not
//  like certains C# synatax beause it could not be converted/

//  Syntax
//  Query
//  Where condition [logical operator (and/or) condition 2 ...]
//  Method
//  Notice that the method syntax make use of the Lambda expressions.
//  .Where(Lambda expression)
//  .Where(x => condition [logical operator (and/or) condition 2...]

//  query 
from x in Albums
where x.ReleaseYear == 2000
select x


// Method
Albums
	.Where(x => x.ReleaseYear == 2000)
	.Select(x => x);

//  Find all albums released in the 90's (1990 - 1999)
//  Display all of the albums records

Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.Select(x => x)

// Find all of the albums for the artist Queen
Albums
	.Where(x => x.Artist.Name.Equals("Queen"))  //  Artist.Name == "Queen"
	.Select(x => x)

//Concern:  The artist name is in another table
//          In SQL, your select query would need a n inner join.
//          In Linq, you DO NOT want to specify your join unless absolutely
//			necessary.
//			Instead use the "navigational properties" of your entity to 
//			generate the relationship

//  .Equals() is an exact match of a string.  You could also use the "=="
//		in SQL = or like "String"
//	.Contains() is a partial string match.
//		in Sql like "%string%"
//	For numerics use your relative operators (==, >, <, <=, >=, !=)

//  Find all albums where the producer (release label) is unknown (Null)
Albums
	.Where(x => x.ReleaseLabel == null)  //  Artist.Name == "Queen"
	.Select(x => x)

