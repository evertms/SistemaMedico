import { http } from '../services/httpClient/api.js';
import { ENDPOINTS } from '../services/httpClient/endpoints.js';

import { setAuthData } from './authStorage';

export async function loginUser(email, password) {
  const response = await http.post(ENDPOINTS.AUTH.LOGIN, { email, password });
  setAuthData(response.data);
  return { token: response.data.token, role: response.data.role };
}
