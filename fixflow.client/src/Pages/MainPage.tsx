import React from 'react';
import './MainPage.css';
import TextCard from '../Components/Card/TextCard';

const MainPage: React.FC = () => {
  return (
    <div className="main-content">
      <h1 className="tete">Your Scheduling Platform</h1>
      <p className="subtitle">To Fix Your WorkFlow</p>

      <br/>
      
      <div style={{ display: 'flex' }}>
        <TextCard
          title="Client"
          items={["Schedule your appointments quick and easily"]}
        />
        <TextCard
          title="Employee"
          items={["Find all appointments to do in a quick and intuitive way"]}
        />
        <TextCard
          title="Secretary"
          items={["Manage and overview all the office's appointments easily"]}
        />

      </div>
    </div>
  );
};

export default MainPage;