import React from "react";
import Card from "../../Components/Card/Card";
import BusinessSidebar from "../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import Expandable from "../../Components/Expandable/Expandable";
import { AptSchedule } from "../../FlowApi";
import FlowButton from "../../Components/FlowButton/FlowButton";
import "../../Styles/SideBar.css";

const BusinessPage: React.FC = () => {
  /// ATENCAO: ISTO É APENAS UM PLACEHOLDER
  const defaultSchedule: AptSchedule = {
    id: "",
    clientId: "",
    businessId: "",
    client: undefined,
    business: undefined,
    contactId: null,
    contact: undefined,
    dateTime: Date.now().toString(),
    price: null,
    observation: null,
  };
  const schedules: AptSchedule[] = [
    defaultSchedule,
    defaultSchedule,
    defaultSchedule,
  ];

  const expandableProps = {
    title: "Expandable Section",
    content: schedules.map((schedule) => JSON.stringify(schedule)),
  };
  // / ////////////////////////////////////

  return (
    <div
      className="user-page"
      style={{ flexGrow: "1", display: "flex", flexDirection: "column" }}>
      <BusinessSidebar />
      <div style={{ marginBottom: "20px" }}>
        <FlowButton></FlowButton>
      </div>
      <div className="cards-container">
        <Card title="Contatos">
          <Expandable {...expandableProps}></Expandable>
        </Card>
        <Card title="Consultas">
          <Expandable {...expandableProps}></Expandable>
        </Card>
      </div>
    </div>
  );
};

export default BusinessPage;
