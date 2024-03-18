import React from 'react';
import './MainPage.css';
import Card from '../Components/Card';
import LogExpendable from '../Components/LogExpandable';

const generateLogs = (count: number) => {
  const logs = [];
  for (let i = 0; i < count; i++) {
    logs.push(<LogExpendable key={i} name={`Person ${i + 1}`} />);
  }
  return logs;
};

const MainPage: React.FC = () => {
  return (
    <div className="main-content">
      <h1 className="tete">Your Scheduling Platform</h1>
      <p className="subtitle">To Fix Your WorkFlow</p>

      <br/>
      
      <div style={{ display: 'flex' }}>
        <Card
          title="Log of Appointments"
          items={generateLogs(2)} // Generating 2 LogExpendables as placeholder data
        />
        <Card
          title="Scheduled Appointments"
          items={generateLogs(3)} // Generating 3 LogExpendables as placeholder data
        />
        <Card
          title="Scheduled Appointments"
          items={generateLogs(3)} // Generating 3 LogExpendables as placeholder data
        />
      </div>
    </div>
  );
};

export default MainPage;