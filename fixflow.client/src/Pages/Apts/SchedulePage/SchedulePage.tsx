import React from "react";
import { useParams } from "react-router-dom";
import { AptSchedule } from "../../../FlowApi";
import Card from "../../../Components/Card/Card";

const SchedulePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();

  const schedule: AptSchedule = {
    id: id as string,
    clientId: "c1",
    businessId: "b1",
    dateTime: "2024-05-16 T10:00:00 Z",
    price: undefined,
    observation: "Initial consultation",
  };

  return (
    <div style={{ fontSize: "20px" }}>
      <Card title="Agendamento">
        <p>
          <b>Client:</b> {schedule.client?.fullName}
        </p>
        <p>
          <b>Empresa:</b> {schedule.business?.name}
        </p>
        <p>
          <b>Hora e Data:</b> {schedule.dateTime}
        </p>
        <p>
          <b>Preço:</b> {schedule.price || "N/A"}
        </p>
        <p>
          <b>Observação:</b> {schedule.observation || "N/A"}
        </p>
      </Card>
    </div>
  );
};

export default SchedulePage;
