﻿using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DatabaseContext _db;
        public Repository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _db.Patients
                .Include(p => p.Appointments).ThenInclude(a => a.Doctor)
                .Include(p => p.Appointments).ThenInclude(a => a.Prescription).ThenInclude(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatient(int id)
        {
            return await _db.Patients
                .Include(p => p.Appointments).ThenInclude(a => a.Doctor)
                .Include(p => p.Appointments).ThenInclude(a => a.Prescription).ThenInclude(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _db.Doctors
                .Include(d => d.Appointments).ThenInclude(a => a.Patient)
                .Include(p => p.Appointments).ThenInclude(a => a.Prescription).ThenInclude(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctor(int id)
        {
            return await _db.Doctors
                .Include(d => d.Appointments).ThenInclude(a => a.Patient)
                .Include(p => p.Appointments).ThenInclude(a => a.Prescription).ThenInclude(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        /*public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _db.Appointments.Where(a => a.DoctorId==id).ToListAsync();
        }*/

        public async Task<IEnumerable<Prescription>> GetPrescriptions()
        {
            return await _db.Prescriptions.Include(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine).ToListAsync();
        }

        public async Task<Prescription?> GetPrescription(int id)
        {
            return await _db.Prescriptions.Include(p => p.MedicinePrescriptions).ThenInclude(mp => mp.Medicine).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
