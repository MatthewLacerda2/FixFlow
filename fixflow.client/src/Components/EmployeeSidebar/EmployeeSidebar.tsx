import { Link } from "react-router-dom";

const EmployeeSidebar = () => {
  return (
    <div className="sidebar">
      <Link to="/sh/ManageEntries" className="bar-item">
        Manage Entries
      </Link>
      <div className="line" />
      <br />
      <Link to="/sh/Leaderboard" className="bar-item">
        Leaderboarders
      </Link>
      <div className="line" />
      <br />
      <Link to="/sh/User/Settings" className="bar-item">
        Settings
      </Link>
      <div className="line" />
      <br />
    </div>
  );
};

export default EmployeeSidebar;
