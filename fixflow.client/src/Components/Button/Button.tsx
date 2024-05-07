import React, { CSSProperties } from "react";
import "./Button.css";

interface ButtonProps {
  text: string;
  onClick: () => void;
  style?: CSSProperties;
}

const Button: React.FC<ButtonProps> = ({ text, onClick, style }) => {
  return (
    <button className="custom-button" style={style} onClick={onClick}>
      {text}
    </button>
  );
};

export default Button;
