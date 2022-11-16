#region Additional Namespaces

using ChinookSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace ChinookSystem
{
    public static class ChinookExtensions
    {
        public static void ChinookSystemBackendDependencies(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            //  Register the DBContext class in Chinook2018 with the service collection
            services.AddDbContext<Chinook2018Context>(options);

            //  Add any services that you create in the class library
            //  using .AddTransient<t>(...)
        }
    }
}
