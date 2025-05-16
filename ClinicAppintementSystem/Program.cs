using ClinicAppintementSystem.Context;
using ClinicAppintementSystem.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClinicAppintementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using ApplicationDbContext _db = new ApplicationDbContext();
            Greeting();
            bool run=true;
            while (run)
            {
                Menue();
                string option=Console.ReadLine();
                switch (option)
                {
                    case "1":
                            Console.WriteLine("-- Register New Patient --");
                            Console.Write("Enter Patient Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter National ID: ");
                            int nationalId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Phone Number: ");
                            int phoneNo = int.Parse(Console.ReadLine());
                            Patient patient = new Patient()
                            {
                                PatientName = name,
                                NationalId = nationalId,
                                PhoneNo = phoneNo
                            };
                        _db.Patients.Add(patient);
                            _db.SaveChanges();
                        Console.WriteLine("Patient registered successfully!");
                        break;
                    case "2":
                        Console.WriteLine("-- Add New Doctor --");
                        Console.Write("Enter Doctor Name: ");
                        string dname = Console.ReadLine();
                        Console.Write("Enter Specialty: ");
                        string specialty = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        int dphoneNo = int.Parse(Console.ReadLine());
                        Doctor doctor = new Doctor()
                        {
                            DoctorName = dname,
                            Specialty = specialty,
                            PhoneNo = dphoneNo
                        };
                        _db.Doctors.Add(doctor);
                        _db.SaveChanges();
                        Console.WriteLine("Patient registered successfully!");
                        break;

                     case "3":
                        Console.WriteLine("-- Search Doctor by Specialty --");
                        Console.Write("Enter Specialty to search: ");
                        string spec= Console.ReadLine();
                        var doc=(from d  in _db.Doctors
                                 where d.Specialty == spec
                                 select d).FirstOrDefault();
                        Console.WriteLine("Doctors Found : ");
                        Console.WriteLine($"- Dr. {doc.DoctorName} | Phone: {doc.PhoneNo}");
                        break;
                     case "4":
                        Console.WriteLine("--   Book Appointement -- ");
                        Console.Write("Enter Patient National ID: ");
                        int pnational= int.Parse(Console.ReadLine());
                        var pExist= (from p in _db.Patients
                                     where p.NationalId==pnational
                                     select p).FirstOrDefault();
                        if (pExist==null)
                        {
                            Console.WriteLine("Sorry Patient is not found..Please try again");
                            break;
                        }
                        Console.Write("Enter Doctor Name :");
                        string docname= Console.ReadLine();
                        var docExists= (from d in _db.Doctors
                                        where d.DoctorName == docname
                                        select d).FirstOrDefault();
                        if (docExists == null)
                        {
                            Console.WriteLine("Sorry doctor is not found..Please try again");
                            break;
                        }
                        Console.Write("Enter Appointement Date (dd/mm/yyyy): ");
                        DateTime day= Convert.ToDateTime(Console.ReadLine());
                        var dateExists= (from a in _db.Appointements
                                         where a.DoctorId == docExists.DoctorId && a.Date==day
                                         select a).FirstOrDefault();
                        if (dateExists != null)
                        {
                            Console.WriteLine("Sorry the doctor is not available in this day");
                            break;
                        }
                        Appointement newApp= new Appointement();
                        {
                            newApp.PatientId = pExist.PatientId;
                            newApp.DoctorId = docExists.DoctorId;
                            newApp.Date = day;


                        }
                        _db.Appointements.Add(newApp);
                        _db.SaveChanges();
                        Console.WriteLine("Appointment booked successfully!");
                        break;
                    case "5":
                        Console.Write("View Patient Appointements --");
                        Console.Write("Enter Patient National ID: ");
                        int nationalView=int.Parse(Console.ReadLine());
                        var p_national=(from p in _db.Patients
                                        where p.NationalId == nationalView
                                        select p.PatientId).FirstOrDefault();
                        if (p_national == 0)
                        {
                            Console.WriteLine("Patient is not found");
                            break;
                        }
                        var Appoint4Patient =  from a in _db.Appointements
                                               join d in _db.Doctors on a.DoctorId equals d.DoctorId
                                               join p in _db.Patients on a.PatientId equals p.PatientId
                                               where a.PatientId == p_national
                                               select new
                                               {
                                                   Doctor = d.DoctorName,
                                                   patient= p.PatientName,
                                                   Date = a.Date,
                                                   special=d.Specialty,
                                               };
                        if (Appoint4Patient.Any())
                        {
                            Console.WriteLine("Appointments found:");
                            foreach (var app in Appoint4Patient)
                            {
                                Console.WriteLine($"Appointements for : {app.patient}: \n - Date: {app.Date} | Doctor: Dr.{app.Doctor} | Specialty: {app.special} ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No appointments found for this patient.");
                        }
                        break;
                    case "6":
                        Console.WriteLine("-- View All Appointements --");
                        Console.WriteLine("All Booked Appointments : ");
                        var allAppointments = from a in _db.Appointements
                                              join p in _db.Patients on a.PatientId equals p.PatientId
                                              join d in _db.Doctors on a.DoctorId equals d.DoctorId
                                              select new
                                              {
                                                  Patient = p.PatientName,
                                                  Doctor = d.DoctorName,
                                                  Date = a.Date,
                                                  Specialty = d.Specialty,
                                              };

                        if (allAppointments.Any())
                        {
                            Console.WriteLine("All appointments:");
                            foreach (var app in allAppointments)
                            {
                                Console.WriteLine($"Patient: {app.Patient} | Doctor: {app.Doctor} | Date: {app.Date} | Specialty: {app.Specialty}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No appointments found.");
                        }
                        break;
                    case "7":
                        Console.WriteLine("Thank you for using Oman Clinic Appointment System. Goodbye! ");
                        return;

                }
            }
            
        }
        public static void Greeting()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("   Welcome to Oman Clinic Appointement System");
            Console.WriteLine("==================================================");

        }
        public static void Menue()
        {
            Console.WriteLine("1. Register New Patient");
            Console.WriteLine("2. Add New Doctor");
            Console.WriteLine("3. Search Doctor by Specialty");
            Console.WriteLine("4. Book Appointement");
            Console.WriteLine("5. View Patient Appointement");
            Console.WriteLine("6. View All Appointement");
            Console.WriteLine("7. Exit");
            Console.WriteLine("\nEnter your choise: ");
        }
    }
}
