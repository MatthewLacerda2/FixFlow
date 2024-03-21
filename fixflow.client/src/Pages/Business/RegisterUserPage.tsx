import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import './RegisterUserPage.css';
import Card from '../../Components/Card/Card';
import '../Shared/CreateSchedulePage.css';

const RegisterUserPage = () => {

  const { userType } = useParams();
  
  const [userForm, setUserForm] = useState({
    FullName: '',
    CPF: '',
    Email: '',
    PhoneNumber: '',
    AdditionalNote: ''
  });

  const handleSave = () => {
    if (userForm.FullName && userForm.CPF && userForm.Email && userForm.PhoneNumber) {
      console.log('Saved stuff:', userForm);
    } else {
      console.log('Fill all fields before saving.');
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setUserForm(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  return (
    <Card title = "Register {userType}">
      
        <div className="input-container">
          <label className='babel'>Full Name:</label>
          <input type="text" name="FullName" value={userForm.FullName} onChange={handleChange} required className='input-area'/>
        </div>
        <div className="input-container">
          <label className='babel'>CPF:</label>
          <input type="text" name="CPF" value={userForm.CPF} onChange={handleChange} className='input-area'/>
        </div>
        <div className="input-container">
          <label className='babel'>Email:</label>
          <input type="email" name="Email" value={userForm.Email} onChange={handleChange} className='input-area'/>
        </div>
        <div className="input-container">
          <label className='babel'>Phone Number:</label>
          <input type="tel" name="PhoneNumber" value={userForm.PhoneNumber} onChange={handleChange} required className='input-area'/>
        </div>
        {userType === 'client' && (
          <div className="input-container">
            <label className='babel'>Additional Note:</label>
            <textarea name="AdditionalNote" value={userForm.AdditionalNote} onChange={handleChange} />
          </div>
        )}

        <br></br>

        <button onClick={handleSave}>Save</button>
      
    </Card>
  );
};

export default RegisterUserPage;