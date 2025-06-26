import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { http } from "../../services/httpClient/api";
import { ENDPOINTS } from "../../services/httpClient/endpoints";

const PatientDashboard = () => {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const fetchAppointments = async () => {
    try {
      const response = await http.get(ENDPOINTS.APPOINTMENTS.GET_MY_PENDING);
      setAppointments(response.data);
    } catch (error) {
      console.error("Error al obtener citas:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAppointments();
  }, []);

  const handleLogout = () => {
    localStorage.clear();
    navigate("/");
  };

  const handleScheduleAppointment = () => {
    navigate("/patient/book");
  };

  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container-fluid">
          <span className="navbar-brand">Panel del Paciente</span>
          <div className="d-flex">
            <button className="btn btn-outline-primary me-2" onClick={handleScheduleAppointment}>
              Agregar cita
            </button>
            <button className="btn btn-outline-danger" onClick={handleLogout}>
              Cerrar sesión
            </button>
          </div>
        </div>
      </nav>

      <div className="container mt-4">
        <h2>Mis citas programadas</h2>

        {loading ? (
          <p>Cargando...</p>
        ) : appointments.length === 0 ? (
          <div className="alert alert-info">No tienes citas programadas.</div>
        ) : (
          <div className="row">
            {appointments.map((appointment) => (
              <div className="col-md-6 mb-4" key={appointment.id}>
                <div className="card">
                  <div className="card-body">
                    <h5 className="card-title">Cita con: {appointment.doctorName}</h5>
                    <p className="card-text">
                      Fecha: {new Date(appointment.scheduledDate).toLocaleDateString()}<br />
                      Hora: {new Date(appointment.scheduledDate).toLocaleTimeString()}
                    </p>
                    <div className="d-flex justify-content-between">
                      <button className="btn btn-warning btn-sm">Reprogramar</button>
                      <button className="btn btn-danger btn-sm">Cancelar</button>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default PatientDashboard;