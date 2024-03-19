import React from 'react';
import './Actions.css';
import { Link } from 'react-router-dom';

const Actions: React.FC = () => {
  return (
    <div className="actions">
      <Link to="/AllLogs" className="action-button">Create Schedule</Link>
      <Link to="/AllSchedules" className="action-button">Create Schedule</Link>
      <Link to="/AppointmentSchedule" className="action-button">Create Schedule</Link>
    </div>
  );
};

export default Actions;