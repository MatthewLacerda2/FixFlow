/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type AptContact = {
    id: string;
    /**
     * The Id of the Client to Contact
     */
    clientId: string;
    /**
     * The Id of the Business who owns this Contact
     */
    businessId: string;
    /**
     * The Id of the Log that precedes this Contact
     */
    aptLogId: string;
    /**
     * The Date to Contact the Client
     * The Time is used because, chances are, there is a better Time of the day to contact the Client
     */
    dateTime?: string;
};

