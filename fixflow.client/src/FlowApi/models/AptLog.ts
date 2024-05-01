/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptSchedule } from './AptSchedule';
import type { Business } from './Business';
import type { Client } from './Client';
export type AptLog = {
    id: string;
    /**
     * The Id of the Client who took the Appointment
     */
    clientId: string;
    client?: Client;
    /**
     * The Id of the Business who owns this Contact
     */
    businessId: string;
    business?: Business;
    /**
     * The Id of the Schedule that precedes this Log, if any
     */
    scheduleId?: string | null;
    schedule?: AptSchedule;
    /**
     * The DateTime when the Log was registered
     */
    dateTime?: string;
    price?: number;
    /**
     * Special information about the Appointment, if applicable
     */
    observation?: string | null;
};

