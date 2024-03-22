import React from 'react';

interface DataProps {
  [key: string]: string | number;
}

const DisplayData: React.FC<{ data: DataProps }> = ({ data }) => {
  return (
    <div>
      {Object.entries(data).map(([key, value]) => (
        <div key={key}>
          <strong>{key}: </strong>
          {value}
        </div>
      ))}
    </div>
  );
};

export default DisplayData;