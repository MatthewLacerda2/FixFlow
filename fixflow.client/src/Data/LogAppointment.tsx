import TimeInterval from "./TimeInterval";
import CompletedStatus from "./enumCompletedStatus";

interface LogAppointment {
    id: string;
    attendantId: string;
    clientId: string;
    status: CompletedStatus;
    price: number;
    scheduleId: string;
    interval: TimeInterval;  //PLACEHOLDER
    place: string;
}

export default LogAppointment;