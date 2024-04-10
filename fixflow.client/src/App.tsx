import { Route, Routes } from "react-router-dom";
import "./App.css";
import Header from "./Components/Header/Header";
import MainPage from "./Pages/MainPage";
import NotFoundPage from "./Pages/NotFoundPage";
import UserPage from "./Pages/Shared/UserPage";
import UserSettingsPage from "./Pages/Shared/UserSettingsPage";
import RegisterUser from "./Pages/Business/RegisterUserPage";
import CreateSchedulePage from "./Pages/Shared/CreateSchedulePage";
import CreateLogPage from "./Pages/Shared/CreateLogPage";
import AllSchedulesPage from "./Pages/Shared/AllSchedulesPage";
import AllLogsPage from "./Pages/Shared/AllLogsPage";
import EmployeesSchedules from "./Pages/Secretary/EmployeesSchedules";
import Login from "./Pages/Login";
import "./App.css";

function App() {
  return (
    <div>
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/Client">
          <Route index element={<UserPage />} />
          <Route path="UserSettings" element={<UserSettingsPage />} />

          <Route path="AppointmentSchedule" element={<CreateSchedulePage />} />
          <Route path="AppointmentLog" element={<CreateLogPage />} />
        </Route>
        <Route path="/Employee">
          <Route index element={<UserPage />} />
          <Route path="UserSettings" element={<UserSettingsPage />} />

          <Route path="RegisterClient" element={<RegisterUser />} />
          <Route path="AllSchedules" element={<AllSchedulesPage />} />
          <Route path="AllLogs" element={<AllLogsPage />} />
          <Route path="AppointmentSchedule" element={<CreateSchedulePage />} />
          <Route path="AppointmentLog" element={<CreateLogPage />} />
        </Route>
        <Route path="/Secretary">
          <Route index element={<UserPage />} />
          <Route path="UserSettings" element={<UserSettingsPage />} />

          <Route path="RegisterClient" element={<RegisterUser />} />
          <Route path="AllSchedules" element={<AllSchedulesPage />} />
          <Route path="AllLogs" element={<AllLogsPage />} />

          <Route path="EmployeesSchedules" element={<EmployeesSchedules />} />
          <Route path="RegisterEmployee" element={<RegisterUser />} />
          <Route path="RegisterSecretary" element={<RegisterUser />} />
        </Route>
        <Route path="/Login" element={<Login />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </div>
  );
  return <div></div>;
}

export default App;
