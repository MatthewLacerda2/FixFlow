import SidebarLink from "../SidebarLink/SidebarLink";

const ClientSiderbar: React.FC = () => {
  return (
    <div className="sidebar">
      <SidebarLink route="ManageEntries" text="Manage Entries" />
      <SidebarLink route="Leaderboard" text="Leaderboarder" />
      <SidebarLink route="/User/Settings" text="Settings" />
    </div>
  );
};

export default ClientSiderbar;
