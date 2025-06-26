import React, { useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { loginUser } from "../../services/authService";

const LoginForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  // Mapa de roles visibles (URL) -> reales (backend)
  const roleMap = {
    Admin: "Administrador",
    Doctor: "Doctor",
    Patient: "Patient",
  };

  const requiredRole = searchParams.get("role"); // Ej: "Doctor"
  const expectedRole = roleMap[requiredRole];    // Ej: "Doctor" o "Administrador"

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const { token, role } = await loginUser(email, password);

      // Si hay rol requerido y no coincide con el rol del usuario → error
      if (expectedRole && role !== expectedRole) {
        setError(`Este usuario tiene el rol "${role}" y no puede iniciar sesión como "${requiredRole}".`);
        return;
      }

      // Redirección según el rol
      if (role === "Administrador") navigate("/admin/dashboard");
      else if (role === "Doctor") navigate("/doctor/dashboard");
      else if (role === "Patient") navigate("/patient/dashboard");
      else navigate("/dashboard"); // fallback

    } catch (err) {
      setError(err.message || "Credenciales inválidas");
    }
  };

  return (
    <form onSubmit={handleSubmit} className="container mt-5" style={{ maxWidth: "400px" }}>
      <h2 className="mb-3 text-center">Iniciar sesión {requiredRole && `como ${requiredRole}`}</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <div className="mb-3">
        <input
          type="email"
          placeholder="Correo electrónico"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="form-control"
        />
      </div>
      <div className="mb-3">
        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="form-control"
        />
      </div>
      <button type="submit" className="btn btn-primary w-100">
        Iniciar sesión
      </button>

      <button
        type="button"
        className="btn btn-secondary w-100 mt-2"
        onClick={() => navigate("/")}
      >
        Volver a la página principal
      </button>

    </form>
  );
};

export default LoginForm;
