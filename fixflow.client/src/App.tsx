import { Route, Routes } from "react-router-dom";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";
import NotFoundPage from "./NotFoundPage";
import SettingsPage from "./Pages/SettingsPage/SettingsPage";
import BusinessPage from "./Pages/BusinessPage/BusinessPage";
import ClientPage from "./Pages/ClientPage/ClientPage";

function App() {
  return (
    <div>
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />

        <Route path="*" element={<NotFoundPage />} />

        <Route path="/e">
          <Route index element={<BusinessPage />} />

          <Route path="agendamentos" element={<SettingsPage />} />
          <Route path="atendimentos" element={<SettingsPage />} />
          <Route path="contatos" element={<SettingsPage />} />
          <Route path="settings" element={<SettingsPage />} />
        </Route>

        <Route path="/in">
          <Route index element={<ClientPage />} />

          <Route path="agendamentos" element={<SettingsPage />} />
          <Route path="atendimentos" element={<SettingsPage />} />
          <Route path="settings" element={<SettingsPage />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
