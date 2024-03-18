import { useState } from 'react';

function ScheduleFormulary() {
  
  const [clientId, setClientId] = useState('');
  const [dateTime, setDateTime] = useState('');
  const [attendantId, setAttendantId] = useState('');
  const [secretaryId, setSecretaryId] = useState('');
  const [expectedPrice, setExpectedPrice] = useState('');
  const [observation, setObservation] = useState('');
  const [buttonColor, setButtonColor] = useState('yellow');

  const handleSend = () => {
    if (dateTime && clientId) {
      setButtonColor('green');
      // Logic to send data
    }
  };  

  return (
    <div>
      <header className="header">
        <div className="header-left">
          FixFlow
        </div>
        <div className="header-right">
          <button>Login</button>
        </div>
      </header>
      <div className="form-container">
        <div>
            <label>Client ID:</label>
            <input type="text" value={clientId} onChange={(e) => setClientId(e.target.value)} required />
        </div>
        <div>
            <label>Date Time:</label>
            <input type="datetime-local" value={dateTime} onChange={(e) => setDateTime(e.target.value)} required />
        </div>
        <div>
            <label>Attendant ID:</label>
            <input type="text" value={attendantId} onChange={(e) => setAttendantId(e.target.value)} />
        </div>
            <label>Secretary ID:</label>
            <input type="text" value={secretaryId} onChange={(e) => setSecretaryId(e.target.value)} />
        <div>
            <label>Expected Price:</label>
            <input type="number" value={expectedPrice} onChange={(e) => setExpectedPrice(e.target.value)} />
        </div>
        <div>
            <label>Observation:</label>
            <textarea value={observation} onChange={(e) => setObservation(e.target.value)} />
        </div>
        <button style={{ backgroundColor: buttonColor }} onClick={handleSend}>Send</button>
      </div>
    </div>
  );
}

export default ScheduleFormulary;