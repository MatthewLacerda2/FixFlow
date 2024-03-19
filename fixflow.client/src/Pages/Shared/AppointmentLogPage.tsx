import LogAppointment from '../../Data/LogAppointment';
import { useState } from 'react';

const AppointmentLogPage: React.FC<LogAppointment> = ( logAppoint : LogAppointment ) => {
  
  const [schedule, setSchedule] = useState<LogAppointment>(logAppoint);
  const [buttonColor, setButtonColor] = useState('yellow');

  const handleSend = () => {
    if (schedule.clientId) {
      setButtonColor('green');
      // Lógica para enviar dados
    }
  };

  const handleChange = (field: string, value: string | Date) => {
    setSchedule(prevSchedule => ({
      ...prevSchedule,
      [field]: value
    }));
  };

  return (
    <div>

      <div>
        <label>Attendant ID:</label>
        <input type="text" value={schedule.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} required />
      </div>
      <div>
        <label>Client ID:</label>
        <input type="text" value={schedule.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required />
      </div>
      <div>
        <label>Status:</label>
        <input type="text" value={schedule.status} onChange={(e) => handleChange('status', e.target.value)} required />
      </div>
      <div>
        <label>Price:</label>
        <input type="text" value={schedule.price} onChange={(e) => handleChange('price', e.target.value)} required />
      </div>
      <div>
        <label>Schedule Id:</label>
        <input type="text" value={schedule.scheduleId} onChange={(e) => handleChange('scheduleId', e.target.value)} required />
      </div>
      <div>
        <label>Place:</label>
        <input type="text" value={schedule.place} onChange={(e) => handleChange('place', e.target.value)} required />
      </div>

      <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>

    </div>
  );
};

export default AppointmentLogPage;