import './App.css';
import { Route, Routes } from 'react-router-dom';
import MainPage from './Pages/MainPage';
import ClientPage from './Pages/Client/ClientPage';
import EmployeePage from './Pages/Employee/EmployeePage';
import SecretaryPage from './Pages/Secretary/SecretaryPage';
import AppointmentSchedule from './Pages/Shared/AppointmentSchedule';
import NotFoundPage from './Pages/NotFoundPage';
import RegisterSecretary from './Pages/Secretary/RegisterSecretary';

function App() {
    return (
        <Routes>
            <Route path = "/" element = {<MainPage/>}/>
            <Route path = "/Client">
                <Route index element={<ClientPage/>}/>
                <Route path=":id" element={<ClientPage/>}/>
            </Route>
            <Route path = "/Employee">
                <Route index element={<EmployeePage/>}/>
            </Route>
            <Route path = "/Secretary">
                <Route index element={<SecretaryPage/>}/>
                <Route path="/RegisterSecretary" element={<RegisterSecretary/>}/>
            </Route>
            <Route path = "/AppointmentSchedule/:id" element = {<AppointmentSchedule/>}/>
            <Route path = "*" element = {<NotFoundPage/>}/>
        </Routes>
    );
}

export default App;