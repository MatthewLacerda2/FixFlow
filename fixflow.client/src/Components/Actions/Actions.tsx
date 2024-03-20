import React from 'react';
import './Actions.css';
import { Link } from 'react-router-dom';

const Actions: React.FC = () => {
  return (
    <div className="actions">
      <Link to="AppointmentSchedule" className="action-button">Create Schedule</Link>
      <Link to="AllSchedules" className="action-button">All Schedules</Link>
      <Link to="AllLogs" className="action-button">All Logs</Link>
    </div>
  );
};

export default Actions;