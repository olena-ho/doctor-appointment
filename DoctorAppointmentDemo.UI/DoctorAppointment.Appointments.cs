using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.UI.Enums;

namespace DoctorAppointmentDemo.UI;

public partial class DoctorAppointment
{
    private void ManageAppointments()
    {
        while (true)
        {
            Console.WriteLine("===== Appointments =====");

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Return to main menu");
            Console.WriteLine("2. See all appointments");
            Console.WriteLine("3. Add a new appointment");
            Console.WriteLine("4. Update an appointment");
            Console.WriteLine("5. Delete an appointment");
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var action))
            {
                Console.WriteLine("Invalid input. Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            switch ((ActionMenu)action)
            {
                case ActionMenu.Return:
                    return;

                case ActionMenu.ListAll:
                    DisplayAppointments();
                    break;

                case ActionMenu.Add:
                    AddAppointment();
                    break;

                case ActionMenu.Update:
                    UpdateAppointment();
                    break;

                case ActionMenu.Delete:
                    DeleteAppointment();
                    break;

                default:
                    Console.WriteLine("Unknown option. Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }

    }

    private void DisplayAppointments()
    {
        Console.WriteLine("=== The List of All Appointments ===");

        var appointments = _appointmentService.GetAll();

        if (!appointments.Any())
        {
            Console.WriteLine("No appointments found.");
            return;
        }

        foreach (var a in appointments)
        {
            _appointmentService.ShowInfo(a);
        }
        Console.WriteLine("\n");
    }

    private void AddAppointment()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Appointment ===");
        var appointment = new Appointment();
        DisplayDoctors();
        DisplayPatients();

        Console.Write("Doctor ID: ");
        int doctorId = int.TryParse(Console.ReadLine(), out var docId) ? docId : 0;
        appointment.Doctor = _doctorService.Get(doctorId);

        Console.Write("Patient ID: ");
        int patientId = int.TryParse(Console.ReadLine(), out var patId) ? patId : 0;

        appointment.Patient = _patientService.Get(patientId);

        Console.Write("Appointment Date and Time (yyyy-MM-dd HH:mm): ");
        appointment.DateTimeFrom = DateTime.TryParse(Console.ReadLine(), out var date)
            ? date
            : DateTime.Now;

        Console.Write("Description (optional): ");
        appointment.Description = Console.ReadLine();

        _appointmentService.Create(appointment);

        Console.WriteLine("Appointment added successfully! Press Enter to return.");
        Console.ReadLine();
    }

    private void UpdateAppointment()
    {
        Console.Clear();
        Console.WriteLine("=== Update Appointment ===");

        DisplayAppointments();
        Console.Write("Enter Appointment ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        var existing = _appointmentService.Get(id);
        if (existing == null)
        {
            Console.WriteLine("Appointment not found. Press Enter to return.");
            Console.ReadLine();
            return;
        }

        Console.Write($"Doctor ID ({existing.Doctor.Id}): ");
        if (int.TryParse(Console.ReadLine(), out var docId)) existing.Doctor.Id = docId;

        Console.Write($"Patient ID ({existing.Patient.Id}): ");
        if (int.TryParse(Console.ReadLine(), out var patId)) existing.Patient.Id = patId;

        Console.Write($"Date and Time ({existing.DateTimeFrom}): ");
        if (DateTime.TryParse(Console.ReadLine(), out var date)) existing.DateTimeFrom = date;

        Console.Write($"Description ({existing.Description}): ");
        var desc = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(desc)) existing.Description = desc;

        _appointmentService.Update(id, existing);

        Console.WriteLine("Appointment updated! Press Enter to return.");
        Console.ReadLine();
    }

    private void DeleteAppointment()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Appointment ===");

        DisplayAppointments();
        Console.Write("Enter Appointment ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        if (_appointmentService.Delete(id))
        {
            Console.WriteLine("Appointment deleted successfully.");
        }
        else
        {
            Console.WriteLine("Appointment not found.");
        }

        Console.WriteLine("Press Enter to return.");
        Console.ReadLine();
    }
}
