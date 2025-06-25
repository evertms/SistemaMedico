export function setAuthData({ token, userId, role, email, doctorId, patientId }) {
  localStorage.setItem('token', token);
  localStorage.setItem('userId', userId);
  localStorage.setItem('role', role);
  localStorage.setItem('email', email);
  if (doctorId) localStorage.setItem('doctorId', doctorId);
  if (patientId) localStorage.setItem('patientId', patientId);
}
