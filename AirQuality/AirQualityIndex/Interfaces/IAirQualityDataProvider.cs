using AirQualityIndex.Models;
using System.Collections.Generic;

namespace AirQualityIndex.Interfaces
{
    public interface IAirQualityDataProvider
    {
        List<Record> Fetch(int offset, int limit, string filters);
    }
}
