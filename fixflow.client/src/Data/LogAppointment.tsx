import TimeInterval from "./TimeInterval";
import CompletedStatus from "./enumCompletedStatus";

class LogAppointment {
    id: string = '';
    attendantId: string = '';
    clientId: string = '';
    status: CompletedStatus = CompletedStatus.Successful;
    price: number = 30;
    scheduleId: string = '';
    interval: TimeInterval = new TimeInterval();  //PLACEHOLDER
    place: string = '';
}

export default LogAppointment;