import {  useParams } from 'react-router-dom';
import LogAppointment from '../../Data/LogAppointment';

//useSearchParams
const AppointmentLogPage: React.FC<LogAppointment> = ( ) => {

  const {id} = useParams();

  return (
    <div>
      <h1>Hello, this is {id}</h1>
      <p>You can customize this component as needed.</p>
    </div>
  );
};

export default AppointmentLogPage;