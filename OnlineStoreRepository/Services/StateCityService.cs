using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class StateCityService : IStateCityInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public List<StateModel> GetStates()
        {
            List<StateModel> listOfStates = db.States.Select(o => new StateModel { StateID = o.StateID, StateName = o.StateName }).ToList();
            return listOfStates;
        }

        public List<CityModel> GetCities(int stateID)
        {
            List<CityModel> listOfCities = db.Cities.Where(o => o.StateID == stateID).Select(o => new CityModel { CityID = o.CityID, CityName = o.CityName }).ToList();
            return listOfCities;
        }
    }
}
