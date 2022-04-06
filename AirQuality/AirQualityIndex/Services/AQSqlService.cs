using AirQualityIndex.Interfaces;
using AirQualityIndex.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GeneralHelper;

namespace AirQualityIndex.Services
{
    public class AQSqlService : IDataBaseService
    {
        private string _connectionString = "";
        private AppSettings _appSettings;
        public AQSqlService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _connectionString = _appSettings.ConnectionString;
        }

        public bool ClearTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return false;
            }
            else
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    var sqlCommand = new SqlCommand();
                    //sqlCommand.CommandText = $"DELETE FROM QUOTENAME({})"
                    return true;
                }
            }
        }

        public bool WriteToDb(List<Record> records)
        {
            var tableName = _appSettings.StorageTableName;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    sqlConnection.Open();
                    sqlBulkCopy.DestinationTableName = tableName;
                    try
                    {
                        var dt = records.ToDataTable<Record>();
                        sqlBulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return true;
        }
    }
}
