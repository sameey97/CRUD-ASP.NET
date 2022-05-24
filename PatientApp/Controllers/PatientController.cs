using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PatientApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Mvc;
using System.Net;
using System.Linq;

namespace PatientApp.Controllers
{


    public class PatientController : Controller
    {
        private readonly string connstring = ConfigurationManager.ConnectionStrings["PatientString"].ConnectionString.ToString();
        // GET: Patient
        public ActionResult Index()
        {
            List<PatientModelView> patients = new List<PatientModelView>();
            try
            {
                string query = "SELECT * FROM Patient_tbl";
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                patients.Add(new PatientModelView
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Patient_Name = Convert.ToString(sdr["Patient_Name"]),
                                    Patient_Number = Convert.ToString(sdr["Patient_Number"]),
                                    Patient_Email = Convert.ToString(sdr["Patient_Email"]),
                                    Address = Convert.ToString(sdr["Address"]),
                                    Blood_Group = Convert.ToString(sdr["Blood_Group"])
                                });
                            }
                        }
                        con.Close();

                    }
                }

            }
            catch (Exception e)
            {

            }
            if (patients.Count == 0)
            {
                patients.Add(new PatientModelView());
            }
            return View(patients);

        }



        public ActionResult fetch_patient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PatientModelView Patient = new PatientModelView();
            try
            {
                string query = "SELECT * FROM Patient_tbl where Id=" + id;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Patient = new PatientModelView
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Patient_Name = Convert.ToString(sdr["Patient_Name"]),
                                    Patient_Number = Convert.ToString(sdr["Patient_Number"]),
                                    Patient_Email = Convert.ToString(sdr["Patient_Email"]),
                                    Address = Convert.ToString(sdr["Address"]),
                                    Blood_Group = Convert.ToString(sdr["Blood_Group"])
                                };
                            }
                        }
                        con.Close();

                    }
                }

            }
            catch (Exception e)
            {

            }
            if (Patient == null)
            {
                return HttpNotFound();
            }
            return View(Patient);

        }

        public ActionResult CreatePatient()
        {
            return View();
        }


        // Post 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePatient([Bind(Include = "Id, Patient_Name, Patient_Number, Patient_Email,Address, Blood_group")] PatientModelView patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {
                        string query = "INSERT into Patient_tbl VALUES (@Patient_Name, @Patient_Number, @Patient_Email,  @Address, @Blood_Group)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Patient_Name", patient.Patient_Name);
                            cmd.Parameters.AddWithValue("@Patient_Number", patient.Patient_Number);
                            cmd.Parameters.AddWithValue("@Patient_Email", patient.Patient_Email);
                            cmd.Parameters.AddWithValue("@Address", patient.Address);
                            cmd.Parameters.AddWithValue("@Blood_Group", patient.Blood_Group);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {


            }
            return View(patient);
        }

        // 3. *************Update Patient Detail in the Clinic **************** 

        public ActionResult UpdatePatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PatientModelView Patient = new PatientModelView();
            try
            {
                string query = "SELECT * FROM Patient_tbl where Id=" + id;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Patient = new PatientModelView
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Patient_Name = Convert.ToString(sdr["Patient_Name"]),
                                    Patient_Number = Convert.ToString(sdr["Patient_Number"]),
                                    Patient_Email = Convert.ToString(sdr["Patient_Email"]),
                                    Address = Convert.ToString(sdr["Address"]),
                                    Blood_Group = Convert.ToString(sdr["Blood_Group"])
                                };
                            }
                        }
                        con.Close();

                    }
                }

            }
            catch (Exception e)
            {

            }
            if (Patient == null)
            {
                return HttpNotFound();
            }
            return View(Patient);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient([Bind(Include = "Id,Patient_Name,Patient_Number,Patient_Email,Address,Blood_Group")] PatientModelView patient)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Patient_tbl SET Patient_Name = @Patient_Name, Patient_Number=@Patient_Number, Patient_Email=@Patient_Email,  Address=@Address, Blood_Group =@Blood_Group WHERE Id =@Id";
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Patient_Name", patient.Patient_Name);
                        cmd.Parameters.AddWithValue("@Patient_Number", patient.Patient_Number);
                        cmd.Parameters.AddWithValue("@Patient_Email", patient.Patient_Email);
                        cmd.Parameters.AddWithValue("@Address", patient.Address);
                        cmd.Parameters.AddWithValue("@Blood_Group", patient.Blood_Group);
                        cmd.Parameters.AddWithValue("@Id", patient.Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(patient);

        }


        // 3. *************Deelete Patient in the Clinic ******************

        public ActionResult DeletePatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PatientModelView Patient = new PatientModelView();
            try
            {
                string query = "SELECT * FROM Patient_tbl where Id=" + id;
                using (SqlConnection con = new SqlConnection(connstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Patient = new PatientModelView
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    Patient_Name = Convert.ToString(sdr["Patient_Name"]),
                                    Patient_Number = Convert.ToString(sdr["Patient_Number"]),
                                    Patient_Email = Convert.ToString(sdr["Patient_Email"]),
                                    Address = Convert.ToString(sdr["Address"]),
                                    Blood_Group = Convert.ToString(sdr["Blood_Group"])
                                };
                            }
                        }
                        con.Close();

                    }
                }

            }
            catch (Exception e)
            {

            }

            return View(Patient);

        }

        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePatient(int id)
        {
            using (SqlConnection con = new SqlConnection(connstring))
            {
                string query = "Delete FROM Patient_tbl WHERE Id='" + id + "'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


            }
            return RedirectToAction("Index");
        }

    }








}