using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Repositories;
using DoctorAppointmentDemo.Domain.Entities;
using MyDoctorAppointment.Data.Interfaces;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository()
        {
            dynamic config = ReadFromAppSettings();

            string basePath = config.Database.BasePath;
            string fileName = config.Database.Doctors.Path;

            Path = System.IO.Path.Combine(AppContext.BaseDirectory, basePath.ToString(), fileName.ToString());
            LastId = config.Database.Doctors.LastId;
        }

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine($"----- Doctor ID: {doctor.Id} -----\nName: {doctor.Name} {doctor.Surname}, Type: {doctor.DoctorType}, Experience: {doctor.Experience}\n"
            );
        }


        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}
