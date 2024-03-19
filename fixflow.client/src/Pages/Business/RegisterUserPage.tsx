import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import './RegisterUserPage.css';

const RegisterUserPage = () => {

  const { userType } = useParams();
  
  const [userForm, setUserForm] = useState({
    FullName: '',
    CPF: '',
    Email: '',
    PhoneNumber: '',
    AdditionalNote: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setUserForm(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSave = () => {
    if (userForm.FullName && userForm.CPF && userForm.Email && userForm.PhoneNumber) {
      console.log('Saved stuff:', userForm);
    } else {
      console.log('Fill all fields before saving.');
    }
  };

  return (
    <div className="register-user-page">
      <h2>Register {userType}</h2>
      <div className="form-container">
        <div className="form-group">
          <label>Full Name:</label>
          <input type="text" name="FullName" value={userForm.FullName} onChange={handleChange} />
        </div>
        <div className="form-group">
          <label>CPF:</label>
          <input type="text" name="CPF" value={userForm.CPF} onChange={handleChange} />
        </div>
        <div className="form-group">
          <label>Email:</label>
          <input type="email" name="Email" value={userForm.Email} onChange={handleChange} />
        </div>
        <div className="form-group">
          <label>Phone Number:</label>
          <input type="tel" name="PhoneNumber" value={userForm.PhoneNumber} onChange={handleChange} />
        </div>
        {userType === 'client' && (
          <div className="form-group">
            <label>Additional Note:</label>
            <textarea name="AdditionalNote" value={userForm.AdditionalNote} onChange={handleChange} />
          </div>
        )}
        <button onClick={handleSave}>Save</button>
      </div>
    </div>
  );
};

export default RegisterUserPage;