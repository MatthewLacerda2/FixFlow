import React from "react";
import Card from "../../Components/Card/Card";
import EmployeeSidebar from "../../Components/EmployeeSidebar/EmployeeSidebar";

const EmployeePage: React.FC = () => {
  return (
    <div className="user-page">
      <EmployeeSidebar />
      <div className="cards-container">
        <Card title="Appointments today" />
        <Card title="Reminders today" />
      </div>
    </div>
  );
};

export default EmployeePage;
