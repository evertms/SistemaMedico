import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

api.interceptors.request.use(request => {
  console.log('URL de la solicitud:', request.url);
  console.log('URL completa:', request.baseURL + request.url);
  return request;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      switch (error.response.status) {
        case 404:
          throw new Error('Recurso no encontrado');
        case 500:
          throw new Error('Error interno del servidor');
        default:
          throw new Error('Error en la solicitud');
      }
    } else if (error.request) {
      throw new Error('No se pudo conectar con el servidor');
    }
    throw error;
  }
);

export const http = {
  get: (url, config = {}) => api.get(url, config),
  post: (url, data = {}, config = {}) => api.post(url, data, config),
  put: (url, data = {}, config = {}) => api.put(url, data, config),
  delete: (url, config = {}) => api.delete(url, config),
};