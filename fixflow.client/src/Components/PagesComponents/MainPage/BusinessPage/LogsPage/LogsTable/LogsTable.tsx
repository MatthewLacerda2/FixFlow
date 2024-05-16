import React from "react";
import { AptLog } from "../../../../../../FlowApi";
import FlowButton from "../../../../../FlowButton/FlowButton";

type LogsTableProps = {
  logs: AptLog[];
};

const LogsTable: React.FC<LogsTableProps> = ({ logs }) => {
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
        {logs.map((logs) => (
          <tr key={logs.id}>
            <td>{logs.client?.fullName || "N/A"}</td>
            <td>{logs.dateTime || "N/A"}</td>
            <td>{logs.price != null ? `$${logs.price}` : "N/A"}</td>
            <td>{logs.observation || "N/A"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default LogsTable;
