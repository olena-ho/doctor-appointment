using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    void ShowInfo(Patient patient);
}
