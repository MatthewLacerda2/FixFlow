import './App.css';
import { Route, Routes } from 'react-router-dom';
import MainPage from './Pages/MainPage';
import ClientPage from './Pages/Client/ClientPage';
import EmployeePage from './Pages/Employee/EmployeePage';
import SecretaryPage from './Pages/Secretary/SecretaryPage';
import AppointmentSchedule from './Pages/Shared/AppointmentSchedule';
import NotFoundPage from './Pages/NotFoundPage';

function App() {
    return (
        <Routes>
            <Route path = "/" element = {<MainPage/>}/>
            <Route path = "/Client" element = {<ClientPage/>}/>
            <Route path = "/Employee" element = {<EmployeePage/>}/>
            <Route path = "/Secretary" element = {<SecretaryPage/>}/>
            <Route path = "/AppointmentSchedule/:id" element = {<AppointmentSchedule/>}/>
            <Route path = "*" element = {<NotFoundPage/>}/>
        </Routes>
    );
}

export default App;