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
	//  Program IDE
	//  You can have multiple quesries written in this IDE environment
	//  This environment work "like" a console application 

	//  This allows one to pre-test complete components that can be 
	//  moved directly into your backend application (class library)

	//IMPORTANT:  Query Syntax
	//  Queries in this environnment MUST be wrtten using the
	//  C# langauge grammer for a statement.  This mean that
	//  each statement must end in a semi-colon.
	//  Result MUST be placed in a receiving variable.
	//  To display the results, use the LinqPad method .Dump();

	//  Query Syntax 
	//int paramYear = 1990;  //  image this is a parameter on a method signature.
	//var selectQ = from x in Albums
	//			  where x.ReleaseYear == paramYear  //  we need two equals signs for a compare (C# grammar/syntax) 
	//			  select x;
	//selectQ.Dump();  //  image that this is the return statement in a method.

	//  image this is a method in your BLL server.
	List<Albums> GetAllAlbumQ(int paramYear)
	{
		var resultQ = from x in Albums
					  where x.ReleaseYear == paramYear  //  we need two equals signs for a compare (C# grammar/syntax) 
					  select x;
		return resultQ.ToList();
	}

	//List<Albums> resultSQ = GetAllAlbumQ(2000);
	//resultSQ.Dump();



	// Method Syntax
	List<Albums> GetAllAlbumM(int paramYear)
	{
		var selectM = Albums
		.Where(x => x.ReleaseYear == paramYear)
		.Select(x => x);
		return selectM.ToList();
	}
	List<Albums> resultMQ = GetAllAlbumQ(2000);
	resultMQ.Dump();

}
// You can define other methods, fields, classes and namespaces here