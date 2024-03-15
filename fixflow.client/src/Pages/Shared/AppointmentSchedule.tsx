import React from 'react';
import { useParams } from 'react-router-dom';

const AppointmentSchedule = () => {

  const {id} = useParams();

  return (
    <div>
      <h1>Hello, this is {id}</h1>
      <p>You can customize this component as needed.</p>
    </div>
  );
};

export default AppointmentSchedule;