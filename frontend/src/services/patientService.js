import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const patientService = {
  // Registrar un nuevo paciente
  registerPatient: async (payload) => {
    const response = await http.post(ENDPOINTS.PATIENTS.REGISTER, payload);
    return response.data;
  },

  // Dar de baja/unlink a un paciente
  unlinkPatient: async (patientId) => {
    const url = ENDPOINTS.PATIENTS.UNLINK(patientId);
    const response = await http.delete(url);
    return response.data;
  },
};

export default patientService;