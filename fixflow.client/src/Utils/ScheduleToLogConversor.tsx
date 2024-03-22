import LogAppointment from "../Data/LogAppointment";
import ScheduleAppointment from "../Data/ScheduleAppointment";

class ScheduleToLogConversor {
  static convert(schedule: ScheduleAppointment): LogAppointment {
    const logAppointment = new LogAppointment();
    logAppointment.attendantId = schedule.attendantId;
    logAppointment.clientId = schedule.clientId;
    logAppointment.price = schedule.expectedPrice;
    logAppointment.scheduleId = schedule.id;
    logAppointment.interval.start = schedule.dateTime;
    logAppointment.interval.finish = schedule.dateTime;
    return logAppointment;
  }
}

export default ScheduleToLogConversor;