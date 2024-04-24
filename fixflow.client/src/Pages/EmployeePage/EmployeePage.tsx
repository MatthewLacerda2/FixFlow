import React from "react";
import Card from "../../Components/Card/Card";

const EmployeePage: React.FC = () => {
  return (
    <div>
      <Card title="Appointments today" />
      <Card title="Reminders today" />
    </div>
  );
};

export default EmployeePage;
