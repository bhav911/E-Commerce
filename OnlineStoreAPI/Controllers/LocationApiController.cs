using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStoreAPI.Controllers
{
    public class LocationApiController : ApiController
    {
        private readonly StateCityService _stateCity = new StateCityService();
     
        [HttpGet]
        [Route("api/LocationApi/GetStates")]
        public List<StateModel> GetStates()
        {
            List<StateModel> stateList = _stateCity.GetStates();
            return stateList;
        }

        [HttpGet]
        [Route("api/LocationApi/GetCities")]
        public List<CityModel> GetCities(int stateID)
        {
            List<CityModel> cityList = _stateCity.GetCities(stateID);
            return cityList;
        }
    }
}