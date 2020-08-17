using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
  public  class StateDistrictCity
    {
      public StateDistrictCity()
      { 
      }
      public int StateID { get; set; }
      public string State { get; set; }
      public int CreatedBY { get; set; }
      public int DistrictID { get; set; }
      public string District { get; set; }
      public int CityID { get; set; }
      public string City { get; set; }
      public int SchoolID { get; set; }
      public string School { get; set; }
      public int Pin { get; set; }
      public string Address { get; set; }
    }
}
