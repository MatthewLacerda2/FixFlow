import { Route, Routes } from 'react-router-dom';
import './App.css'
import MainPage from './Pages/MainPage';
import ClientPage from './Pages/Client/ClientPage';
import EmployeePage from './Pages/Employee/EmployeePage';
import SecretaryPage from './Pages/Secretary/SecretaryPage';
import RegisterEmployeePage from './Pages/Secretary/RegisterEmployee';
import RegisterSecretaryPage from './Pages/Secretary/RegisterSecretary';
import AppointmentSchedulePage from './Pages/Shared/AppointmentSchedulePage';
import NotFoundPage from './Pages/NotFoundPage';

function App() {
    return (
        <Routes>
            <Route path="/" element = {<MainPage/>}/>
            <Route path="/Client">
                <Route index element={<ClientPage/>}/>
                <Route path=":id" element={<ClientPage/>}/>
            </Route>
            <Route path="/Employee">
                <Route index element={<EmployeePage/>}/>
            </Route>
            <Route path="/Secretary">
                <Route index element={<SecretaryPage/>}/>
                <Route path="RegisterEmployee" element={<RegisterEmployeePage/>}/>
                <Route path="RegisterSecretary" element={<RegisterSecretaryPage/>}/>
            </Route>
            <Route path="/AppointmentSchedule/:id" element={<AppointmentSchedulePage/>}/>
            <Route path="*" element={<NotFoundPage/>}/>
        </Routes>
    )
}

export default App;