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
	//  Conversion .ToList()
	// Albums.Select(a =>a).ToList().Dump();
	
	//  Display all albums and their tracks.
	//  Display the album title, artist and album tracks
	//  For each track, show the song name and playtime in seconds
	//  Show only albums with 25 or more tracks.
	
	List<AlbumTracks> albumList = Albums
					.Where(a => a.Tracks.Count() >= 25)
					.Select(a => new AlbumTracks
					{
						Title = a.Title,
						Artist = a.Artist.Name,
						Songs = a.Tracks.Select(tr => new SongItem
						{
							Song = tr.Name,
							PlayTime = tr.Milliseconds / 1000.0
						}).ToList()
					}).ToList()
					//.Dump()
					;
//  Typically if the albumlist was a var variable in your BLL method
//   AND the method return datatype was a List<T>, one could on the
//   return statement do: return albumnList.ToList();  (Saw in your 1517 course)

//  Using FirstOrDefault()
//  great for lookups that you expect 0, 1, or more instances retrun
//  FInd the first ablum of Deep Purple

string artistParam = "Deep Purple";
var resultsFOD = Albums
					.Where(a => a.Artist.Name.Equals(artistParam))
					.Select( a=> a)
					.OrderBy(a => a.ReleaseYear)
					.FirstOrDefault()
					//.Dump()
					;
//if (resultsFOD != null)
//resultsFOD.Dump();
//else
//Console.WriteLine($"No albums found for artist {artistParam}");

//  Using SingleOrDeafault()
//  Expecting at most a single instance being return

//  Find the album by albumID
int albumID = 1;
var resultSOD = Albums
					.Where(a => a.AlbumId == albumID)
					.Select(a => a)
					.SingleOrDefault()
					;

	//if (resultSOD != null)
	//	resultSOD.Dump();
	//else
	//	Console.WriteLine($"No albums found for id of {albumID}");
	
//  Distinct()
//  Removes duplicated reported lines

//  Obtain a list of customer countries.
var resultDistinct = Customers
						.OrderBy( c=> c.Country)
						.Select(c => c.Country)
						.Distinct()
						.Dump()
						;
}













// You can define other methods, fields, classes and namespaces here
public class SongItem
{
	public string Song {get; set;}
	public double PlayTime {get; set;}
}

public class AlbumTracks
{
	public string Title {get; set;}
	public string Artist {get; set;}
	public List<SongItem> Songs {get; set;}
}