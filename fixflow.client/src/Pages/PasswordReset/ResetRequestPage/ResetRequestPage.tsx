import React from "react";
import Card from "../../../Components/Card/Card";

const ResetRequestPage: React.FC = () => {
  const sendEmail = () => {
    console.log("Email sent!");
  };

  return (
    <Card title="Reset sua senha">
      <h2>Digite o Email da sua conta</h2>
      <p>
        Você receberá um email com o link por onde poderá resetar sua conta. O
        link é válido por 15 minutos!
      </p>
      <form onSubmit={sendEmail}>
        <input type="email" placeholder="Email" />
        <button type="submit">Send</button>
      </form>
    </Card>
  );
};

export default ResetRequestPage;

/*
sent &&
    <Card title="Email enviado!">
        <p>
            Cheque sua correspondência. Use o link no email para resetar sua senha
        </p>
    </Card>*/
