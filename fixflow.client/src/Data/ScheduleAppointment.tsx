interface ScheduleAppointment {

    id: string;
    clientId: string;
    attendantId: string;
    secretaryId: string;
    expectedPrice: number;
    dateTime: Date;
    observation: string;

}

export default ScheduleAppointment;