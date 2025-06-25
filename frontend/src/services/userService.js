import { http } from './httpClient/api';
import { ENDPOINTS } from './httpClient/endpoints';

const userService = {
  // Registrar un nuevo usuario
  registerUser: async (payload) => {
    const response = await http.post(ENDPOINTS.USERS.REGISTER, payload);
    return response.data;
  },
};

export default userService;