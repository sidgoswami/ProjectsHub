using AirQualityIndex.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirQualityIndex.Interfaces
{
    public interface IAirQualityDataProvider
    {
        Task<List<Record>> Fetch(int offset, int limit, string filters);
    }
}
