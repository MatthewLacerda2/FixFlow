import './SideBar.css';

const SideBar = () => {
  return (
    <div className="sidebar">
      <button className="bar-item">Something</button>
      <div className='line'></div>
      <br/>
      <button className="bar-item">Else</button>
      <div className='line'></div>
      <br/>
      <button className="bar-item">Anything</button>
      <div className='line'></div>
    </div>
  );
};

export default SideBar;