using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Domain.Enums;
using DoctorAppointmentDemo.Service.Interfaces;
using DoctorAppointmentDemo.Service.Services;
using DoctorAppointmentDemo.UI.Enums;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace DoctorAppointmentDemo.UI;

public partial class DoctorAppointment
{
    private readonly IDoctorService _doctorService;
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;

    public DoctorAppointment()
    {
        _doctorService = new DoctorService();
        _patientService = new PatientService();
        _appointmentService = new AppointmentService();
    }

    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====== Doctor Appointment System ======");
            Console.WriteLine("1. Doctors");
            Console.WriteLine("2. Patients");
            Console.WriteLine("3. Appointments");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var choice))
            {
                Console.WriteLine("Invalid input. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            switch ((MainMenuOptions)choice)
            {
                case MainMenuOptions.Exit:
                    Console.WriteLine("Goodbye!");
                    return;

                case MainMenuOptions.ManageDoctors:
                    ManageDoctors();
                    break;

                case MainMenuOptions.ManagePatients:
                    ManagePatients();
                    break;

                case MainMenuOptions.ManageAppointments:
                    ManageAppointments();
                    break;

                default:
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
