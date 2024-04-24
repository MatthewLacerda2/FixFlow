import { Link } from "react-router-dom";
import "../../Styles/SideBar.css";

const EmployeeSidebar: React.FC = () => {
  return (
    <div className="sidebar">
      <Link to="/sh/Reminders" className="bar-item">
        All Reminders
      </Link>
      <div className="line" />
      <br />
      <Link to="/sh/Schedules" className="bar-item">
        All Schedules
      </Link>
      <div className="line" />
      <br />
      <Link to="/sh/Appointments" className="bar-item">
        All Appointments
      </Link>
      <div className="line" />
      <Link to="/sh/Settings" className="bar-item">
        Settings
      </Link>
      <br />
    </div>
  );
};

export default EmployeeSidebar;
