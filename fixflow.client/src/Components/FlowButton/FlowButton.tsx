import React from "react";
import "./FlowButton.css";

interface ButtonProps {
  text: string;
  onClick: () => void;
}

const FlowButton: React.FC<ButtonProps> = ({ text, onClick }) => {
  return (
    <button className="custom-button" onClick={onClick}>
      {text}
    </button>
  );
};

export default FlowButton;
