using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Data.Repositories;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Service.Interfaces;

namespace DoctorAppointmentDemo.Service.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService()
    {
        _patientRepository = new PatientRepository();
    }

    public Patient Create(Patient patient) => _patientRepository.Create(patient);

    public bool Delete(int id) => _patientRepository.Delete(id);

    public Patient? Get(int id) => _patientRepository.GetById(id);

    public IEnumerable<Patient> GetAll() => _patientRepository.GetAll();

    public Patient Update(int id, Patient patient) => _patientRepository.Update(id, patient);

    public void ShowInfo(Patient patient)
    {
        _patientRepository.ShowInfo(patient);
    }
}
