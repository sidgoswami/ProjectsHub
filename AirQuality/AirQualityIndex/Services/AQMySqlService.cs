using AirQualityIndex.Interfaces;
using AirQualityIndex.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralHelper;

namespace AirQualityIndex.Services
{
    public class AQMySqlService : IDataBaseService
    {
        private string _connectionString = "";
        private AppSettings _appSettings;
        public AQMySqlService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _connectionString = _appSettings.ConnectionString;
        }

        public bool ClearTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool WriteToDb(List<Record> records)
        {
            try
            {
                var tableName = _appSettings.StorageTableName;
                using (MySqlConnection sqlConnection = new MySqlConnection(_connectionString))
                {
                    MySqlBulkCopy sqlBulkCopy = new MySqlBulkCopy(sqlConnection);
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
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }            
        }
    }
}
