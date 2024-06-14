using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface IStateCityInterface
    {
        List<StateModel> GetStates();

        List<CityModel> GetCities(int stateID);
    }
}
