using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppintementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public int NationalId { get; set; }
        public string PatientName { get; set; }
        public int PhoneNo { get; set; }
        public List<Appointement> Appointements = new List<Appointement>();

    }
}
