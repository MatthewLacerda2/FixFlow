import React, { useState } from "react";
import Card from "../../../Components/Card/Card";
import FlowButton from "../../../Components/FlowButton/FlowButton";
import "../../../Styles/Form.css";
import { AccountsService } from "../../../FlowApi";

const ResetRequestPage: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const sendEmail = async () => {
    if (!email.includes("@")) {
      setError("Digite um email valido");
    }

    setError(null);
    setSuccess(null);

    AccountsService.postApiV1AccountsResetEmail(email)
      .then((response) => {
        setSuccess(response);
      })
      .catch((error) => {
        setError(error);
      });
  };

  if (success) {
    <Card title="Email enviado">{success}</Card>;
  }

  return (
    <Card title="Reset sua senha">
      <h2 style={{ marginTop: "-2px", marginBottom: "5px" }}>
        Digite o Email da sua conta
      </h2>
      <p>
        Você receberá um email com o link para resetar sua senha. O link é
        válido por 15 minutos!
      </p>

      <input
        type="email"
        placeholder="Email"
        className="input-field"
        onChange={handleEmailChange}
      />
      <FlowButton text="Send" onClick={sendEmail} />

      {error && <div style={{ color: "#cc0000" }}>{error}</div>}
    </Card>
  );
};

export default ResetRequestPage;
