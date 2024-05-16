import React from "react";
import BusinessSidebar from "../../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import "../../../Styles/Form.css";

const ContactsPage: React.FC = () => {
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
          type="text"
          placeholder="Observação"
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
    </div>
  );
};
export default ContactsPage;
