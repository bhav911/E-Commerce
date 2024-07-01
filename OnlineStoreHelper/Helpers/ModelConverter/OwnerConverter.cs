using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class OwnerConverter
    {
        public static Owner ConvertNewOwnerToOwner(NewRegistration ownerModel)
        {
            Owner owner = new Owner()
            {
                shopname = ownerModel.Username,
                CityID = ownerModel.CityID,
                email = ownerModel.Email,
                password = ownerModel.Password,
                StateID = ownerModel.StateID,
                Description = ownerModel.Description
            };

            return owner;
        }

        public static List<OwnerModel> ConvertOwnerListToOwnerModelList(List<Owner> shopList)
        {
            List<OwnerModel> ownerModelList = new List<OwnerModel>();

            foreach (Owner owner in shopList)
            {
                OwnerModel ownerModel = new OwnerModel()
                {
                    OwnerID = owner.OwnerID,
                    Shopname = owner.shopname,
                    Email = owner.email,
                    State = owner.States.StateName,
                    City = owner.Cities.CityName,
                    Description = owner.Description
                };

                ownerModelList.Add(ownerModel);
            }

            return ownerModelList;
        }

        public static OwnerModel ConvertOwnerToOwnerModel(Owner owner)
        {
            OwnerModel ownerModel = new OwnerModel()
            {
                OwnerID = owner.OwnerID,
                Shopname = owner.shopname,
                Email = owner.email,
                Description = owner.Description,
                City = owner.Cities.CityName,
                Password = owner.password,
                State = owner.States.StateName,
                CityID = owner.CityID,
                StateID = owner.StateID
            };

            return ownerModel;
        }
    }
}
