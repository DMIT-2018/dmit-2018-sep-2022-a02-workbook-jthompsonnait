using ChinookSystem.DAL;
using ChinookSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.BLL
{
    public class SecurityServices
    {
        #region Constructor and COntext Dependency

        //  replace the following context with your context
        private readonly Chinook2018Context _context;
        internal SecurityServices(Chinook2018Context context)
        {
            _context = context;
        }
        #endregion

        #region Services

        //  Query to obtain the DbVersion data
        public DbVersionInfo GetDbVersion()
        {
            DbVersionInfo info = _context.DbVersions
                .Select(x => new DbVersionInfo
                {
                    Major = x.Major,
                    Minor = x.Minor,
                    Build = x.Build,
                    ReleaseDate = x.ReleaseDate
                })
                .SingleOrDefault();
            return info;
        }
        #endregion
    }
}
