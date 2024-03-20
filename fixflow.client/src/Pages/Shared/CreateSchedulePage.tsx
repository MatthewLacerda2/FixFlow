import { useState } from 'react';
import ScheduleAppointment from '../../Data/ScheduleAppointment';
import Card from '../../Components/Card/Card';
import './CreateSchedulePage.css';

const CreateSchedulePage = () => {
  
  const [schedule, setSchedule] = useState<ScheduleAppointment>(new ScheduleAppointment);
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
    <Card title = "Create schedule">
      
      <div className='input-container'>
        <label className='babel'>Client:</label>
        <input type="text" value={schedule.clientId} onChange={(e) => handleChange('clientId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Attendant ID:</label>
        <input type="text" value={schedule.attendantId} onChange={(e) => handleChange('attendantId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Secretary ID:</label>
        <input type="text" value={schedule.secretaryId} onChange={(e) => handleChange('secretaryId', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Price:</label>
        <input type="text" value={schedule.expectedPrice} onChange={(e) => handleChange('expectedPrice', e.target.value)} required className='input-area'/>
      </div>
      <div className='input-container'>
        <label className='babel'>Observation:</label>
        <input type="text" value={schedule.observation} onChange={(e) => handleChange('observation', e.target.value)} required className='input-area'/>
      </div>

      <br></br>
      <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>

    </Card>
  );
};

export default CreateSchedulePage;