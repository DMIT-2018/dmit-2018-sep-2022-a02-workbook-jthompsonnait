#nullable disable
using ChinookSystem.DAL;
using ChinookSystem.ViewModels;

namespace ChinookSystem.BLL
{
    public class TrackServices
    {
        #region Constructor for COntext Dependency

        private readonly Chinook2018Context _context;
        internal TrackServices(Chinook2018Context context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<TrackSelection> Track_FetchTracksBy(string searcharg, string searchby)
        {
            if (string.IsNullOrWhiteSpace(searcharg))
            {
                throw new ArgumentNullException("No search value submitted");
            }
            if (string.IsNullOrWhiteSpace(searchby))
            {
                throw new ArgumentNullException("No search style submitted");
            }
            IEnumerable<TrackSelection> results = _context.Tracks
                .Where(x => (x.Album.Artist.Name.Contains(searcharg) &&
                             searchby.Equals("Artist")) ||
                            (x.Album.Title.Contains(searcharg) &&
                             searchby.Equals("Album")))
                .Select(x => new TrackSelection
                {
                    TrackId = x.TrackId,
                    SongName = x.Name,
                    AlbumTitle = x.Album.Title,
                    ArtistName = x.Album.Artist.Name,
                    Milliseconds = x.Milliseconds,
                    Price = x.UnitPrice
                });
            return results.ToList();
        }


        #endregion
    }
}
