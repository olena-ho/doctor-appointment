using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Data.Repositories;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Service.Interfaces;

namespace DoctorAppointmentDemo.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public Appointment Create(Appointment appointment) => _appointmentRepository.Create(appointment);

        public bool Delete(int id) => _appointmentRepository.Delete(id);

        public Appointment? Get(int id) => _appointmentRepository.GetById(id);

        public IEnumerable<Appointment> GetAll() => _appointmentRepository.GetAll();

        public void ShowInfo(Appointment appointment)
        {
            _appointmentRepository.ShowInfo(appointment);
        }

        public Appointment Update(int id, Appointment appointment) => _appointmentRepository.Update(id, appointment);
    }
}
