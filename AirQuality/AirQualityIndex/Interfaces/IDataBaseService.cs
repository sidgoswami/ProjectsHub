using AirQualityIndex.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityIndex.Interfaces
{
    public interface IDataBaseService
    {
        bool WriteToDb(List<Record> lst);
        bool ClearTable(string tableName);
    }
}
