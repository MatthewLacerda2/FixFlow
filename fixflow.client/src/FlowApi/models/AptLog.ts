/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CompletedStatus } from './CompletedStatus';
export type AptLog = {
    id?: string | null;
    clientId: string;
    scheduleId?: string | null;
    status?: CompletedStatus;
    dateTime?: string;
    price?: number;
    observation?: string | null;
};

