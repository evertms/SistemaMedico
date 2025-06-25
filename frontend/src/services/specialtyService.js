import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const specialtyService = {
  // Obtener todas las especialidades
  getAllSpecialties: async () => {
    const response = await http.get(ENDPOINTS.SPECIALTIES.GET_ALL);
    return response.data;
  },

  // Obtener médicos por especialidad
  getDoctorsBySpecialty: async (specialtyId) => {
    const url = ENDPOINTS.SPECIALTIES.GET_DOCTORS_BY_SPECIALTY(specialtyId);
    const response = await http.get(url);
    return response.data;
  },
};

export default specialtyService;