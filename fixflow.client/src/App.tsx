import { Route, Routes } from "react-router-dom";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";
import NotFoundPage from "./NotFoundPage";
import SettingsPage from "./Pages/SettingsPage/SettingsPage";
import BusinessPage from "./Pages/BusinessPage/BusinessPage";
import ClientPage from "./Pages/ClientPage/ClientPage";
import SchedulesPage from "./Pages/BusinessPage/SchedulesPage/SchedulesPage";
import LogsPage from "./Pages/BusinessPage/LogsPage/LogsPage";
import ContactsPage from "./Pages/BusinessPage/ContactsPage/ContactsPage";

function App() {
  return (
    <div>
      <Header />

      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="*" element={<NotFoundPage />} />

        <Route path="/e">
          <Route index element={<BusinessPage />} />

          <Route path="agendamentos" element={<SchedulesPage />} />
          <Route path="atendimentos" element={<LogsPage />} />
          <Route path="contatos" element={<ContactsPage />} />
          <Route path="settings" element={<SettingsPage />} />
          <Route path="preferences" element={<SettingsPage />} />
        </Route>

        <Route path="/in">
          <Route index element={<ClientPage />} />
          <Route path="settings" element={<SettingsPage />} />
          <Route path="preferences" element={<SettingsPage />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
