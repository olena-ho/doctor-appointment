using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;

namespace MyDoctorAppointment.Data.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        void ShowInfo(Doctor doctor);
    }
}
