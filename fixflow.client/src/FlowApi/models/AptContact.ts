/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptLog } from './AptLog';
import type { Business } from './Business';
import type { Client } from './Client';
export type AptContact = {
    id: string;
    /**
     * The Id of the Client to Contact
     */
    clientId: string;
    client?: Client;
    /**
     * The Id of the Business who owns this Contact
     */
    businessId: string;
    business?: Business;
    /**
     * The Id of the Log that precedes this Contact
     */
    aptLogId: string;
    aptLog?: AptLog;
    /**
     * The Date to Contact the Client
     * The Time is used because, chances are, there is a better Time of the day to contact the Client
     */
    dateTime?: string;
};

