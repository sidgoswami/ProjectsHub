using AirQualityIndex.Interfaces;
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
                return Json(_dbService.WriteToDb(fetchedAirQuality));
            }
            return Json(false);
        }
    }
}
