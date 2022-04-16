using AirQualityIndex.Models;
using GeneralHelper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityIndex.Interfaces
{
    public interface IDataBaseService
    {
        Task<GenericResponse<string>> WriteToDb(List<Record> lst);
        Task<bool> ClearTable();
        Task<List<string>> GetAllCities(string state);
        Task<List<string>> GetAllCities();
        Task<List<string>> GetAllStates();
        Task<string> GetLastRefreshed();

        Task<DataTable> GetAQData(string state, string city);
    }
}
