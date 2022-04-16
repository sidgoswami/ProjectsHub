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
using GeneralHelper.Models;

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

        public bool ClearTable()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM {_appSettings.StorageTableName}";
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        var rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected >= 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                    return false;
                }

                return true;
            }
        }

        public GenericResponse<string> WriteToDb(List<Record> records)
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
                        return new GenericResponse<string>(true, "", "API data written to database.");
                    }
                    catch (Exception ex)
                    {
                        return new GenericResponse<string>(false, ex.Message, "Error in writing data to database.");
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }

        public List<string> GetAllCities(string state)
        {
            try
            {
                List<string> lst = new List<string>();
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT city FROM AirQualityRecords WHERE state = @state";
                    using (var sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@state", state));                        
                        try
                        {
                            sqlConnection.Open();
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                            while (sqlDataReader.Read())
                            {
                                lst.Add((string)sqlDataReader[0]);
                            }
                            sqlDataReader.Close();
                            return lst;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetAllCities()
        {
            try
            {
                List<string> lst = new List<string>();
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT city FROM AirQualityRecords";
                    using (var sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                            while (sqlDataReader.Read())
                            {
                                lst.Add((string)sqlDataReader[0]);
                            }
                            sqlDataReader.Close();
                            return lst;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetAllStates()
        {
            try
            {
                List<string> lst = new List<string>();
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DISTINCT state FROM AirQualityRecords";
                    using (var sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                            while (sqlDataReader.Read())
                            {
                                lst.Add((string)sqlDataReader[0]);
                            }
                            sqlDataReader.Close();
                            return lst;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetLastRefreshed()
        {
            try
            {
                List<string> lst = new List<string>();
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT TOP 1 last_update FROM AirQualityRecords ORDER BY LAST_UPDATE DESC";
                    using (var sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        try
                        {
                            sqlConnection.Open();
                            return sqlCommand.ExecuteScalar().ToString();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetAQData(string state, string city)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "";
                if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(state))
                {
                    query = $"SELECT * FROM AirQualityRecords WHERE state = @state and city = @city";
                }
                else if (!string.IsNullOrWhiteSpace(state))
                {
                    query = $"SELECT * FROM AirQualityRecords WHERE state = @state";
                }
                else if (!string.IsNullOrWhiteSpace(city))
                {
                    query = $"SELECT * FROM AirQualityRecords WHERE city = @city ";
                }
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(state))
                        {
                            sqlCommand.Parameters.Add(new SqlParameter("@state", state));
                        }
                        if (!string.IsNullOrWhiteSpace(city))
                        {
                            sqlCommand.Parameters.Add(new SqlParameter("@city", city));
                        }
                        sqlConnection.Open();
                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlDataAdapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return null;
        }
    }
}
