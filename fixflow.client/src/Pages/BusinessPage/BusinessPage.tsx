import React from "react";
import Card from "../../Components/Card/Card";
import BusinessSidebar from "../../Components/Sidebar/BusinessSidebar/BusinessSidebar";
import Expandable from "../../Components/Expandable/Expandable";

const BusinessPage: React.FC = () => {
  const expandableProps = {
    title: "Expandable Section",
    content: ["Item 1", "Item 2", "Item 3"],
  };

  return (
    <div className="user-page">
      <BusinessSidebar />
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
