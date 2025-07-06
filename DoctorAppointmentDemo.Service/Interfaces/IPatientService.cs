using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces;

public interface IPatientService
{
    Patient Create(Patient patient);
    IEnumerable<Patient> GetAll();
    Patient? Get(int id);
    Patient Update(int id, Patient patient);
    bool Delete(int id);
    void ShowInfo(Patient patient);
}
