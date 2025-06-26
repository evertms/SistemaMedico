import React, { useEffect, useState } from 'react';
import SpecialtySelect from '../../components/PatientBooking/SpecialtySelect';
import DoctorSelect from '../../components/PatientBooking/DoctorSelect';
import AvailabilityList from '../../components/PatientBooking/AvailabilityList';
import ScheduleForm from '../../components/PatientBooking/ScheduleForm';

const PatientBooking = () => {
  const [specialtyId, setSpecialtyId] = useState(null);
  const [doctorId, setDoctorId] = useState(null);
  const [slot, setSlot] = useState(null);

  return (
    <div className="container mt-5">
      <h2>Reservar cita</h2>
      <SpecialtySelect onSelect={setSpecialtyId} />
      {specialtyId && <DoctorSelect specialtyId={specialtyId} onSelect={setDoctorId} />}
      {doctorId && <AvailabilityList doctorId={doctorId} onSelect={setSlot} />}
      {slot && <ScheduleForm slot={slot} />}
    </div>
  );
};

export default PatientBooking;