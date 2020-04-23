using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_s16424_AP.DTOs
{
    public class GetPrescriptionResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string PatientLastName { get; set; }
        public string DoctorLastName { get; set; }
    }
}
