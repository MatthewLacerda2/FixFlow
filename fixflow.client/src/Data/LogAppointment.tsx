import CompletedStatus from "./enumCompletedStatus";

class LogAppointment {
    id: string = '';
    attendantId: string = '';
    clientId: string = '';
    status: CompletedStatus = CompletedStatus.Successful;
    price: number = 30;
    scheduleId: string = '';
    interval: string = '';  //PLACEHOLDER
    observation: string = '';
    place: string = '';
}
  
export default LogAppointment;  