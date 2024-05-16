import React from "react";
import { AptContact } from "../../../../../../FlowApi";
import FlowButton from "../../../../../FlowButton/FlowButton";

type ContactsTableProps = {
  contacts: AptContact[];
};

const ContactsTable: React.FC<ContactsTableProps> = ({ contacts }) => {
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
              text="Ultimo Consulta"
              onClick={() => {
                console.log("oi");
              }}
            ></FlowButton>
          </th>
        </tr>
      </thead>
      <tbody style={{ textAlign: "center" }}>
        {contacts.map((contact) => (
          <tr key={contact.id}>
            <td>{contact.client?.fullName || "N/A"}</td>
            <td>{contact.dateTime || "N/A"}</td>
            <td>{contact.aptLog?.dateTime || "N/A"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default ContactsTable;
