import {  useParams } from 'react-router-dom';

//useSearchParams
const AppointmentLogPage = () => {

  const {id} = useParams();

  return (
    <div>
      <h1>Hello, this is {id}</h1>
      <p>You can customize this component as needed.</p>
    </div>
  );
};

export default AppointmentLogPage;