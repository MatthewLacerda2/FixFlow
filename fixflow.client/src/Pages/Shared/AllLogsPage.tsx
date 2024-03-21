import React, { useState } from 'react';
import './AllSchedulesPage.css';
import LogAppointment from '../../Data/LogAppointment';
import CompletedStatus from '../../Data/enumCompletedStatus';
import TimeInterval from '../../Data/TimeInterval';
//filter and paginate this shit
const AllLogsPage: React.FC = () => {
  const [sortBy, setSortBy] = useState<string | null>(null);
  const [sortDescending, setSortDescending] = useState<boolean>(false);

  // Sample array of LogAppointment
  const data: LogAppointment[] = [
    {
      id: '1',
      attendantId: 'attendant1',
      clientId: 'client1',
      status: CompletedStatus.Successful,
      price: 30,
      scheduleId: 'client1',
      interval: new TimeInterval(),
      place: 'plaza'
    },
    {
        id: '1',
        attendantId: 'attendant1',
        clientId: 'client1',
        status: CompletedStatus.Successful,
        price: 30,
        scheduleId: 'client1',
        interval: new TimeInterval(),
        place: 'plaza',
    },
    {
        id: '1',
        attendantId: 'attendant1',
        clientId: 'client1',
        status: CompletedStatus.Successful,
        price: 30,
        scheduleId: 'client1',
        interval: new TimeInterval(),
        place: 'plaza',
    },
    {
        id: '1',
        attendantId: 'attendant1',
        clientId: 'client1',
        status: CompletedStatus.Successful,
        price: 30,
        scheduleId: 'client1',
        interval: new TimeInterval(),
        place: 'plaza',
    },
  ];

  const toggleSort = (field: string) => {
    if (sortBy === field) {
      setSortDescending(!sortDescending);
    } else {
      setSortBy(field);
      setSortDescending(false);
    }
  };

  const sortedData = sortBy ? [...data].sort((a, b) => {
    const aValue: string | number | TimeInterval = a[sortBy as keyof LogAppointment]; // Type annotation for aValue
    const bValue: string | number | TimeInterval = b[sortBy as keyof LogAppointment]; // Type annotation for bValue
    if (aValue < bValue) return sortDescending ? 1 : -1;
    if (aValue > bValue) return sortDescending ? -1 : 1;
    return 0;
  }) : data;

  return (
    <table className="schedule-table">
      <br></br>
      <thead>
        <tr>
          <th>
            <button className="sort-button" onClick={() => toggleSort('attendantId')}>
                Attendant ID
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('clientId')}>
                Client ID
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('price')}>
              Price
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('scheduleId')}>
              Schedule
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('place')}>
              Place
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('status')}>
              Status
            </button>
          </th>
        </tr>
      </thead>
      <tbody>
        {sortedData.map((appointment, index) => (
            //attendantId, clientId, status, price, scheduleId, interval, place
          <tr key={index}>
            <td>{appointment.attendantId}</td>
            <td>{appointment.clientId}</td>
            <td>{appointment.price}</td>
            <td>{appointment.scheduleId}</td>
            <td>{appointment.place}</td>
            <td>{appointment.status}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default AllLogsPage;
