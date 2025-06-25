import { http } from '../services/httpClient/api';
import { ENDPOINTS } from '../services/httpClient/endpoints';

const medicalRecordService = {
  // Crear nota médica
  createMedicalNote: async (patientId, payload) => {
    const url = ENDPOINTS.MEDICAL_RECORDS.CREATE_NOTE(patientId);
    const response = await http.post(url, payload);
    return response.data;
  },

  // Crear diagnóstico
  createDiagnosis: async (patientId, payload) => {
    const url = ENDPOINTS.MEDICAL_RECORDS.CREATE_DIAGNOSIS(patientId);
    const response = await http.post(url, payload);
    return response.data;
  },

  // Obtener historial médico de un paciente (con filtro por médico)
  getPatientMedicalRecord: async (patientId, doctorId = null) => {
    const url = ENDPOINTS.MEDICAL_RECORDS.GET_BY_PATIENT(patientId, doctorId);
    const response = await http.get(url);
    return response.data;
  },

  // Obtener mi propio historial médico
  getMyMedicalRecord: async () => {
    const url = ENDPOINTS.MEDICAL_RECORDS.GET_MY_MEDICAL_RECORD;
    const response = await http.get(url);
    return response.data;
  },

  // Descargar mi historial médico como PDF
  downloadMyMedicalRecord: async () => {
    const url = ENDPOINTS.MEDICAL_RECORDS.DOWNLOAD_MY_MEDICAL_RECORD;
    const response = await http.get(url, {
      responseType: 'blob', // Importante para archivos binarios
    });
    return response;
  },
};

export default medicalRecordService;