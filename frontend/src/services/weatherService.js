import { http } from './httpClient/api';
import { ENDPOINTS } from './httpClient/endpoints';

export const weatherService = {
  getWeatherForecast: async () => {
    try {
      const response = await http.get(ENDPOINTS.WEATHER.FORECAST);
      console.log('Respuesta del servidor:', response); // Para debug
      console.log('Datos recibidos:', response.data); // Para debug
      return Array.isArray(response.data) ? response.data : [];
    } catch (error) {
      console.error('Error en weatherService:', error); // Para debug
      throw error;
    }
  }
};