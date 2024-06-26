import { Route, Routes } from "react-router-dom";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";
import NotFoundPage from "./NotFoundPage";
import ClientPage from "./Pages/ClientPage/ClientPage";
import BusinessPage from "./Pages/BusinessPage/BusinessPage";
import SchedulesPage from "./Pages/BusinessPage/SchedulesPage/SchedulesPage";
import LogsPage from "./Pages/BusinessPage/LogsPage/LogsPage";
import ContactsPage from "./Pages/BusinessPage/ContactsPage/ContactsPage";
import InfographicsPage from "./Pages/BusinessPage/InfograficosPage/InfograficosPage";
import SettingsPage from "./Pages/SettingsPage/SettingsPage";
import PreferencesPage from "./Pages/BusinessPage/PreferencesPage/PreferencesPage";
import SchedulePage from "./Pages/Apts/SchedulePage/SchedulePage";
import ResetLinkPage from "./Pages/PasswordReset/ResetLinkPage/ResetLinkPage";
import ResetRequestPage from "./Pages/PasswordReset/ResetRequestPage/ResetRequestPage";

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
          <Route path="infographics" element={<InfographicsPage />} />
          <Route path="settings" element={<SettingsPage />} />
          <Route path="preferences" element={<PreferencesPage />} />
        </Route>

        <Route path="/in">
          <Route index element={<ClientPage />} />
          <Route path="settings" element={<SettingsPage />} />
          <Route path="preferences" element={<PreferencesPage />} />
        </Route>

        <Route path="/agendamento">
          <Route index element={<NotFoundPage />} />
          <Route path="*" element={<SchedulePage />} />
        </Route>

        <Route path="/reset">
          <Route index element={<NotFoundPage />} />
          <Route path="request" element={<ResetRequestPage />} />
          <Route path="*" element={<ResetLinkPage />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
