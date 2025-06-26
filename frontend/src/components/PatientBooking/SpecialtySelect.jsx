import React, { useState, useEffect } from 'react';
import { http } from '../../services/httpClient/api';
import { ENDPOINTS } from '../../services/httpClient/endpoints';

const SpecialtySelect = ({ onSelect }) => {
  const [list, setList] = useState([]);

  useEffect(() => {
    http.get(ENDPOINTS.SPECIALTIES.GET_ALL).then(r => setList(r.data));
  }, []);

  return (
    <div className="mb-3">
      <label className="form-label">Especialidad</label>
      <select className="form-select" onChange={e => onSelect(e.target.value)}>
        <option value="">-- Seleccionar --</option>
        {list.map(s => (
          <option key={s.id} value={s.id}>{s.name}</option>
        ))}
      </select>
    </div>
  );
};

export default SpecialtySelect;