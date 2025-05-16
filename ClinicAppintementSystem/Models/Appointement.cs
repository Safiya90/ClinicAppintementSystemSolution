using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppintementSystem.Models
{
    public class Appointement
    {
        public int AppointementId { get; set; }
        public DateTime Date { get; set; }
        public Doctor doctor { get; set; }
        public int DoctorId{ get; set; }

        public Patient patient { get; set; }
        public int PatientId { get; set; }
    }
}
