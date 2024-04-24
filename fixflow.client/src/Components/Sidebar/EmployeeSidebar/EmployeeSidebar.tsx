import "../../../Styles/SideBar.css";
import SidebarLink from "../SidebarLink/SidebarLink";

const EmployeeSidebar: React.FC = () => {
  return (
    <div className="sidebar">
      <SidebarLink route="/sh/reminders" text="All Reminders" />
      <SidebarLink route="/sh/schedules" text="All Schedules" />
      <SidebarLink route="/sh/appointments" text="All Appointments" />
      <SidebarLink route="/sh/clients" text="Clients" />
      <SidebarLink route="/sh/settings" text="Settings" />
    </div>
  );
};

export default EmployeeSidebar;
