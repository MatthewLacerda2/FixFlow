/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type AptReminder = {
    id: string;
    /**
     * The Id of the Client who took the Appointment
     */
    clientId: string;
    /**
     * The Id of the Appointment Log that precedes this Reminder
     */
    previousAppointmentId: string;
    /**
     * The Date to Contact the Client
     */
    dateTime?: string;
};

