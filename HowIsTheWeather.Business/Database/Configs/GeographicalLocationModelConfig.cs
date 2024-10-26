using HowIsTheWeather.Service.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database.Configs;

internal class GeographicalLocationModelConfig : IEntityTypeConfiguration<GeographicalLocationModel>
{
    public void Configure(EntityTypeBuilder<GeographicalLocationModel> builder)
    {
        builder.ToTable("GeographicalLocation");

        builder.HasKey(c => c.Id);


        builder.Property(c=> c.Id).ValueGeneratedOnAdd();
        builder.Property(c=> c.Latitude).IsRequired();
        builder.Property(c=> c.Longitude).IsRequired();


        builder.HasMany(c => c.Temperature)
            .WithOne(c => c.GeographicalLocation)
            .HasForeignKey(c => c.LocationId);
    }
}
