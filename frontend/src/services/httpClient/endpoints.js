export const ENDPOINTS = {
  WEATHER: {
    FORECAST: '/weatherforecast',
  },
  APPOINTMENTS: {
    SCHEDULE: '/api/Appointments/schedule', 
    CANCEL: '/api/Appointments/cancel',
    RESCHEDULE: '/api/Appointments/reschedule',
    GET_BY_DOCTOR: (doctorId) => `/api/Appointments/doctor/${doctorId}`,
    GET_MY_PENDING: '/api/Appointments/my-pending'
  }, 
  AUTH: {
    LOGIN: '/api/Auth/login',
  }, 
  DOCTORS: {
    ADD: '/api/Doctors',
    EDIT: (doctorId) => `/api/Doctors/${doctorId}`,
    DEACTIVATE: (doctorId) => `/api/Doctors/${doctorId}`,
    AVAILABLE_DOCTORS: '/api/Doctors',
    AVAILABLE_SCHEDULES: (doctorId) => `/api/Doctors/${doctorId}/schedules`,
  },
  MEDICAL_RECORDS: {
    CREATE_NOTE: (patientId) => `/api/MedicalRecords/${patientId}/note`,
    CREATE_DIAGNOSIS: (patientId) => `/api/MedicalRecords/${patientId}/diagnosis`,
    GET_BY_PATIENT: (patientId, doctorId) =>
      `/api/MedicalRecords/${patientId}${doctorId ? `?doctorId=${doctorId}` : ''}`,
    GET_MY_MEDICAL_RECORD: 'api/MedicalRecords/me',
    DOWNLOAD_MY_MEDICAL_RECORD: '/api/MedicalRecords/me/download'
  },
  PATIENTS: {
    REGISTER: '/api/Patients/register',
    UNLINK: (patientId) => `/api/Patients/${patientId}/unlink`,
  },
  SCHEDULES: {
    CREATE_AVAILABILITY: '/api/Schedules/availability'
  },
  SPECIALTIES: {
    GET_ALL: '/api/Specialties',
    GET_DOCTORS_BY_SPECIALTY: (specialtyId) => `/api/Specialties/${specialtyId}/doctors`,
  },
  USERS:{
    REGISTER: '/api/Users/register'
  }
};