import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import userService from "../../services/userService";

const RegisterUserForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phone, setPhone] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const userId = await userService.registerUser({
        email,
        password,      // El API hará el hash
        phoneNumber: phone,
        role: 0 // Patient or defecto
      });

      // Guardar el userId para el siguiente paso
      localStorage.setItem("tempUserId", userId);
      navigate("/register/patient");
    } catch (err) {
      setError(err.message || "Error registrando usuario");
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: 400 }}>
      <h2 className="mb-4">Registro de Usuario</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Correo</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Contraseña</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={e => setPassword(e.target.value)}
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
        <button type="submit" className="btn btn-primary w-100">
          Siguiente: Datos de Paciente
        </button>
      </form>
    </div>
  );
};

export default RegisterUserForm;
