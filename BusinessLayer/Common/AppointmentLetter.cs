using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class AppointmentLetter
    {
        public AppointmentLetter()
        {
        }

        public DataSet GetAppointmentLetter(Entity.Common.AppointmentLetter Appointment)
        {
            return DataAccess.Common.AppointmentLetter.GetAppointmentLetter(Appointment);
        }
    }
}
