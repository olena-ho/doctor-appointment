using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Service.Interfaces;

public interface IAppointmentService
{
    Appointment Create(Appointment appointment);
    IEnumerable<Appointment> GetAll();
    Appointment? Get(int id);
    Appointment Update(int id, Appointment appointment);
    bool Delete(int id);
    void ShowInfo(Appointment appointment);
}
