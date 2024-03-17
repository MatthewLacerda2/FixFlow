import React from 'react';
import { Route, useParams } from 'react-router-dom';

//useSearchParams
const AppointmentSchedulePage = () => {

  const {id} = useParams();

  return (
    <div>
      <h1>Hello, this is {id}</h1>
      <p>You can customize this component as needed.</p>
    </div>
  );
};

export default AppointmentSchedulePage;