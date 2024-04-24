import { Route, Routes } from "react-router-dom";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";
import NotFoundPage from "./NotFoundPage";
import SettingsPage from "./Pages/SettingsPage/SettingsPage";
import EmployeePage from "./Pages/EmployeePage/EmployeePage";
import ClientPage from "./Pages/ClientPage/ClientPage";
import EmployeeSidebar from "./Components/Sidebar/EmployeeSidebar/EmployeeSidebar";

function App() {
  return (
    <div>
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="*" element={<NotFoundPage />} />
        <Route path="/sh">
          <Route index element={<EmployeePage />} />
          <Route path="Settings" element={<SettingsPage />} />
          <Route path="*" element={<EmployeeSidebar />} />
        </Route>
        <Route path="/in">
          <Route index element={<ClientPage />} />
          <Route path="Settings" element={<SettingsPage />} />
          <Route path="*" element={<EmployeeSidebar />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
