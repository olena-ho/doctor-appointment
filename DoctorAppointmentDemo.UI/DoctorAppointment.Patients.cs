using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Domain.Enums;
using DoctorAppointmentDemo.UI.Enums;

namespace DoctorAppointmentDemo.UI;

public partial class DoctorAppointment
{
    private void ManagePatients()
    {
        while (true)
        {
            Console.WriteLine("===== Patients =====");

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Return to main menu");
            Console.WriteLine("2. See all patients");
            Console.WriteLine("3. Add a new patient");
            Console.WriteLine("4. Update an existing patient");
            Console.WriteLine("5. Remove a patient");
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
                    DisplayPatients();
                    break;

                case ActionMenu.Add:
                    AddPatient();
                    break;

                case ActionMenu.Update:
                    UpdatePatient();
                    break;

                case ActionMenu.Delete:
                    DeletePatient();
                    break;

                default:
                    Console.WriteLine("Unknown option. Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void DisplayPatients()
    {
        Console.WriteLine("=== The List of All Patients ===");

        var patients = _patientService.GetAll();

        if (!patients.Any())
        {
            Console.WriteLine("No patients found.");
            return;
        }

        foreach (var p in patients)
        {
            _patientService.ShowInfo(p);
        }
        Console.WriteLine("\n");
    }

    private void AddPatient()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Patient ===");

        var patient = new Patient();

        Console.Write("Name: ");
        patient.Name = Console.ReadLine() ?? "";

        Console.Write("Surname: ");
        patient.Surname = Console.ReadLine() ?? "";

        Console.Write("Phone (optional): ");
        patient.Phone = Console.ReadLine();

        Console.Write("Email (optional): ");
        patient.Email = Console.ReadLine();

        Console.Write("Address: ");
        patient.Address = Console.ReadLine();

        Console.Write("Additional Info (optional): ");
        patient.AdditionalInfo = Console.ReadLine();

        Console.WriteLine("Select Illness Type:");
        foreach (var type in Enum.GetValues(typeof(IllnessTypes)))
        {
            Console.WriteLine($"{(int)type}. {type}");
        }

        Console.Write("Choice: ");
        patient.IllnessType = Enum.TryParse<IllnessTypes>(Console.ReadLine(), out var illness)
            ? illness
            : IllnessTypes.Infection;

        _patientService.Create(patient);
        Console.WriteLine("Patient added successfully! Press Enter to return.");
        Console.ReadLine();
    }

    private void UpdatePatient()
    {
        Console.Clear();
        Console.WriteLine("=== Update Patient ===");

        DisplayPatients();
        Console.Write("Enter Patient ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        var existing = _patientService.Get(id);
        if (existing == null)
        {
            Console.WriteLine("Patient not found. Press Enter to return.");
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

        Console.Write($"Address ({existing.Address}): ");
        var address = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(address)) existing.Address = address;

        Console.Write($"Additional Info ({existing.AdditionalInfo}): ");
        var add = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(add)) existing.AdditionalInfo = add;

        Console.WriteLine("Select Illness Type:");
        foreach (var type in Enum.GetValues(typeof(IllnessTypes)))
        {
            Console.WriteLine($"{(int)type}. {type}");
        }

        Console.Write($"Choice ({existing.IllnessType}): ");
        if (Enum.TryParse<IllnessTypes>(Console.ReadLine(), out var illnessType))
            existing.IllnessType = illnessType;

        _patientService.Update(id, existing);

        Console.WriteLine("Patient updated! Press Enter to return.");
        Console.ReadLine();
    }

    private void DeletePatient()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Patient ===");

        DisplayPatients();
        Console.Write("Enter Patient ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) return;

        if (_patientService.Delete(id))
        {
            Console.WriteLine("Patient deleted successfully.");
        }
        else
        {
            Console.WriteLine("Patient not found.");
        }

        Console.WriteLine("Press Enter to return.");
        Console.ReadLine();
    }
}
