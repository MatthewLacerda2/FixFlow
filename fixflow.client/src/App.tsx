import { Route, Routes } from 'react-router-dom';
import './App.css'
import Header from './Components/Header/Header';
import MainPage from './Pages/MainPage';
import NotFoundPage from './Pages/NotFoundPage';
import UserPage from './Pages/Shared/UserPage';
import RegisterEmployeePage from './Pages/Secretary/RegisterEmployee';
import RegisterSecretaryPage from './Pages/Secretary/RegisterSecretary';
import AppointmentLogPage from './Pages/Shared/AppointmentLogPage';
import CreateSchedulePage from './Pages/Shared/CreateSchedulePage';
import AppointmentSchedulePage from './Pages/Shared/AppointmentSchedulePage';

//falta register client
//falta log appointment
//falta ver log appointment
function App() {
    return (
        <div>
            <Header/>
            <Routes>
                <Route path="/" element = {<MainPage/>}/>
                <Route path="/Client">
                    <Route index element={<UserPage/>}/>
                </Route>
                <Route path="/Employee">
                    <Route index element={<UserPage/>}/>
                </Route>
                <Route path="/Secretary">
                    <Route index element={<UserPage/>}/>
                    <Route path="RegisterEmployee" element={<RegisterEmployeePage/>}/>
                    <Route path="RegisterSecretary" element={<RegisterSecretaryPage/>}/>
                </Route>
                <Route path="/AppointmentLog/:id" element={<AppointmentLogPage/>}/>
                <Route path="/AppointmentSchedule" element={<CreateSchedulePage/>}/>
                <Route path="/AppointmentSchedule/:id" element={<AppointmentSchedulePage/>}/>                
                <Route path="*" element={<NotFoundPage/>}/>
            </Routes>
        </div>
    )
}

export default App;