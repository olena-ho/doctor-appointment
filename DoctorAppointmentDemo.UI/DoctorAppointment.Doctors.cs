using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Domain.Enums;
using DoctorAppointmentDemo.UI.Enums;

namespace DoctorAppointmentDemo.UI;

public partial class DoctorAppointment
{
    private void ManageDoctors()
    {
        while (true)
        {
            Console.WriteLine("===== Doctors =====");

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Return to main menu");
            Console.WriteLine("2. See all doctors");
            Console.WriteLine("3. Add a new doctor");
            Console.WriteLine("4. Update an existing doctor");
            Console.WriteLine("5. Remove a doctor");
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
                    DisplayDoctors();
                    break;

                case ActionMenu.Add:
                    AddDoctor();
                    break;

                case ActionMenu.Update:
                    UpdateDoctor();
                    break;

                case ActionMenu.Delete:
                    DeleteDoctor();
                    break;

                default:
                    Console.WriteLine("Unknown option. Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void DisplayDoctors()
    {
        var doctors = _doctorService.GetAll();

        if (!doctors.Any())
        {
            Console.WriteLine("No doctors found.");
            return;
        }

        foreach (var doc in doctors)
        {
            _doctorService.ShowInfo(doc);
        }
        Console.WriteLine("\n");
    }

    private void AddDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Doctor ===");

        var doctor = new Doctor();

        Console.Write("Name: ");
        doctor.Name = Console.ReadLine() ?? "";

        Console.Write("Surname: ");
        doctor.Surname = Console.ReadLine() ?? "";

        Console.Write("Phone (optional): ");
        doctor.Phone = Console.ReadLine();

        Console.Write("Email (optional): ");
        doctor.Email = Console.ReadLine();

        Console.Write("Experience (years): ");
        doctor.Experience = byte.TryParse(Console.ReadLine(), out var exp) ? exp : (byte)0;

        Console.Write("Salary: ");
        doctor.Salary = decimal.TryParse(Console.ReadLine(), out var salary) ? salary : 0;

        Console.WriteLine("Select Doctor Type:");
        foreach (var type in Enum.GetValues(typeof(DoctorTypes)))
        {
            Console.WriteLine($"{(int)type}. {type}");
        }

        Console.Write("Choice: ");
        doctor.DoctorType = Enum.TryParse<DoctorTypes>(Console.ReadLine(), out var doctorType) ? doctorType : DoctorTypes.FamilyDoctor;

        _doctorService.Create(doctor);
        Console.WriteLine("Doctor added successfully! Press Enter to return.");
        Console.ReadLine();
    }


    private void UpdateDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Update Doctor ===");

        DisplayDoctors();
        Console.Write("Enter Doctor ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        var existing = _doctorService.Get(id);
        if (existing == null)
        {
            Console.WriteLine("Doctor not found. Press Enter to return.");
            Console.ReadLine();
            return;
        }

        Console.Write($"Name ({existing.Name}): ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name)) existing.Name = name;

        Console.Write($"Surname ({existing.Surname}): ");
        var surname = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(surname)) existing.Surname = surname;

        Console.Write($"Phone ({existing.Phone}): ");
        var phone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phone)) existing.Phone = phone;

        Console.Write($"Email ({existing.Email}): ");
        var email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email)) existing.Email = email;

        Console.Write($"Experience ({existing.Experience}): ");
        if (byte.TryParse(Console.ReadLine(), out var exp)) existing.Experience = exp;

        Console.Write($"Salary ({existing.Salary}): ");
        if (decimal.TryParse(Console.ReadLine(), out var salary)) existing.Salary = salary;

        Console.WriteLine("Select Doctor Type:");
        foreach (var type in Enum.GetValues(typeof(DoctorTypes)))
        {
            Console.WriteLine($"{(int)type}. {type}");
        }

        Console.Write($"Choice ({existing.DoctorType}): ");
        if (Enum.TryParse<DoctorTypes>(Console.ReadLine(), out var doctorType))
            existing.DoctorType = doctorType;

        _doctorService.Update(id, existing);

        Console.WriteLine("Doctor updated! Press Enter to return.");
        Console.ReadLine();
    }

    private void DeleteDoctor()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Doctor ===");

        DisplayDoctors();
        Console.Write("Enter Doctor ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        if (_doctorService.Delete(id))
        {
            Console.WriteLine("Doctor deleted successfully.");
        }
        else
        {
            Console.WriteLine("Doctor not found.");
        }

        Console.WriteLine("Press Enter to return.");
        Console.ReadLine();
    }

}
