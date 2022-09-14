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
	//  Nested Queries
	//  Sometime referred to as sub-queries

	//  Simply put:  It is a query within a query.
	//					[query in a query in a query etc]

	//  List all sales support employees showing their
	//  FullName (last, first), Title, Phone #
	//  For each employees, show a lst of their customers
	//   that they support.
	//  FullName (last, first), City, State/Prov.

	//  Thompson, James  Instructor 7801234567  //Employees
	//		Kan, Jerry Edmonton Ab  // Customer
	//		Thom, Sue Devon Ab  // Customer
	//  	Apple Mary Edmonton Ab  // Customer
	//  	Low, Mike Calgary Ab  // Customer

	//  Smith, Bob  Chair 7805551212  //Employees
	//		Can, Mark Edmonton Ab  // Customer
	//		Freddy, Shelly Devon Ab  // Customer
	//  	Micro, Bill Edmonton Ab  // Customer
	//  	Peterson, Cindy Calgary Ab  // Customer
	
	//  There appears to be 2 separate list that needs to be
	//   withon one final dataset/collection
	//	One for the employees
	//	One for the customers
	//  Concerns:  The list are inter mixed!!!
}

// You can define other methods, fields, classes and namespaces here