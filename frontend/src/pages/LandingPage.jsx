import React from "react";
import { useNavigate } from "react-router-dom";

const LandingPage = () => {
  const navigate = useNavigate();

  const handleLoginRedirect = (role) => {
    navigate(`/login?role=${role}`);
  };

  const handleRegisterRedirect = () => {
    navigate("/register");
  };

  return (
    <div className="container text-center mt-5">
      <h1 className="mb-4">Bienvenido al Sistema Médico</h1>
      <p className="lead mb-5">Selecciona una opción para continuar:</p>
      <div className="d-grid gap-3 col-6 mx-auto">
        <button
          className="btn btn-primary"
          onClick={() => handleLoginRedirect("Admin")}
        >
          Iniciar como Administrador
        </button>
        <button
          className="btn btn-success"
          onClick={() => handleLoginRedirect("Patient")}
        >
          Iniciar como Paciente
        </button>
        <button
          className="btn btn-info"
          onClick={() => handleLoginRedirect("Doctor")}
        >
          Iniciar como Médico
        </button>

        <div>
          <p className="mb-2">¿Aún no tienes cuenta?</p>
          <button
            className="btn btn-outline-secondary"
            onClick={handleRegisterRedirect}
          >
            Registrarse como Paciente
          </button>
        </div>
      </div>
    </div>
  );
};

export default LandingPage;
