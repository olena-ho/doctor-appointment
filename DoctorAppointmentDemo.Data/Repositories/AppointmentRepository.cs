using System.Numerics;
using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public override string Path { get; set; }
    public override int LastId { get; set; }

    public AppointmentRepository()
    {
        dynamic config = ReadFromAppSettings();
        string basePath = config.Database.BasePath;
        string fileName = config.Database.Appointments.Path;

        Path = System.IO.Path.Combine(AppContext.BaseDirectory, basePath.ToString(), fileName.ToString());
        LastId = config.Database.Appointments.LastId;
    }

    public override void ShowInfo(Appointment a)
    {
        Console.WriteLine($"----- Appointment ID: {a.Id} -----\nDoctor: {a.Doctor.Name} {a.Doctor.Surname}, Patient: {a.Patient.Name} {a.Patient.Surname}, Date: {a.DateTimeFrom}, Description: {a.Description}");
    }

    protected override void SaveLastId()
    {
        dynamic result = ReadFromAppSettings();
        result.Database.Appointments.LastId = LastId;
        File.WriteAllText(Constants.AppSettingsPath, result.ToString());
    }
}