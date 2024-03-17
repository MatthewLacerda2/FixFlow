import React, { useEffect } from 'react';
import { Navigate, redirect, useNavigate, useParams } from 'react-router-dom';

const NotFoundPage = () => {

  const {id} = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    // Check if the id matches any of the specified patterns
    if (id && ['cliente', 'clients', 'clientes'].includes(id.toLowerCase())) {
      redirect("/Client");
    }
  }, [id]);

  return (
    <div>
      <h1>Not {id}</h1>
      <p>You can customize this component as needed.</p>
    </div>
  );
};

export default NotFoundPage;