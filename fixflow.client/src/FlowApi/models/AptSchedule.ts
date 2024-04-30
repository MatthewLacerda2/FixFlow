/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptContact } from './AptContact';
import type { Business } from './Business';
import type { Client } from './Client';
export type AptSchedule = {
    id: string;
    /**
     * The Id of the Client who made the Schedule
     */
    clientId: string;
    client?: Client;
    /**
     * The Id of the Business who owns this Contact
     */
    businessId: string;
    business?: Business;
    /**
     * The Id of the Contact that precedes this Schedule, if any
     */
    contactId: string;
    contact?: AptContact;
    /**
     * The scheduled DateTime of the Appointment
     */
    dateTime?: string;
    price?: number;
    observation?: string | null;
};

