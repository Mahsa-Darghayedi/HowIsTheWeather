using HowIsTheWeather.Service.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database.Configs;

internal class TemperatureModelConfig : IEntityTypeConfiguration<TemperatureModel>
{
    public void Configure(EntityTypeBuilder<TemperatureModel> builder)
    {
        builder.ToTable("Temperature");

        builder.HasKey(c => c.Id);


        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.LocationId).IsRequired();
        builder.Property(c => c.Time).IsRequired();
        builder.Property(c => c.Temperature).IsRequired();


    }
}
