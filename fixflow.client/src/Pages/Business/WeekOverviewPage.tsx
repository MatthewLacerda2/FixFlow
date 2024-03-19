import './WeekOverview.css';

const WeekOverviewPage = () => {
  // Generate dates for the current week
  const currentDate = new Date();
  const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  const today = currentDate.getDay(); // Index of the current day (0 for Sunday, 1 for Monday, etc.)
  const startDate = new Date(currentDate);
  startDate.setDate(currentDate.getDate() - today); // Start from the beginning of the week (Sunday)

  // Function to handle button click
  const handleClick = (dayIndex : number) => {
    console.log('Clicked on day:', daysOfWeek[dayIndex]);
    // You can add logic to change button colors here
  };

  return (
    <div className="week-overview">
      <div className="days-bar">
        {daysOfWeek.map((day, index) => {
          const date = new Date(startDate);
          date.setDate(startDate.getDate() + index); // Get the date for each day of the week
          const dayOfMonth = date.getDate();
          const monthName = date.toLocaleString('default', { month: 'short' });
          const isToday = index === today;
          return (
            <button
              key={index}
              className={`day-button ${isToday ? 'today' : ''}`}
              onClick={() => handleClick(index)}
            >
              <div className="day-number">{dayOfMonth}</div>
              <div className="month-name">{monthName}</div>
            </button>
          );
        })}
      </div>
      <div className="list-container">
        <div className="list-item">
          <h3>Title</h3>
          <p>Description</p>
        </div>
        {/* Add more list items here */}
      </div>
    </div>
  );
};

export default WeekOverviewPage;