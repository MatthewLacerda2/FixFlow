import { useState } from 'react';
import LogAppointment from '../../Data/LogAppointment';
import Card from '../../Components/Card/Card';
import './CreateSchedulePage.css';

const CreateLogPage = () => {
  
  const [schedule, setSchedule] = useState<LogAppointment>(new LogAppointment);
  const [buttonColor, setButtonColor] = useState('green');

  const handleSend = () => {
    if (schedule.clientId) {
      setButtonColor('blue');
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
    <Card title = "Create Log">
      
      <div className='input-container'>
        <label className='babel'>Client:</label>
        <input type="text" value={schedule.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Attendant ID:</label>
        <input type="text" value={schedule.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Schedule ID:</label>
        <input type="text" value={schedule.scheduleId} onChange={(e) => handleChange('scheduleId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Price:</label>
        <input type="text" value={schedule.place} onChange={(e) => handleChange('place', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Observation:</label>
        <input type="text" value={schedule.price} onChange={(e) => handleChange('price', e.target.value)} required className='input-area'/>
      </div>

      <br></br>
      <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>

    </Card>
  );
};

export default CreateLogPage;