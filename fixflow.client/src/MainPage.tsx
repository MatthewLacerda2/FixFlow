import React from "react";
import Card from "./Components/Card/Card";
import "./Components/Forms/Form.css";
import { LoginForm } from "./Components/PagesComponents/MainPage/LoginForm/LoginForm";

const MainPage: React.FC = () => {
  return (
    <div className="main-container">
      <div className="header">
        <div className="brand">
          <h1>Flow</h1>
          <p>Agende atendimentos, fácil e rápido</p>
        </div>
      </div>
      <Card title="Login">
        <LoginForm />
      </Card>
    </div>
  );
};

export default MainPage;
