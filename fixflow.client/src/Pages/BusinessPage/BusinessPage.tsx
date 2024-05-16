import React from "react";
import Card from "../../Components/Card/Card";
import BusinessSidebar from "../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import Expandable from "../../Components/Expandable/Expandable";
import FlowButton from "../../Components/FlowButton/FlowButton";
import "../../Styles/SideBar.css";

const BusinessPage: React.FC = () => {
  const expandableProps = {
    title: "Expandable Section",
    content: ["schedules.map((schedule)", "=>", "JSON.stringify(schedule))"],
  };

  function CreateSchedule(): void {
    console.log("create schedule");
  }

  return (
    <div
      className="user-page"
      style={{ flexGrow: "1", display: "flex", flexDirection: "column" }}
    >
      <BusinessSidebar />
      <div style={{ alignItems: "center" }}>
        <FlowButton text="Agendar Atendimento" onClick={CreateSchedule} />
        <FlowButton text="Registrar Atendimento" onClick={CreateSchedule} />
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
