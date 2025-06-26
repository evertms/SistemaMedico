import React from 'react';
import { http } from '../../services/httpClient/api';
import { ENDPOINTS } from '../../services/httpClient/endpoints';
import { useNavigate } from 'react-router-dom';

const ScheduleForm = ({ slot }) => {
  const navigate = useNavigate();

  const handleConfirm = async () => {
    const patientId = localStorage.getItem('patientId');
    await http.post(ENDPOINTS.APPOINTMENTS.SCHEDULE, {
      doctorId: slot.doctorId,
      patientId,
      scheduleId: slot.id
    });
    navigate('/patient/dashboard');
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