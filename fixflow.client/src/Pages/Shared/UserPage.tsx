import React from 'react';
import Card from '../../Components/Card/Card';
import LogExpendable from '../../Components/LogExpendable/LogExpandable';
import Actions from '../../Components/Actions/Actions';

const UserPage: React.FC = () => {
  
  const generateLogs = (count: number) => {
    const logs = [];
    for (let i = 0; i < count; i++) {
      logs.push(<LogExpendable key={i} name={`Person ${i + 1}`} />);
    }
    return logs;
  };

  return (
    <div>
      <Actions/>
      <div style={{ display: 'flex' }}>
        <Card
          title="Log of Appointments"
          items={generateLogs(2)} // Generating 2 LogExpendables as placeholder data
        />
        <Card
          title="Scheduled Appointments"
          items={generateLogs(3)} // Generating 3 LogExpendables as placeholder data
        />
      </div>
    </div>
  );
};

export default UserPage;