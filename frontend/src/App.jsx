import LoginForm from './features/auth/LoginForm'
import { Routes, Route } from 'react-router'
import 'bootstrap/dist/css/bootstrap.min.css'
import { WeatherForecast } from './features/weather/WeatherForecast'
import LandingPage from './pages/LandingPage'
import AdminDashboard from './pages/AdminDashboard'
import PatientBooking from './pages/patient/PatientBooking'
import RegisterUserForm from './features/auth/RegisterUserForm';
import RegisterPatientForm from './features/patient/RegisterPatientForm';
import PatientDashboard from './pages/patient/PatientDashboard'


function App() {
  return (
    <Routes>
      <Route path="/" element={<LandingPage />} />
      <Route path="/login" element={<LoginForm />} />
      <Route path="/weather" element={<WeatherForecast />} />
      <Route path="/register" element={<RegisterUserForm />} />
      <Route path="/register/patient" element={<RegisterPatientForm />} />
      <Route path="/admin/dashboard" element={<AdminDashboard />}/>
      {<Route path="/patient/dashboard" element={<PatientDashboard />} />}
      <Route path="/patient/book" element={<PatientBooking />} />

    </Routes>
  )
}

export default App
