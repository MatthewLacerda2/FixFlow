import React from 'react';
import Card from '../../Components/Card/Card';
import LogExpendable from '../../Components/LogExpendable/LogExpandable';

const UserSettingsPage: React.FC = () => {
  
  const generateLogs = (count: number) => {
    const logs = [];
    for (let i = 0; i < count; i++) {
      logs.push(<LogExpendable key={i} name={`Person ${i + 1}`} />);
    }
    return logs;
  };

  return (
    <div style={{ display: 'flex' }}>
      <Card
        title="User Preferences"
        items={generateLogs(2)}
      />
      <Card
        title="Page Settings"
        items={generateLogs(3)}
      />
    </div>
  );
};

export default UserSettingsPage;