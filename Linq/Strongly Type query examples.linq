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
	//  Strongly type query set

	//  Anonymous dataset from a query does NOT have a permanent 
	//   specified class definition (dynamic).
	//  Strongly type query dataset HAS a permanent class definition
	//   in its code.

	//  Find all songs that contains a partial string of a track name.
	//  Display the Album, Song (Track Name), Artist.

	//  Image your solutions need to mimic a webapp query to some 
	//   BLL service.

	string partialSongName = "dance";

	List<Song> results = SongByPartialName(partialSongName);
	results.Dump();

}

//  You can...
//  developer defined data type.

public class Song
{
	public string AlbumTitle { get; set; }
	public string SongTitle { get; set; }
	public string Artist { get; set; }
}

//  to change anonymous dataset to a stringly type dataset
//   add the datatype (song_ to the instance creation operator (new)
List<Song> SongByPartialName(string partialSongName)
{
	var songCollection =
	Tracks
		  .Where(x => x.Name.Contains(partialSongName))
		  .Select(x => new Song
		  {
		  	AlbumTitle = x.Album.Title, //  We are using the navigation property
			  SongTitle = x.Name,
			  Artist = x.Album.Artist.Name  //  We are using the navigation property
		  });
	return songCollection.ToList();
}










