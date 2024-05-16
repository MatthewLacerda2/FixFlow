import React from "react";
import { AptSchedule } from "../../../../../../FlowApi";
import FlowButton from "../../../../../FlowButton/FlowButton";

type ScheduleTableProps = {
  schedules: AptSchedule[];
};

const SchedulesTable: React.FC<ScheduleTableProps> = ({ schedules }) => {
  return (
    <table>
      <thead>
        <tr>
          <th>
            <FlowButton
              text="Cliente"
              onClick={() => {
                console.log("oi");
              }}
            ></FlowButton>
          </th>
          <th>
            <FlowButton
              text="Hora e Data"
              onClick={() => {
                console.log("oi");
              }}
            ></FlowButton>
          </th>
          <th>
            <FlowButton
              text="Preço"
              onClick={() => {
                console.log("oi");
              }}
            ></FlowButton>
          </th>
          <th>
            <FlowButton
              text="Observação"
              onClick={() => {
                console.log("oi");
              }}
            ></FlowButton>
          </th>
        </tr>
      </thead>
      <tbody style={{ textAlign: "center" }}>
        {schedules.map((schedule) => (
          <tr key={schedule.id}>
            <td>{schedule.client?.fullName || "N/A"}</td>
            <td>{schedule.dateTime || "N/A"}</td>
            <td>{schedule.price != null ? `$${schedule.price}` : "N/A"}</td>
            <td>{schedule.observation || "N/A"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default SchedulesTable;
