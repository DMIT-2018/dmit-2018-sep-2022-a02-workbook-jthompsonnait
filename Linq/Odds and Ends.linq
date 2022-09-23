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
						.Select(a => a)
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
							.OrderBy(c => c.Country)
							.Select(c => c.Country)
							.Distinct()
							//.Dump()
							;

	// .Take() and SKip()
	//  In1517, whey you wanted to use your paginator
	//   the quesry method was to return ONLY the
	//   needed records to display
	//	a)  You passed in the pagesize and pagenumber
	//	b)  the quesry was executed, returning all rows
	//	c)  Set your out parameter to the .Count of rows
	//  d)  Calculated the number of rows to skip (pageNumber -1) * pagesize
	//	e)	On the return statement, against your collection, you used a .SKIP & .TAKE
	//	Return variableName.Skip(rowsSku=ipped).Take(pagesize).ToList()


	//  Any and All

	//  There are 25 genres on files.
	//  Show genres that have tracks which are not on any playlist

	Genres
		.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
		.Select(g => g)
		//.Dump()
		;

	//  Show genres that have all tracks at least once on a playlist
	Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	.Select(g => g)
	//.Dump()
	;

	//  Compare the track collection of 2 people using ALl and Any
	//  Create a small anonymous collection for two people
	//  Roberta Almeida (AlmeidaR) and Michelle Brooks (BrooksM)

	var listA = PlaylistTracks
					.Where(x => x.Playlist.UserName.Equals("AlmeidaR"))
					.Select(x => new
					{
						Song = x.Track.Name,
						Genre = x.Track.Genre.Name,
						ID = x.TrackId,
						Artist = x.Track.Album.Artist.Name
					})
					.Distinct()  //  So we don't get duplicated tracks
					.OrderBy(x => x.Song)
					//.Dump()  // 110
					;

	var listB = PlaylistTracks
					.Where(x => x.Playlist.UserName.Equals("BrooksM"))
					.Select(x => new
					{
						Song = x.Track.Name,
						Genre = x.Track.Genre.Name,
						ID = x.TrackId,
						Artist = x.Track.Album.Artist.Name
					})
					.Distinct()  //  So we don't get duplicated tracks
					.OrderBy(x => x.Song)
					//.Dump()  // 88
					;
	//  List the tracks that BOTH Roberto and Michelle like
	//  Compare 2 collections together (List A and List B)
	//  ASSUME LISTA is Roverto and ListB is Michelle
	//  ListA is the collection you wish to report on
	//  ListB is what your wish to compare ListA to (no reporting)

	listA
		.Where(la => listB.Any(lb => lb.ID == la.ID))
		//  listB find ANYthing from ListB ID's == ListA ID's
		.OrderBy(lista => lista.Song)
		//.Dump()
		;

	listB
		.Where(lb => listA.Any(la => la.ID == lb.ID))
		//  listB find ANYthing from ListB ID's == ListA ID's
		.OrderBy(listb => listb.Song)
		//.Dump()
		;

	//  What songs does listA like but listB does not
	listA
		.Where(la => listB.All(lb => lb.ID != la.ID))
		//  listB find ANYthing from ListB ID's == ListA ID's
		.OrderBy(lista => lista.Song)
		.Dump()
		;

}













// You can define other methods, fields, classes and namespaces here
public class SongItem
{
	public string Song { get; set; }
	public double PlayTime { get; set; }
}

public class AlbumTracks
{
	public string Title { get; set; }
	public string Artist { get; set; }
	public List<SongItem> Songs { get; set; }
}