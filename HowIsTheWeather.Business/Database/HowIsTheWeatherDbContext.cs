using HowIsTheWeather.Service.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database;

internal class HowIsTheWeatherDbContext : DbContext
{
    public HowIsTheWeatherDbContext(DbContextOptions<HowIsTheWeatherDbContext> options) : base(options)
    {

    }


    public virtual DbSet<GeographicalLocationModel> GeographicalLocationModels { get; set; }
    public virtual DbSet<TemperatureModel> TemperatureModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
