﻿using AirQualityIndex.Interfaces;
using GeneralHelper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityIndex.Controllers
{
    public class StoredDataController : Controller
    {
        private readonly IAirQualityDataProvider _airQualityDataProvider;
        private readonly IDataBaseService _dbService;

        public StoredDataController(IAirQualityDataProvider airQualityDataProvider, IDataBaseService dbService)
        {
            _airQualityDataProvider = airQualityDataProvider;
            _dbService = dbService;
        }

        public IActionResult StoredDataIndex()
        {
            return View();
        }

        /// <summary>
        /// Stores the data from the API call in the database for a faster retrieval
        /// </summary>
        /// <returns></returns>
        [HttpGet("AirQualityFullDbRefresh")]
        public JsonResult AirQualityFullDbRefresh()
        {
            var fetchedAirQuality = _airQualityDataProvider.Fetch(0, 10, "");
            if (fetchedAirQuality != null && fetchedAirQuality.Count > 0)
            {
                if (_dbService.ClearTable())
                {
                    var writtenToDb = _dbService.WriteToDb(fetchedAirQuality);
                    if (writtenToDb.Success)
                    {
                        string lastRefreshed = _dbService.GetLastRefreshed();
                        var obj = new
                        {
                            Cities = fetchedAirQuality.Select(aq => aq.city).ToList<string>(),
                            States = fetchedAirQuality.Select(aq => aq.state).ToList<string>(),
                            LastRefreshed = lastRefreshed
                        };
                        var genericSuccessResponse = new GenericResponse<object>(true, obj, $"Db Refreshed with data of {lastRefreshed}");
                        return Json(genericSuccessResponse);
                    }
                    else
                    {
                        return Json(writtenToDb);
                    }                    
                }
            }
            return Json(false);
        }

        [HttpGet("GetLastRefreshed")]
        public JsonResult GetLastRefreshed()
        {
            var lastRefreshed = _dbService.GetLastRefreshed();
            if (!string.IsNullOrWhiteSpace(lastRefreshed))
            {
                var successResponse = new GenericResponse<string>(true, lastRefreshed, "Fetched Last Refresh Successfully");
                return Json(successResponse);
            }
            var failedResponse = new GenericResponse<string>(false, lastRefreshed, $"No record found in database.");
            return Json(failedResponse);
        }

        /// <summary>
        /// Get all the cities for state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("GetCityForState")]
        public JsonResult GetCityForState(string state)
        {
            var fetchedCities = _dbService.GetAllCities(state);
            if (fetchedCities != null && fetchedCities.Count > 0)
            {
                var successResponse = new GenericResponse<List<string>>(true, fetchedCities, "Fetched cities Successfully");
                return Json(successResponse);
            }
            var failedResponse = new GenericResponse<List<string>>(false, fetchedCities, $"No cities fetched for state: {state}");
            return Json(failedResponse);
        }
    }
}
