import { Alert } from 'react-bootstrap';

export const HttpErrorAlert = ({ error }) => {
  const getErrorMessage = (status) => {
    switch (status) {
      case 400:
        return 'La solicitud es incorrecta. Por favor, revise los datos enviados.';
      case 401:
        return 'No está autorizado para realizar esta acción.';
      case 403:
        return 'No tiene permisos para acceder a este recurso.';
      case 404:
        return 'El recurso solicitado no fue encontrado.';
      case 500:
        return 'Error interno del servidor. Por favor, intente más tarde.';
      default:
        return 'Ha ocurrido un error inesperado.';
    }
  };

  if (!error) return null;

  return (
    <Alert variant="danger" className="mt-3">
      <Alert.Heading>Error {error.status}</Alert.Heading>
      <p>{getErrorMessage(error.status)}</p>
      {error.detail && (
        <p className="mb-0">
          <small>Detalles: {error.detail}</small>
        </p>
      )}
    </Alert>
  );
};