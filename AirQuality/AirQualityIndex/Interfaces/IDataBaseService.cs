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
        GenericResponse<string> WriteToDb(List<Record> lst);
        bool ClearTable();
        List<string> GetAllCities(string state);
        string GetLastRefreshed();
    }
}
