using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using HowIsTheWeather.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database;

internal class LocationRepository : ILocationRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<GeographicalLocationModel> _dbSet;
    public LocationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbSet = _unitOfWork.Context.GeographicalLocationModels;
    }

    public async Task InsertAsync(GeographicalLocationModel model)
    {
        await _dbSet.AddAsync(model);
        await _unitOfWork.CommitAsync();

    }

    public async Task<WeatherResponseDto?> GetLatestWeatherAsync(double latitude, double longitude)
    {
        var model = await _dbSet.Where(l => l.Latitude.Equals(latitude) && l.Longitude.Equals(longitude))
            .Include(c => c.Temperature).AsSplitQuery()
            .OrderByDescending(l => l.Id).AsNoTracking().FirstOrDefaultAsync();

        return model?.ToWeatherResponseDto();
    }
}
