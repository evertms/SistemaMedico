import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const doctorService = {
  // Agregar un nuevo médico
  addDoctor: async (payload) => {
    const response = await http.post(ENDPOINTS.DOCTORS.ADD, payload);
    return response.data;
  },

  // Editar información de un médico
  editDoctor: async (doctorId, payload) => {
    const url = ENDPOINTS.DOCTORS.EDIT(doctorId);
    const response = await http.put(url, payload);
    return response.data;
  },

  // Dar de baja a un médico
  deactivateDoctor: async (doctorId) => {
    const url = ENDPOINTS.DOCTORS.DEACTIVATE(doctorId);
    const response = await http.delete(url);
    return response.data;
  },

  // Obtener médicos disponibles
  getAvailableDoctors: async () => {
    const response = await http.get(ENDPOINTS.DOCTORS.AVAILABLE_DOCTORS);
    return response.data;
  },

  // Obtener horarios disponibles de un médico
  getAvailableSchedules: async (doctorId) => {
    const url = ENDPOINTS.DOCTORS.AVAILABLE_SCHEDULES(doctorId);
    const response = await http.get(url);
    return response.data;
  },
};

export default doctorService;