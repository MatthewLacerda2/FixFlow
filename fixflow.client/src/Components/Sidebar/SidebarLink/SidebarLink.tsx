import React from "react";
import { Link } from "react-router-dom";

type SidebarLinkProps = {
  route: string;
  text: string;
  color?: string;
};

const SidebarLink: React.FC<SidebarLinkProps> = ({ route, text, color }) => {
  const linkStyle: React.CSSProperties = color ? { color: color } : {};

  return (
    <div>
      <Link to={route} className="bar-item" style={linkStyle}>
        {text}
      </Link>
      <div className="line" />
      <br />
    </div>
  );
};

export default SidebarLink;
