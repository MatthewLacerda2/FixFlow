import React from "react";
import BusinessSidebar from "../../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import "../../../Styles/Form.css";
import ContactsTable from "../../../Components/PagesComponents/MainPage/BusinessPage/ContactsPage/ContactsTable/ContactsTable";
import { AptContact } from "../../../FlowApi";

const ContactsPage: React.FC = () => {
  const contacts: AptContact[] = [
    {
      id: "1",
      clientId: "c1",
      client: { id: "c1", fullName: "John Doe" },
      aptLogId: "11",
      businessId: "b1",
      dateTime: "2024-05-16T10:00:00Z",
    },
  ];

  return (
    <div className="user-page">
      <BusinessSidebar />
      <div className="form-group">
        <input
          className="input-field "
          type="text"
          placeholder="Nome do Cliente"
        ></input>
        <input
          className="input-field"
          style={{ width: "150px" }}
          type="date"
        ></input>
        {/*min date*/}
        <input
          className="input-field"
          style={{ width: "150px" }}
          type="date"
        ></input>
        {/*max date*/}
      </div>
      <ContactsTable contacts={contacts}></ContactsTable>
    </div>
  );
};
export default ContactsPage;
