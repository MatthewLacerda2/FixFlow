/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type AptLog = {
    id: string;
    /**
     * The Id of the Client who took the Appointment
     */
    clientId: string;
    /**
     * The Id of the Schedule that precedes the Log, if any
     */
    scheduleId?: string | null;
    /**
     * The DateTime when the Log was created
     */
    dateTime?: string;
    price?: number;
    /**
     * Special information about the Appointment, if applicable
     */
    observation?: string | null;
};

