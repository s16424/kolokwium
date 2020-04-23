using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace kolokwium_s16424_AP.DTOs
{
    public class AddPrescriptionRequest
    {
        [Required(ErrorMessage = "Podaj date wystawienia")]
        public DateTime Date { get; set; }
       
        [Required(ErrorMessage = "Podaj date waznosci")]
        public DateTime DueDate { get; set; }
       
        [Required(ErrorMessage = "Podaj id pacjenta")]
        public int idPatient { get; set; }

        [Required(ErrorMessage = "Podaj id lekarza")]
        public string idDoctor { get; set; }
    }
}
