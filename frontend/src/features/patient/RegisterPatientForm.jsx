// src/features/patient/RegisterPatientForm.jsx
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import patientService from "../../services/patientService";

const RegisterPatientForm = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [dob, setDob] = useState("");
  const [phone, setPhone] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  // Recuperar userId temporal
  const userId = localStorage.getItem("tempUserId");

  useEffect(() => {
    if (!userId) navigate("/register");
  }, [userId, navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {
      await patientService.registerPatient({
        userId,
        firstName,
        lastName,
        dateOfBirth: dob,
        phoneNumber: phone
      });

      // Limpio y redirijo a login
      localStorage.removeItem("tempUserId");
      navigate("/login");
    } catch (err) {
      setError(err.message || "Error registrando datos de paciente");
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: 400 }}>
      <h2 className="mb-4">Registro de Paciente</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Nombre</label>
          <input
            type="text"
            className="form-control"
            value={firstName}
            onChange={e => setFirstName(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Apellido</label>
          <input
            type="text"
            className="form-control"
            value={lastName}
            onChange={e => setLastName(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Fecha de nacimiento</label>
          <input
            type="date"
            className="form-control"
            value={dob}
            onChange={e => setDob(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Teléfono</label>
          <input
            type="text"
            className="form-control"
            value={phone}
            onChange={e => setPhone(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-success w-100">
          Completar Registro
        </button>
      </form>
    </div>
  );
};

export default RegisterPatientForm;
