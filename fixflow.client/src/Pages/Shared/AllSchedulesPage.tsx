import React, { useState } from 'react';
import './AllSchedulesPage.css';
import ScheduleAppointment from '../../Data/ScheduleAppointment';
//filter and paginate this shit
const AllSchedulesPage: React.FC = () => {
  const [sortBy, setSortBy] = useState<string | null>(null);
  const [sortDescending, setSortDescending] = useState<boolean>(false);

  // Sample array of ScheduleAppointment
  const data: ScheduleAppointment[] = [
    {
      id: '1',
      clientId: 'client1',
      attendantId: 'attendant1',
      secretaryId: 'secretary1',
      expectedPrice: 30,
      dateTime: new Date(),
      observation: 'Observation 1',
    },
    {
      id: '2',
      clientId: 'client2',
      attendantId: 'attendant2',
      secretaryId: 'secretary2',
      expectedPrice: 40,
      dateTime: new Date(),
      observation: 'Observation 2',
    },
    {
      id: '2',
      clientId: 'client2',
      attendantId: 'attendant2',
      secretaryId: 'secretary2',
      expectedPrice: 40,
      dateTime: new Date(),
      observation: 'Observation 2',
    },
    {
      id: '2',
      clientId: 'client2',
      attendantId: 'attendant2',
      secretaryId: 'secretary2',
      expectedPrice: 40,
      dateTime: new Date(),
      observation: 'Observation 2',
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
    const aValue: string | number | Date = a[sortBy as keyof ScheduleAppointment]; // Type annotation for aValue
    const bValue: string | number | Date = b[sortBy as keyof ScheduleAppointment]; // Type annotation for bValue
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
            <button className="sort-button" onClick={() => toggleSort('clientId')}>
              Client ID
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('attendantId')}>
              Attendant ID
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('secretaryId')}>
              Secretary ID
            </button>
          </th>
          <th>
            <button className="sort-button" onClick={() => toggleSort('expectedPrice')}>
              Expected Price
            </button>
          </th>
        </tr>
      </thead>
      <tbody>
        {sortedData.map((appointment, index) => (
          <tr key={index}>
            <td>{appointment.clientId}</td>
            <td>{appointment.attendantId}</td>
            <td>{appointment.secretaryId}</td>
            <td>{appointment.expectedPrice}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default AllSchedulesPage;
