import React from 'react';
import './Card.css';

interface TextCardProps {
  title: string;
  items: string[];
}

const TextCard: React.FC<TextCardProps> = ({ title, items }) => {
  return (
    <div className="card">
      <h2 className="title">{title}</h2>
      <div className="line"></div>
      <div className="content">
        {items.map((item) => (
          <div className="item">
            {item}
          </div>
        ))}
      </div>
    </div>
  );
};

export default TextCard;