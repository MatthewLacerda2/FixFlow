import { Route, Routes } from "react-router-dom";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";
import NotFoundPage from "./NotFoundPage";
import SettingsPage from "./Pages/SettingsPage/SettingsPage";
import BusinessPage from "./Pages/BusinessPage/BusinessPage";
import ClientPage from "./Pages/ClientPage/ClientPage";
import BusinessSidebar from "./Components/Sidebar/BusinessSidebar/BusinessSidebar";

function App() {
  return (
    <div>
      <Header />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="*" element={<NotFoundPage />} />
        <Route path="/e">
          <Route index element={<BusinessPage />} />
          <Route path="Settings" element={<SettingsPage />} />
          <Route path="*" element={<BusinessSidebar />} />
        </Route>
        <Route path="/in">
          <Route index element={<ClientPage />} />
          <Route path="Settings" element={<SettingsPage />} />
          <Route path="*" element={<BusinessSidebar />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
