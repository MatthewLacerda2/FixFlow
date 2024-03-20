import React from 'react';
import './MainPage.css';
import Card from '../Components/Card/Card';

const MainPage: React.FC = () => {
  return (
    <div className="main-content">
      <h1 className="tete">Your Scheduling Platform</h1>
      <p className="subtitle">To Fix Your WorkFlow</p>
      
      <br/>
      
      <div style={{ display: 'flex' }}>
        <Card
          title="Client"
          items={["Schedule your appointments quick and easily"]}
        />
        <Card
          title="Employee"
          items={["Find all appointments to do in a quick and intuitive way"]}
        />
        <Card
          title="Secretary"
          items={["Manage and overview all the office's appointments easily"]}
        />
      </div>
    </div>
  );
};

export default MainPage;