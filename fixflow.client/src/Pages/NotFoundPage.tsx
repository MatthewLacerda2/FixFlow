import React from 'react';
import { useParams } from 'react-router-dom';

const NotFoundPage = () => {

  const {id} = useParams();

  return (
    <div>
      <h1>Not Found {id}</h1>
    </div>
  );
};

export default NotFoundPage;