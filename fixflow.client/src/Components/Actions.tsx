import React from 'react';
import './Actions.css';

const Actions: React.FC = () => {
  return (
    <div className="actions">
      <button className="action-button">All Logs</button>
      <button className="action-button">All Schedules</button>
      <button className="action-button">Create Schedule</button>
    </div>
  );
};

export default Actions;
