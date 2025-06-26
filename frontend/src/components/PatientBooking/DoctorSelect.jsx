import React, { useState, useEffect } from 'react';
import { http } from '../../services/httpClient/api';
import { ENDPOINTS } from '../../services/httpClient/endpoints';

const DoctorSelect = ({ specialtyId, onSelect }) => {
  const [list, setList] = useState([]);

  useEffect(() => {
    http.get(ENDPOINTS.SPECIALTIES.GET_DOCTORS_BY_SPECIALTY(specialtyId))
      .then(r => setList(r.data));
  }, [specialtyId]);

  return (
    <div className="mb-3">
      <label className="form-label">Doctor</label>
      <select className="form-select" onChange={e => onSelect(e.target.value)}>
        <option value="">-- Seleccionar --</option>
        {list.map(d => (
          <option key={d.id} value={d.id}>{d.fullName}</option>
        ))}
      </select>
    </div>
  );
};

export default DoctorSelect;