import { Route, Routes } from 'react-router-dom';
import './App.css'
import Header from './Components/Header/Header';
import MainPage from './Pages/MainPage';
import NotFoundPage from './Pages/NotFoundPage';
import UserPage from './Pages/Shared/UserPage';
import CreateLogPage from './Pages/Shared/CreateLogPage';
import CreateSchedulePage from './Pages/Shared/CreateSchedulePage';
import AppointmentSchedulePage from './Pages/Shared/AppointmentSchedulePage';
import UserSettingsPage from './Pages/Shared/UserSettingsPage';
import RegisterUser from './Pages/Business/RegisterUserPage';
import WeekOverviewPage from './Pages/Business/WeekOverviewPage';
import AppointmentLogPage from './Pages/Shared/AppointmentLogPage';

function App() {
    return (
        <div>
            <Header/>
            <Routes>
                <Route path="/" element = {<MainPage/>}/>
                <Route path="/Client">
                    <Route index element={<UserPage/>}/>
                    <Route path="UserSettings" element={<UserSettingsPage/>}/>
                </Route>
                <Route path="/Employee">
                    <Route index element={<UserPage/>}/>
                    <Route path="UserSettings" element={<UserSettingsPage/>}/>
                    <Route path="RegisterClient" element={<RegisterUser/>}/>
                    <Route path="WeekOverview" element={<WeekOverviewPage/>}/>
                </Route>
                <Route path="/Secretary">
                    <Route index element={<UserPage/>}/>
                    <Route path="UserSettings" element={<UserSettingsPage/>}/>
                    <Route path="RegisterClient" element={<RegisterUser/>}/>
                    <Route path="RegisterEmployee" element={<RegisterUser/>}/>
                    <Route path="RegisterSecretary" element={<RegisterUser/>}/>
                    <Route path="WeekOverview" element={<WeekOverviewPage/>}/>
                </Route>
                <Route path="/AppointmentLog/" element={<AppointmentLogPage/>}/>
                <Route path="/AppointmentLog/:id" element={<CreateLogPage/>}/>
                <Route path="/AppointmentSchedule" element={<CreateSchedulePage/>}/>
                <Route path="/AppointmentSchedule/:id" element={<AppointmentSchedulePage/>}/>                
                <Route path="*" element={<NotFoundPage/>}/>
            </Routes>
        </div>
    )
}

export default App;