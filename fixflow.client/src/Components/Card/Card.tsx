import React from 'react';
import './Card.css';

interface CardProps {
  title: string;
  items?: React.ReactNode[] | string[]; // Making 'items' optional
  children?: React.ReactNode; // Adding children prop
}

const Card: React.FC<CardProps> = ({ title, items, children }) => {
  return (
    <div className="card">
      <h2 className="title">{title}</h2>
      <div className="line"></div>
      <div className="content">
        {items?.map((item) => (
          <div className="item">
            {item}
          </div>
        ))}
      </div>
      {children}
    </div>
  );
};

export default Card;