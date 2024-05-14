import "../../../Styles/SideBar.css";
import { FLOW_ROUTES } from "../../../Utils/FlowRoutes";
import SidebarLink from "../SidebarLink/SidebarLink";

const BusinessSidebar: React.FC = () => {
  return (
    <div className="sidebar">
      <SidebarLink
        route={FLOW_ROUTES.business.agendamentos}
        text="Agendamentos"
      />
      <SidebarLink
        route={FLOW_ROUTES.business.atendimentos}
        text="Atendimentos"
      />
      <SidebarLink route={FLOW_ROUTES.business.contatos} text="Contatos" />
      <SidebarLink route={FLOW_ROUTES.business.settings} text="Settings" />
    </div>
  );
};

export default BusinessSidebar;
