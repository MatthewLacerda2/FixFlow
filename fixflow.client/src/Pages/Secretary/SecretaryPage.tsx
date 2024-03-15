import React from 'react';
import { Link } from 'react-router-dom';

const SecretaryPage = () => {
  return (
    <div>
      <h1>Hello, this is an example component!</h1>
      <p>You can customize this component as needed.</p>
      <Link to="/RegisterSecretary"/>
    </div>
  );
};

export default SecretaryPage;