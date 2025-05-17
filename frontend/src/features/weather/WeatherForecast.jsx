import { useState, useEffect } from 'react';
import { Card, Container, Row, Col, Alert } from 'react-bootstrap';
import { weatherService } from '../../services/weatherService';
import { ToastContainer } from 'react-toastify';
import { HttpErrorAlert } from '../../components/errors/HttpErrorAlert';
import { ConnectionErrorAlert } from '../../components/errors/ConnectionErrorAlert';
import 'react-toastify/dist/ReactToastify.css';

export const WeatherForecast = () => {
  const [forecasts, setForecasts] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  const fetchWeather = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await weatherService.getWeatherForecast();
      console.log('Datos recibidos en el componente:', data); // Para debug
      setForecasts(data);
    } catch (err) {
      console.error('Error en el componente:', err); // Para debug
      setError({
        status: err.response?.status,
        detail: err.message,
        isConnectionError: !err.response
      });
      setForecasts([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchWeather();
  }, []);

  if (loading) {
    return (
      <Container className="mt-4">
        <Alert variant="info">
          Cargando pronóstico del tiempo...
        </Alert>
      </Container>
    );
  }

  if (error) {
    return (
      <Container className="mt-4">
        {error.isConnectionError ? (
          <ConnectionErrorAlert retry={fetchWeather} />
        ) : (
          <HttpErrorAlert error={error} />
        )}
        <ToastContainer />
      </Container>
    );
  }

  return (
    <Container className="mt-4">
      <h2 className="mb-4">Pronóstico del Tiempo</h2>
      {forecasts.length === 0 ? (
        <Alert variant="warning">
          No hay datos de pronóstico disponibles
        </Alert>
      ) : (
        <Row>
          {forecasts.map((forecast, index) => (
            <Col key={index} md={4} className="mb-3">
              <Card>
                <Card.Body>
                  <Card.Title>{new Date(forecast.date).toLocaleDateString()}</Card.Title>
                  <Card.Text>
                    Temperatura: {forecast.temperatureC}°C / {forecast.temperatureF}°F
                    <br />
                    Resumen: {forecast.summary}
                  </Card.Text>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>
      )}
      <ToastContainer />
    </Container>
  );
};