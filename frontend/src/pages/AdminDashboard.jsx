import React from "react";
import { useNavigate } from "react-router-dom";

const AdminDashboard = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.clear();
    navigate("/");
  };

  return (
    <div className="container mt-5 text-center">
      <h1 className="mb-4">Panel de Administrador</h1>
      <p>Bienvenido, administrador del sistema.</p>
      <button className="btn btn-danger mt-4" onClick={handleLogout}>
        Cerrar sesión
      </button>
    </div>
  );
};

export default AdminDashboard;
