import React, { useState } from 'react';
import './LogExpandable.css';

interface LogExpendableProps {
  name: string;
}

const LogExpendable: React.FC<LogExpendableProps> = ({ name }) => {
  const [expanded, setExpanded] = useState(false);

  const generateRandomDateTime = () => {
    const startDate = new Date('2024-01-01T08:00:00');
    const endDate = new Date('2024-01-01T17:00:00');
    const randomDateTime = new Date(
      startDate.getTime() + Math.random() * (endDate.getTime() - startDate.getTime())
    );
    return randomDateTime.toLocaleString();
  };

  const dateTime = generateRandomDateTime();

  const handleToggleExpand = () => {
    setExpanded(!expanded);
  };

  return (
    <div className="log-expendable">
      <div className="header" onClick={handleToggleExpand}>
        <div className="dateTime">{dateTime}</div>
        <div className="arrow">{expanded ? '▼' : '▶'}</div>
      </div>
      {expanded && <div className="name">{name}</div>}
    </div>
  );
};

export default LogExpendable;
