import React, { useState, useEffect } from 'react';
import { http } from '../../services/httpClient/api';
import { ENDPOINTS } from '../../services/httpClient/endpoints';

const AvailabilityList = ({ doctorId, onSelect }) => {
  const [slots, setSlots] = useState([]);

  useEffect(() => {
    http.get(ENDPOINTS.DOCTORS.AVAILABLE_SCHEDULES(doctorId))
      .then(r => setSlots(r.data));
  }, [doctorId]);

  return (
    <div className="mb-3">
      <label className="form-label">Horarios disponibles</label>
      <ul className="list-group">
        {slots.map(slot => (
          <li
            key={slot.id}
            className="list-group-item"
            onClick={() => onSelect(slot)}
            style={{ cursor: 'pointer' }}
          >
            {new Date(slot.startDate).toLocaleString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AvailabilityList;