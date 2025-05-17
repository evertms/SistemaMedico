import { Alert } from 'react-bootstrap';

export const ConnectionErrorAlert = ({ retry = null }) => {
  return (
    <Alert variant="warning" className="mt-3">
      <Alert.Heading>Error de Conexión</Alert.Heading>
      <p>
        No se pudo establecer conexión con el servidor. Por favor, verifique su conexión a internet.
      </p>
      {retry && (
        <div className="d-flex justify-content-end">
          <button 
            onClick={retry} 
            className="btn btn-outline-warning"
          >
            Reintentar
          </button>
        </div>
      )}
    </Alert>
  );
};