import { Route, Routes } from "react-router-dom";
import "./App.css";
import Header from "./Components/Header/Header";
import MainPage from "./MainPage";

function App() {
  return (
    <div>
      <Header />
      <Routes>
        <Route path="/" element={<MainPage></MainPage>} />
      </Routes>
    </div>
  );
}

export default App;
