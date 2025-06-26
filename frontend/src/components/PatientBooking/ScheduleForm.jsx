import React from 'react';
import { http } from '../../services/httpClient/api';
import { ENDPOINTS } from '../../services/httpClient/endpoints';
import { useNavigate } from 'react-router-dom';

const ScheduleForm = ({ slot }) => {
  const navigate = useNavigate();
  const handleConfirm = async () => {
    try {
      const patientId = localStorage.getItem('patientId');
      console.log({
        doctorId: slot.doctorId,
        patientId,
        scheduleId: slot.scheduleId,
        duration: slot.duration,
        reasonForVisit: "Consulta general"
      });

      await http.post(ENDPOINTS.APPOINTMENTS.SCHEDULE, {
        doctorId: slot.doctorId,
        patientId,
        scheduleId: slot.scheduleId,
        duration: slot.duration,
        reasonForVisit: "Consulta general"
      });

      navigate('/patient/dashboard');
    } catch (error) {
      console.error('Error al agendar cita:', error.response?.data || error.message);
      alert('Error al agendar cita: ' + (error.response?.data?.message || error.message));
    }
  };


  return (
    <div className="mt-3">
      <h5>Confirmar cita:</h5>
      <p>{new Date(slot.startDate).toLocaleString()}</p>
      <button className="btn btn-success" onClick={handleConfirm}>
        Confirmar cita
      </button>
    </div>
  );
};

export default ScheduleForm;