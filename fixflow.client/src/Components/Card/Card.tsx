import React from 'react';
import './Card.css';

interface CardProps {
  title: string;
  items: React.ReactNode[];
}

const Card: React.FC<CardProps> = ({ title, items }) => {
  return (
    <div className="card">
      <h2 className="title">{title}</h2>
      <div className="line"></div>
      <div className="content">
        {items.map((item, index) => (
          <div key={index} className="item">
            {item}
          </div>
        ))}
      </div>
    </div>
  );
};

export default Card;