import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { http } from "../../services/httpClient/api";
import { ENDPOINTS } from "../../services/httpClient/endpoints";

const ReprogramAppointment = () => {
  const { state } = useLocation();
  const navigate = useNavigate();
  const appointment = state?.appointment;

  const [slots, setSlots] = useState([]);
  const patientId = localStorage.getItem("patientId");

  useEffect(() => {
    if (!appointment) {
      alert("No se ha proporcionado una cita");
      navigate("/patient/dashboard");
      return;
    }
    http.get(ENDPOINTS.DOCTORS.AVAILABLE_SCHEDULES(appointment.doctorId))
      .then(res => setSlots(res.data));
  }, [appointment]);

  console.log(appointment);
  const handleSelect = async (slot) => {
    try {
      await http.post(ENDPOINTS.APPOINTMENTS.RESCHEDULE, {
        appointmentId: appointment.id,
        patientId,
        newScheduleId: slot.scheduleId,
        newDate: slot.startDate
      });

      alert("Cita reprogramada exitosamente");
      navigate("/patient/dashboard");
    } catch (err) {
      console.error(err);
      alert("Error al reprogramar cita");
    }
  };

  return (
    <div className="container mt-4">
      <h2>Reprogramar cita con {appointment?.doctorName}</h2>
      {slots.length === 0 ? (
        <p>No hay horarios disponibles.</p>
      ) : (
        <ul className="list-group">
          {slots.map(slot => (
            <li
              key={slot.scheduleId}
              className="list-group-item d-flex justify-content-between align-items-center"
              style={{ cursor: "pointer" }}
              onClick={() => handleSelect(slot)}
            >
              {new Date(slot.startDate).toLocaleString()}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ReprogramAppointment;
