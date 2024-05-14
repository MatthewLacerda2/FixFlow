import React from "react";
import Card from "../../Components/Card/Card";
import BusinessSidebar from "../../Components/Sidebar/BusinessSidebar/BusinessSidebar";

const BusinessPage: React.FC = () => {
  return (
    <div className="user-page">
      <BusinessSidebar />
      <div className="cards-container">
        <Card>
          <Card title="Reminders today"></Card>
          <Card title="Reminders today"></Card>
          <Card title="Reminders today"></Card>
        </Card>
      </div>
    </div>
  );
};

export default BusinessPage;
