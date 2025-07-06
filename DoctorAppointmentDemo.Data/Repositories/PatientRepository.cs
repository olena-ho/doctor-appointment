using System.Numerics;
using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public override string Path { get; set; }
    public override int LastId { get; set; }

    public PatientRepository()
    {
        dynamic config = ReadFromAppSettings();
        string basePath = config.Database.BasePath;
        string fileName = config.Database.Patients.Path;

        Path = System.IO.Path.Combine(AppContext.BaseDirectory, basePath.ToString(), fileName.ToString());
        LastId = config.Database.Patients.LastId;
    }

    public override void ShowInfo(Patient patient)
    {
        Console.WriteLine($"----- Patient ID: {patient.Id} -----\nName:{patient.Name} {patient.Surname}, Illness: {patient.IllnessType}, Email: {patient.Email}");
    }

    protected override void SaveLastId()
    {
        dynamic result = ReadFromAppSettings();
        result.Database.Patients.LastId = LastId;
        File.WriteAllText(Constants.AppSettingsPath, result.ToString());
    }
}
