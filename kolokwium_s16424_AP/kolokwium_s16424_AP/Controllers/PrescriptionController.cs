using kolokwium_s16424_AP.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_s16424_AP.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        iPrepDal _service;

        public PrescriptionController(iPrepDal service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetPescr
            ([FromServices]iPrepDal service, [FromQuery] string nazwisko)
        {
            if (nazwisko == null)
            {
                return Ok(service.GetPresciptions().OrderByDescending(s => s.Date));
            }

            using (var con = new SqlConnection("Data Source=db-mssql ;Initial Catalog=s16424; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from doctor where LastName = @nazwisko;";
                com.Parameters.AddWithValue("nazwisko", nazwisko);
                con.Open();
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    return BadRequest("Taki lekarz nie istnieje");
                }
                else
                    return Ok(service.GetPresciptions2(nazwisko).OrderByDescending(s => s.Date));

            }





        }
    }
}
