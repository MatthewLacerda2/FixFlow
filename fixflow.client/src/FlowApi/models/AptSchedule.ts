/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type AptSchedule = {
    id: string;
    /**
     * The Id of the Client who took the Appointment
     */
    clientId: string;
    /**
     * The Id of the Reminder that precedes this Schedule, if applicable
     */
    reminderId?: string | null;
    /**
     * The Date to Contact the Client
     */
    dateTime?: string;
    price?: number;
    observation?: string | null;
};

