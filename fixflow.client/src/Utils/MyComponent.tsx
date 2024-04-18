import React from "react";
import { ClientDTO } from "../FlowApi";

interface MyComponentProps {
  name: string;
}

const MyComponent: React.FC<MyComponentProps> = ({ name }) => {
  return (
    <div>
      <h1>Hello, {name}!</h1>
      <p>This is a functional React component.</p>
    </div>
  );
};

export default MyComponent;
