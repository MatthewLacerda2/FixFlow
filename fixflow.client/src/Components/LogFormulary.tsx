import { useState } from 'react';
import LogAppointment from '../Data/LogAppointment';

function LogFormulary() {

  const [logAppointment, setLogAppointment] = useState<LogAppointment>(new LogAppointment());
  const [buttonColor, setButtonColor] = useState('yellow');

  const handleSend = () => {
    if (logAppointment.clientId !== null) {
      setButtonColor('green');
      // Lógica para enviar dados
    }
  };

  const handleChange = (field: keyof LogAppointment, value: string | number | Date) => {
    setLogAppointment(prevAppointment => ({
      ...prevAppointment,
      [field]: value
    }));
  };

  return (
    <div>
      <div className="form-container">
        <div>
          <label>Client ID:</label>
          <input type="text" value={logAppointment.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required />
        </div>
        <div>
          <label>Attendant ID:</label>
          <input type="text" value={logAppointment.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} />
        </div>
        <div>
          <label>Status:</label>
          <input type="text" value={logAppointment.status} onChange={(e) => handleChange('status', e.target.value)} />
        </div>
        <div>
          <label>Price:</label>
          <input type="text" value={logAppointment.price} onChange={(e) => handleChange('price', e.target.value)} />
        </div>
        <div>
          <label>Schedule ID:</label>
          <input type="text" value={logAppointment.scheduleId} onChange={(e) => handleChange('scheduleId', e.target.value)} />
        </div>
        <div>
          <label>Place:</label>
          <input type="text" value={logAppointment.place} onChange={(e) => handleChange('place', e.target.value)} />
        </div>
        <div>
          <label>Observation:</label>
          <textarea value={logAppointment.observation} onChange={(e) => handleChange('observation', e.target.value)} />
        </div>
        <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>
      </div>
    </div>
  );
}

export default LogFormulary;

/*

<div>
  <label>Date Time Start:</label>
  <input type="datetime-local" value={logAppointment.interval.start} onChange={(e) => handleChange('interval.start', new Date(e.target.value))} required />
</div>
<div>
  <label>Date Time End:</label>
  <input type="datetime-local" value={logAppointment.interval.finish} onChange={(e) => handleChange('interval.finish', new Date(e.target.value))} required />
</div>

*/