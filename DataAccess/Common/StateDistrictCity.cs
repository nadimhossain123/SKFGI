using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Common
{
    public class StateDistrictCity
    {
        public StateDistrictCity()
        {
        }
        public static int Save(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                oDm.Add("@State", SqlDbType.VarChar, 100, StateDistrictCity.State);
                oDm.Add("@CreatedBY", SqlDbType.Int, StateDistrictCity.CreatedBY);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("USP_State_Master_SAVE");
            }
        }
        public static DataTable GetAll(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateDistrictCity.StateID != 0)
                    oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                else
                    oDm.Add("@StateID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.State.Length != 0)
                    oDm.Add("@State", SqlDbType.VarChar, StateDistrictCity.State);
                else
                    oDm.Add("@State", SqlDbType.VarChar, DBNull.Value);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("USP_State_Master_GetAll");
            }
        }

        public static int SaveDistrict(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                oDm.Add("@District", SqlDbType.VarChar, 100, StateDistrictCity.District);
                oDm.Add("@CreatedBY", SqlDbType.Int, StateDistrictCity.CreatedBY);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("USP_District_Master_SAVE");
            }
        }

        public static DataTable GetAllDistrict(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateDistrictCity.DistrictID != 0)
                    oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                else
                    oDm.Add("@DistrictID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.StateID != 0)
                    oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                else
                    oDm.Add("@StateID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.District.Length != 0)
                    oDm.Add("@District", SqlDbType.VarChar, StateDistrictCity.District);
                else
                    oDm.Add("@District", SqlDbType.VarChar, DBNull.Value);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("USP_District_Master_GetAll");
            }
        }


        public static int SaveCity(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@CityID", SqlDbType.Int, StateDistrictCity.CityID);
                oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                oDm.Add("@City", SqlDbType.VarChar, 100, StateDistrictCity.City);
                oDm.Add("@CreatedBY", SqlDbType.Int, StateDistrictCity.CreatedBY);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("USP_City_Master_SAVE");
            }
        }

        public static DataTable GetAllCity(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateDistrictCity.CityID != 0)
                    oDm.Add("@CityID", SqlDbType.Int, StateDistrictCity.CityID);
                else
                    oDm.Add("@CityID", SqlDbType.Int, DBNull.Value);

                if (StateDistrictCity.DistrictID != 0)
                    oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                else
                    oDm.Add("@DistrictID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.StateID != 0)
                    oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                else
                    oDm.Add("@StateID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.City.Length != 0)
                    oDm.Add("@City", SqlDbType.VarChar, StateDistrictCity.City);
                else
                    oDm.Add("@City", SqlDbType.VarChar, DBNull.Value);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("USP_City_Master_GetAll");
            }
        }



        public static int SaveSchool(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@SchoolID", SqlDbType.Int, StateDistrictCity.SchoolID);
                oDm.Add("@School", SqlDbType.VarChar, 200, StateDistrictCity.School);
                oDm.Add("@CityID", SqlDbType.Int, StateDistrictCity.CityID);
                oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                oDm.Add("@Address", SqlDbType.VarChar, StateDistrictCity.Address);
                oDm.Add("@PIN", SqlDbType.Int, StateDistrictCity.Pin);
                oDm.Add("@CreatedBY", SqlDbType.Int, StateDistrictCity.CreatedBY);

                oDm.CommandType = CommandType.StoredProcedure;

                return oDm.ExecuteNonQuery("USP_School_Master_SAVE");
            }
        }



        public static DataTable GetAllSchool(Entity.Common.StateDistrictCity StateDistrictCity)
        {
            using (DataManager oDm = new DataManager())
            {
                if (StateDistrictCity.SchoolID != 0)
                    oDm.Add("@SchoolID", SqlDbType.Int, StateDistrictCity.SchoolID);
                else
                    oDm.Add("@SchoolID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.CityID != 0)
                    oDm.Add("@CityID", SqlDbType.Int, StateDistrictCity.CityID);
                else
                    oDm.Add("@CityID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.DistrictID != 0)
                    oDm.Add("@DistrictID", SqlDbType.Int, StateDistrictCity.DistrictID);
                else
                    oDm.Add("@DistrictID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.StateID != 0)
                    oDm.Add("@StateID", SqlDbType.Int, StateDistrictCity.StateID);
                else
                    oDm.Add("@StateID", SqlDbType.Int, DBNull.Value);
                if (StateDistrictCity.School.Length != 0)
                    oDm.Add("@School", SqlDbType.VarChar, 200, StateDistrictCity.School);
                else
                    oDm.Add("@School", SqlDbType.VarChar, DBNull.Value);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("USP_School_Master_GetAll");
            }
        }
    }
}
