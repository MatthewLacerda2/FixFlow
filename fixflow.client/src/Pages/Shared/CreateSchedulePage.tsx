import { useState } from 'react';
import ScheduleAppointment from '../../Data/ScheduleAppointment';
import Card from '../../Components/Card/Card';

const CreateSchedulePage = () => {
  
  const [schedule, setSchedule] = useState<ScheduleAppointment>(new ScheduleAppointment);
  const [buttonColor, setButtonColor] = useState('yellow');

  const handleSend = () => {
    if (schedule.clientId) {
      setButtonColor('green');
      // Lógica para enviar dados
    }
  };

  const handleChange = (field: string, value: string | number | Date) => {
    setSchedule(prevSchedule => ({
      ...prevSchedule,
      [field]: value
    }));
  };

  return (
    <Card title = "something">

      <div>
        <label>Attendant ID:</label>
        <input type="text" value={schedule.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} required />
      </div>
      <div>
        <label>Client ID:</label>
        <input type="text" value={schedule.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required />
      </div>
      <div>
        <label>Secretary ID:</label>
        <input type="text" value={schedule.secretaryId} onChange={(e) => handleChange('secretaryId', e.target.value)} required />
      </div>
      <div>
        <label>Price:</label>
        <input type="text" value={schedule.expectedPrice} onChange={(e) => handleChange('expectedPrice', e.target.value)} required />
      </div>
      <div>
        <label>Observation:</label>
        <input type="text" value={schedule.observation} onChange={(e) => handleChange('observation', e.target.value)} required />
      </div>

      <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>

    </Card>
  );
};

export default CreateSchedulePage;