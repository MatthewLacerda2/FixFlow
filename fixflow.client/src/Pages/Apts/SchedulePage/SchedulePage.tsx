import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { AptSchedule, ScheduleService } from "../../../FlowApi";
import Card from "../../../Components/Card/Card";

const SchedulePage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [schedule, setSchedule] = useState<AptSchedule | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchSchedule = async () => {
      try {
        if (id) {
          const result = await ScheduleService.getApiV1Schedules(id);
          setSchedule(result);
        }
      } catch (error) {
        setError("There was an error fetching the schedule.");
      }
    };

    fetchSchedule();
  }, [id]);

  if (error) {
    <h1 style={{ fontSize: "32px" }}>{error}</h1>;
  }

  if (!schedule) {
    return <h1 style={{ fontSize: "32px" }}>Schedule does not exist</h1>;
  }

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
          <b>Preço:</b> {schedule.price !== null ? `$${schedule.price}` : "N/A"}
        </p>
        <p>
          <b>Observação:</b> {schedule.observation || "N/A"}
        </p>
      </Card>
    </div>
  );
};

export default SchedulePage;
