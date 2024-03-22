import { useState } from 'react';
import ScheduleAppointment from '../Data/ScheduleAppointment';

function ScheduleFormulary() {

  const [schedule, setSchedule] = useState<ScheduleAppointment>(new ScheduleAppointment());
  const [buttonColor, setButtonColor] = useState('yellow');

  const handleSend = () => {
    if (schedule.dateTime && schedule.clientId) {
      setButtonColor('green');
      // Lógica para enviar dados
    }
  };

  const handleChange = (field: string, value: string | Date | number) => {
    setSchedule(prevSchedule => ({
      ...prevSchedule,
      [field]: value
    }));
  };

  return (
    <div className="form-container">
      <div>
        <label>Client ID:</label>
        <input type="text" value={schedule.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required />
      </div>
      <div>
        <label>Date Time:</label>
        <input type="datetime-local" value={schedule.dateTime.toISOString().substring(0, 16)} onChange={(e) => handleChange('dateTime', new Date(e.target.value))} required />
      </div>
      <div>
        <label>Attendant ID:</label>
        <input type="text" value={schedule.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} />
      </div>
      <div>
        <label>Secretary ID:</label>
        <input type="text" value={schedule.secretaryId} onChange={(e) => handleChange('secretaryId', e.target.value)} />
      </div>
      <div>
        <label>Expected Price:</label>
        <input type="number" value={schedule.expectedPrice} onChange={(e) => handleChange('expectedPrice', parseFloat(e.target.value))} />
      </div>
      <div>
        <label>Observation:</label>
        <textarea value={schedule.observation} onChange={(e) => handleChange('observation', e.target.value)} />
      </div>
      <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>
    </div>
  );
}

export default ScheduleFormulary;
