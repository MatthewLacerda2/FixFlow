import './SideBar.css';

const SideBar = () => {
  return (
    <div className="sidebar">
      <button className="bar-item">Something</button>
      <div className='line'/>
      <br/>
      <button className="bar-item">Else</button>
      <div className='line'/>
      <br/>
      <button className="bar-item">Anything</button>
      <div className='line'/>
    </div>
  );
};

export default SideBar;