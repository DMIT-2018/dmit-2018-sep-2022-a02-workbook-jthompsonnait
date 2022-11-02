<Query Kind="Program" />

void Main()
{
	//	Extension Methods
	//	C# is referred to as an extensive language
	//	This basically mean you can add your own personal "customization" to the C# language.
	//	This "customization" extends thecapability of C#, such as 
	//		datatypes (classes), methods (extension methods), ...
	//	These capabilities are only available to a project of they are included.

	//	In this example, we will "extend" the functionality of the string class:
	//	Create an instance of the string class call speak
	string speak = "hello world";
	Console.WriteLine(speak);

	//	Classes have properties and methods
	Console.WriteLine(speak.Length);  //  Length is a property of the string class
	Console.WriteLine(speak.Substring(3));  //	Substring is a method of the string class

	//	What if I would like my string to "quack"
	Console.WriteLine(speak.Quack());  //	Quack() is NOT part of the C# string class.
	Console.WriteLine(speak);

	//	What if I have arguments to send to my new extension method
	Console.WriteLine(speak.Quack(20));  //  Quack(argument) method does not exist, you need to create an overload extension method

}

//	You can define other methods, fields, classes and namespaces here.

//	Create extension method(s) for the following C# vlass: string

//	Step 1:	Make a static class to hold the extension method(s)
//				this class can be called anything you like

public static class MyExtensionStringMethod
{
	//	Step 2:	Add your public static string method(s) to this class
	public static string Quack(this string self)
	{
		//	The return datatype from this method will be a string
		//	This is the datatype of the class instance we are extending
		//
		//	NOTE:  You do NOT necessarily need to return a value; that is the rdt could be void
		//
		//	The 1st parameter(the error msg does ute the word argument) of the method
		//		signature identifies the class extension method is assiciated with, string
		//
		//	The paramter requires the following syntax ->	this datatype parameter name
		//	The contents of the paramter will be the contents of the calling instance (er speak)

		//	Your logic for the method
		string result = "quack " + self + " quack";
		return result;
	}

	public static string Quack(this string self, int quackTimes)
	{
		//	Your logic for the method
		string quacks = string.Empty;
		for (int i = 0; i < quackTimes; i++)
		{
			quacks += " ..quacks.. ";
		}
		return $"{self} ({quacks})";
	}
}













// You can define other methods, fields, classes and namespaces here