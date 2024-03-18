import { Route, Routes } from 'react-router-dom';
import './App.css'
import Header from './Components/Header/Header';
import MainPage from './Pages/MainPage';
import RegisterEmployeePage from './Pages/Secretary/RegisterEmployee';
import RegisterSecretaryPage from './Pages/Secretary/RegisterSecretary';
import AppointmentSchedulePage from './Pages/Shared/AppointmentSchedulePage';
import NotFoundPage from './Pages/NotFoundPage';
import UserPage from './Pages/Shared/UserPage';
import CreateSchedulePage from './Pages/Shared/CreateSchedulePage';

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
                <Route path="/AppointmentSchedule" element={<CreateSchedulePage/>}/>
                <Route path="/AppointmentSchedule/:id" element={<AppointmentSchedulePage/>}/>
                <Route path="*" element={<NotFoundPage/>}/>
            </Routes>
        </div>
    )
}

export default App;