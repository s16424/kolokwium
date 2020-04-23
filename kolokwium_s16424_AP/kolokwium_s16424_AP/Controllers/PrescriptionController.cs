using kolokwium_s16424_AP.DTOs;
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
            }
            return Ok(service.GetPresciptions2(nazwisko).OrderByDescending(s => s.Date));
            }

    [HttpPost]
    public IActionResult AddPrescription([FromBody]AddPrescriptionRequest request)
        {
            var pr = new AddPrescriptionRequest();
            pr.Date = request.Date;
            pr.DueDate = request.DueDate;

            if (pr.DueDate > pr.Date)
            {
                return BadRequest("Zle dane - data waznosci dalsza niz data wystawienia recepty");
            }

            pr.idDoctor = request.idDoctor;
            pr.idPatient = request.idPatient;

            using (var con = new SqlConnection("Data Source=db-mssql ;Initial Catalog=s16424; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    com.CommandText = "SELECT * from doctor where idDoctor = @id";
                    com.Parameters.AddWithValue("id",pr.idDoctor);
                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("taki lekarz nie istnieje");
                    }
                    com.CommandText = "SELECT * from patient where idPatient = @id";
                    com.Parameters.AddWithValue("id", pr.idPatient);
                    dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("taki pacjent nie istnieje");
                    }
                        com.CommandText = "INSERT INTO Prescription VALUES @data, @datawaznosci, @idpacjenta, @iddoktora";
                        com.Parameters.AddWithValue("data", pr.Date);
                        com.Parameters.AddWithValue("datawaznosci", pr.DueDate);
                        com.Parameters.AddWithValue("idpacjenta", pr.idPatient);
                        com.Parameters.AddWithValue("iddoktora", pr.idDoctor);

                    com.ExecuteNonQuery();
                        tran.Commit();
                    }
                    
                catch (SqlException ex)
                {
                    tran.Rollback();
                }
            }
            
            return Ok();
        }

        }
    }

