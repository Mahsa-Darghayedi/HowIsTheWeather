using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database;

internal interface IUnitOfWork : IDisposable
{
    HowIsTheWeatherDbContext Context { get; }
    Task CommitAsync();
    Task CommitBulkAsync();

}
