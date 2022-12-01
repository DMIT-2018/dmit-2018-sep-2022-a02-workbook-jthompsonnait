#nullable disable
using EToolSystem.DAL;
using EToolSystem.ViewModel;

namespace EToolSystem.BLL
{
    public class EmployeeService
    {
        #region Constructor and COntext Dependency

        //  rename the context to match your system
        private readonly eTools2021Context _context;
        internal EmployeeService(eTools2021Context context)
        {
            _context = context;
        }
        #endregion

        #region Services


        //  You will need to set up your extended method/backend dependencies

        //  Query to obtain the employee data
        public EmployeeInfo GetEmployeeInfo()
        {
            return _context.Employees
                .Where(x => x.EmployeeID == Security.EmployeeId())
                .Select(x => new EmployeeInfo()
                {
                    EmployeeID = x.EmployeeID,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .SingleOrDefault();
        }

        #endregion
    }
}