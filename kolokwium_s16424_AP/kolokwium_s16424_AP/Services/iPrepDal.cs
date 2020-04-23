using kolokwium_s16424_AP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_s16424_AP.Services
{
    public interface iPrepDal
    {
        public IEnumerable<GetPrescriptionResponse> GetPresciptions();
        public IEnumerable<GetPrescriptionResponse> GetPresciptions2(string orderby);

    }
}
