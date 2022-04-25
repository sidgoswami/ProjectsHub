using AirQualityIndex.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirQualityIndex.Controllers
{
    public class RealTimeController : Controller
    {
        private readonly IAirQualityDataProvider _airQualityDataProvider;

        public RealTimeController(IAirQualityDataProvider airQualityDataProvider)
        {
            _airQualityDataProvider = airQualityDataProvider;
        }
        public IActionResult RealTimeIndex()
        {
            return View();
        }

        [HttpGet("FetchAirQuality")]
        [ResponseCache(CacheProfileName = "filtersProfile" )]
        public async Task<JsonResult> FetchAirQuality(int offset, int limit, string filters)
        {
            var fetchedAirQuality = await _airQualityDataProvider.Fetch(offset, limit, filters); //(0, 10, "[city]=Darbhanga");
            return Json(fetchedAirQuality);
        }
    }
}
