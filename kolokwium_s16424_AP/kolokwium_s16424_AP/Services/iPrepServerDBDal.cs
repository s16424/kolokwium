using kolokwium_s16424_AP.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_s16424_AP.DTOs
{
    public class iPrepServerDBDal : iPrepDal
    {
        public IEnumerable<GetPrescriptionResponse> GetPresciptions2(string orderBy)
        {
            var list = new List<GetPrescriptionResponse>();
            using (var con = new SqlConnection("Data Source=db-mssql ;Initial Catalog=s16424; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select IdPrescription, Date, DueDate, Patient.LastName a, Doctor.LastName b " +
                    "FROM Prescription INNER JOIN Doctor ON Prescription.IdDoctor = Doctor.IdDoctor " +
                    "INNER JOIN Patient ON Prescription.IdPatient = Patient.IdPatient WHERE Doctor.LastName = @nazwisko;";
                com.Parameters.AddWithValue("nazwisko", orderBy);
                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var Pre = new GetPrescriptionResponse();
                    Pre.IdPrescription = (int)dr["IdPrescription"];
                    Pre.Date = (DateTime)dr["Date"];
                    Pre.DueDate = (DateTime)dr["DueDate"];
                    Pre.PatientLastName = dr["a"].ToString();
                    Pre.DoctorLastName = dr["b"].ToString();
                    list.Add(Pre);
                }
            }
            return (list);
        }

        public IEnumerable<GetPrescriptionResponse> GetPresciptions()
        {
            var list = new List<GetPrescriptionResponse>();
            using (var con = new SqlConnection("Data Source=db-mssql ;Initial Catalog=s16424; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select IdPrescription, Date, DueDate, Patient.LastName a, Doctor.LastName b " +
                    "FROM Prescription INNER JOIN Doctor ON Prescription.IdDoctor = Doctor.IdDoctor " +
                    "INNER JOIN Patient ON Prescription.IdPatient = Patient.IdPatient;";

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var Pre = new GetPrescriptionResponse();
                    Pre.IdPrescription = (int)dr["IdPrescription"];
                    Pre.Date = (DateTime)dr["Date"];
                    Pre.DueDate = (DateTime)dr["DueDate"];
                    Pre.PatientLastName = dr["a"].ToString();
                    Pre.DoctorLastName = dr["b"].ToString();
                    list.Add(Pre);
                }
            }
            return (list);
        }
    }
}
