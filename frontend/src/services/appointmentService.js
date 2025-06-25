import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const appointmentService = {
  // Programar una cita médica
  scheduleAppointment: async (payload) => {
    const response = await http.post(ENDPOINTS.APPOINTMENTS.SCHEDULE, payload);
    return response.data;
  },

  // Cancelar una cita médica
  cancelAppointment: async (payload) => {
    const response = await http.post(ENDPOINTS.APPOINTMENTS.CANCEL, payload);
    return response.data;
  },

  // Reprogramar una cita médica
  rescheduleAppointment: async (payload) => {
    const response = await http.post(ENDPOINTS.APPOINTMENTS.RESCHEDULE, payload);
    return response.data;
  },

  // Obtener todas las citas de un médico
  getDoctorAppointments: async (doctorId) => {
    const url = ENDPOINTS.APPOINTMENTS.GET_BY_DOCTOR(doctorId);
    const response = await http.get(url);
    return response.data;
  },
};

export default appointmentService;