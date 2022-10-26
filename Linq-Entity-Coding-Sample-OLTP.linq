<Query Kind="Program">
  <Connection>
    <ID>f82afac0-3050-40c9-b69d-e7aa1885402e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>Chinook2018</Database>
    <DisplayName>Chinook-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	try
	{
		#region Query method using linq to entity

		//  Track_FetchTrackBy
		//	TrackService is the BLL
		//	FetchTrackBy is the method name

		string searchPattern = "deep";
		string searchType = "Artist";
		List<TrackSelection> tracklist_display = TrackService_FetchTrackBy(searchType, searchPattern);
		//tracklist_display.Dump();

		//  PlaylistTrackService is the BLL
		//  FetchPlaylist is the method name

		string playlistName = "hansenb1";
		string userName = "HansenB";
		//  List<PlaylistTrackInfo> playlist_display = PlaylistTrackService_FetchPlaylist(playlistName, userName);
		//  playlist_display.Dump();
		#endregion

		#region Command method using linq to entity
		//  coded and tested the AddTrack
		//  The command method will receive no collection but will receive individual arguments
		//	playlistName, userName, trackID

		//	793	A Castle Full of Rascals
		//	822	A Twist In The Tail
		//	543	Burn
		//	756	Child In Time
		//  string playlistName = "hansenbtest1";
		//  int trackID = 822;
		//  PlaylistTrackService_AddTrack(playlistName, userName, trackID);


		//	on the web page, the post method would have already have access to the
		//	BindProperty variables containing the input values
		playlistName = "hansenbtest";
		List<PlaylistTrackTRX> tracklistinfo = new List<PlaylistTrackTRX>();
		tracklistinfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = false,
			TrackID = 543,
			TrackNumber = 1,
			TrackInput = 6
		});
		tracklistinfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = false,
			TrackID = 756,
			TrackNumber = 2,
			TrackInput = 99
		});
		tracklistinfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = true,
			TrackID = 822,
			TrackNumber = 3,
			TrackInput = 8
		});
		tracklistinfo.Add(new PlaylistTrackTRX()
		{
			SelectedTrack = true,
			TrackID = 793,
			TrackNumber = 4,
			TrackInput = 2
		});

		//  Call the service method to process data deletion
		PlayListTrackService_RemoveTracks(playlistName, userName, tracklistinfo)

		#endregion
	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

#region Methods
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

// You can define other methods, fields, classes and namespaces here
#region Models
public class TrackSelection
{
	public int TrackId { get; set; }
	public string SongName { get; set; }
	public string AlbumTitle { get; set; }
	public string ArtistName { get; set; }
	public int Milliseconds { get; set; }
	public decimal Price { get; set; }
}

public class PlaylistTrackInfo
{
	public int TrackId { get; set; }
	public int TrackNumber { get; set; }
	public string SongName { get; set; }
	public int Milliseconds { get; set; }
}

public class PlaylistTrackTRX
{
	public bool SelectedTrack { get; set; }
	public int TrackID { get; set; }
	public int TrackNumber { get; set; }
	public int TrackInput { get; set; }
}
#endregion

#region Track BLL (TrackService)
#region Query
public List<TrackSelection> TrackService_FetchTrackBy(string searchType
, string searchPattern)
{
	IEnumerable<TrackSelection> trackSelections = Tracks
					.Where(x => searchType == "Artist" ?
							x.Album.Artist.Name.Contains(searchPattern) :
							x.Album.Title.Contains(searchPattern))
					.Select(x => new TrackSelection
					{
						TrackId = x.TrackId,
						SongName = x.Name,
						AlbumTitle = x.Album.Title,
						ArtistName = x.Album.Artist.Name,
						Milliseconds = x.Milliseconds,
						Price = x.UnitPrice
					})
					.OrderBy(x => x.SongName);
	return trackSelections.ToList();
}

#endregion
#endregion

#region PlaylistTrack Bll (PlaylistTrackService)
#region Query
public List<PlaylistTrackInfo> PlaylistTrackService_FetchPlaylist(string playlistName,
														string userName)
{
	IEnumerable<PlaylistTrackInfo> playlistTrackInfos = PlaylistTracks
									.Where(x => x.Playlist.Name == playlistName &&
											x.Playlist.UserName == userName)
									.Select(x => new PlaylistTrackInfo
									{
										TrackId = x.TrackId,
										TrackNumber = x.TrackNumber,
										SongName = x.Track.Name,
										Milliseconds = x.Track.Milliseconds
									}).OrderBy(x => x.TrackNumber);
	return playlistTrackInfos.ToList();
}

#endregion
#endregion

#region Commands TRX methods
public void PlaylistTrackService_AddTrack(string playlistName, string userName, int trackID)
{
	//  create local variables
	bool trackExist = false;
	Playlists playList = null;
	int trackNumber = 0;
	bool playlistTrackExist = false;
	PlaylistTracks playlistTracks = null;

	#region Business Logic and Parameter Exceptions
	//  create a List<Exception> to contain all discovered errors
	List<Exception> errorList = new List<Exception>();

	//	Business Rules
	//	these are processing rules that need to be satisfied for valid data.
	//		rule:	a track can only exist once on a playlist
	//		rule:	each track on a playlist is assigned a continous track number
	//
	//	If the business rules are passed, consider the data valid, then
	//		a)	stage your transaction work (Adds, Updates, Deletes)
	//		b)	excute a SINGLE .SaveChanges() - commits to database

	//	We could assume that user name and track ID will always be valid.

	//	parameter validation
	if (string.IsNullOrWhiteSpace(playlistName))
	{
		throw new ArgumentNullException("Playlist name is missing");
	}
	if (string.IsNullOrWhiteSpace(userName))
	{
		throw new ArgumentNullException("User name is missing");
	}
	#endregion
	//  check that the incoming data exists
	trackExist = Tracks
		.Where(x => x.TrackId == trackID)
		.Select(x => x.TrackId)
		.Any();
	if (!trackExist)
	{
		throw new ArgumentNullException("Selected track no longer is on file.  Refresh track table");
	}

	//  Business Process
	playList = Playlists
		.Where(x => x.Name == playlistName
					&& x.UserName == userName)
		.FirstOrDefault();
	if (playList == null)
	{
		playList = new Playlists()
		{
			Name = playlistName,
			UserName = userName
		};
		//stage (only in memory)
		Playlists.Add(playList);
		trackNumber = 1;
	}
	else
	{
		//  playlist already exist
		//	rule:	unique tracks on playlist
		playlistTrackExist = PlaylistTracks
							//.Where(x => x.TrackId == trackID)
							.Any(x => x.Playlist.Name == playlistName
								&& x.Playlist.UserName == userName
								&& x.TrackId == trackID);

		if (playlistTrackExist)
		{
			var songName = Tracks
							.Where(x => x.TrackId == trackID)
							.Select(x => x.Name)
							.FirstOrDefault();
			//  rule violation
			errorList.Add(new Exception($"Selected track({songName}) is already on the playlist"));
		}
		else
		{
			trackNumber = PlaylistTracks
							.Where(x => x.Playlist.Name == playlistName &&
										x.Playlist.UserName == userName)
							.Count();
			trackNumber++;
		}
	}

	//  add the track to the playlist
	//  create an instance for the playlist track

	playlistTracks = new PlaylistTracks();

	//	load values
	playlistTracks.TrackId = trackID;
	playlistTracks.TrackNumber = trackNumber;

	//	What about the second part of the priary key: PlayListID?
	//	If playlist exists, then we know the id:  playlist.PlayListID
	//	But if the playlist is NEW, we DO NOT KNOW the ID

	//	In the sitution of a NEW playlist, even though we have created the playlist
	//		instance (see above) it is ONLY staged!!!
	//	This means that the actual SQL record has NOT yet been created.
	//	This means that the IDENTITY value for the new playlist DOES NOT yet exists.
	//	The value on the playlist instance (playList) is zero (0)
	//		Therefore we have a serious problem.

	//	Solution
	//	It is build into the Entity Framework software and is based using the
	//		navigational property in PlayList pointing to it's "child"

	//	Staging a typical Add in the past was to reference the entity and use
	//		the entity.Add(xxx)
	//	IF you use this statement, the playlistID would be zero (0)
	//		causing your transaction to ABORT.
	//	WHY?  PKeys cannot be zero (0) (FKey to PKey problem)

	//  INSTEAD, do the staging using the "parent.navChildProperty.Add(xxx)"
	playList.PlaylistTracks.Add(playlistTracks);

	//	Staging is complete.
	//	Commit the work (Transaction)
	//	Committing the work needs a .SaveChanges()
	//	A transaction has ONLY ONE .SaveChanges()
	//	But, what if you have discovered errors during the business process???
	if (errorList.Count > 0)
	{
		//  throw the list of business processing error(s)
		throw new AggregateException("unable to add new track.  Check concerns", errorList);
	}
	else
	{
		//  consider data valid
		//  has passed business processing rules
		SaveChanges();
	}












}

public void PlayListTrackService_RemoveTracks(string playlistName, string userName,
										List<PlaylistTrackTRX> tracklistinfo)
{
	//  local variables
	Playlists plylist = null;
	PlaylistTracks playlistTracks = null;
	int tracknumber = 0;
	
	//  we need a container to hold x number of Exception messages
	List<Exception> errorList = new List<Exception>();

	//	parameter validation
	if (string.IsNullOrWhiteSpace(playlistName))
	{
		throw new ArgumentNullException("Playlist name is missing");
	}
	if (string.IsNullOrWhiteSpace(userName))
	{
		throw new ArgumentNullException("User name is missing");
	}

var count = tracklistinfo.Count();
if (count == 0)
{
	throw new ArgumentNullException("No list of tracks wer submitted");
}
else
{
	//  obtain the tracks to keep
	//  the SelectedTrack is a boolean field
	//	false:	keep
	//	true:	remove
	//	create a query to extract the "keep" tracks from the incoming data.
	IEnumerable<PlaylistTrackTRX> keepList = tracklistinfo
												.Where( x=> !x.SelectedTrack)
												.OrderBy(x => x.TrackNumber);
	// obtain the tracks to remove
	IEnumerable<PlaylistTrackTRX> removeList = tracklistinfo
												.Where(x => x.SelectedTrack);
												
									
}


}
#endregion









