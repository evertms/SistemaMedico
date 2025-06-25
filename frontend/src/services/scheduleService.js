import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const scheduleService = {
  // Crear disponibilidad para un médico
  createDoctorAvailability: async (payload) => {
    const response = await http.post(ENDPOINTS.SCHEDULES.CREATE_AVAILABILITY, payload);
    return response.data;
  },
};

export default scheduleService;