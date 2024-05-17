import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Card from "../../../Components/Card/Card";

const ResetLinkPage: React.FC = () => {
  const { token } = useParams<{ token: string }>();
  const [error, setError] = useState<string | null>(null);
  const [PR, setPR] = useState<string | null>(null);

  const sendPRrequest = (pr: string) => {
    console.log("Request sent!");
    console.log("Aí checa se deu sucesso, se sim manda o cliente fazer login");
    console.log("Se nao, diz que deu erro e fala qual");
  };

  useEffect(() => {
    const fetchSchedule = async () => {
      try {
        if (token) {
          //Route goes here
          console.error("route implementation goes here");
          setPR(result);
        }
      } catch (error) {
        setError("There was an error fetching the request");
      }
    };

    fetchSchedule();
  }, [token]);

  if (error) {
    <h1 style={{ fontSize: "32px" }}>{error}</h1>;
  }

  if (!PR.Email) {
    return <h1 style={{ fontSize: "32px" }}>This link is invalid</h1>;
  }

  return (
    <div style={{ fontSize: "20px" }}>
      <Card title="Agendamento">
        <p>
          <b>Email:</b> {PR.Email}
        </p>
        <p>
          <b>New Password:</b> {PR.password}
        </p>
        <p>
          <b>Confirm password:</b> {PR.confirmPassword}
        </p>
      </Card>
    </div>
  );
};

export default ResetLinkPage;
