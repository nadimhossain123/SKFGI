using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
   public class StateDistrictCity
    {
       public StateDistrictCity()
       { 
       }
       public int Save(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.Save(StateDistrictCity);
       }

       public DataTable GetAll(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.GetAll(StateDistrictCity);
       }
       public int SaveDistrict(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.SaveDistrict(StateDistrictCity);
       }

       public DataTable GetAllDistrict(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.GetAllDistrict(StateDistrictCity);
       }

       public int SaveCity(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.SaveCity(StateDistrictCity);
       }

       public DataTable GetAllCity(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.GetAllCity(StateDistrictCity);
       }

       public DataTable GetAllSchool(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.GetAllSchool(StateDistrictCity);
       }

     

       public int SaveSchool(Entity.Common.StateDistrictCity StateDistrictCity)
       {
           return DataAccess.Common.StateDistrictCity.SaveSchool(StateDistrictCity);
       }

    }
}
