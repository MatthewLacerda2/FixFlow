import React from "react";
import "./Expandable.css";

interface ExpandableProps {
  title: string;
  content: string[];
}

const Expandable: React.FC<ExpandableProps> = ({ title, content }) => {
  return (
    <div className="expandable-container">
      <div className="expandable-header">
        <h2>{title}</h2>
      </div>
      <div className="expandable-content">
        {content.map((item) => (
          <p>{item}</p>
        ))}
      </div>
    </div>
  );
};

export default Expandable;
