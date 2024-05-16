import React from "react";
import { useParams } from "react-router-dom";

const SchedulePage: React.FC = () => {
  const id = useParams();

  return (
    <div>
      <p>Schedule</p>
    </div>
  );
};

export default SchedulePage;
