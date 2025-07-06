using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    void ShowInfo(Appointment appointment);
}
