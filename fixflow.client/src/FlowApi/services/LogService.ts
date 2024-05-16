/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptLog } from '../models/AptLog';
import type { AptLogFilter } from '../models/AptLogFilter';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class LogService {
    /**
     * Get the Log with the given Id
     * @param id The Log's Id
     * @returns AptLog The Appointment Log
     * @throws ApiError
     */
    public static getApiV1Logs(
        id: string,
    ): CancelablePromise<AptLog> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/logs/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Appointment Log with the given Id`,
            },
        });
    }
    /**
     * Deletes the Appointment Log with the given Id
     * @param id The Id of the AptLog to be deleted
     * @returns void
     * @throws ApiError
     */
    public static deleteApiV1Logs(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/logs/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Log with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Appointment Logs, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param requestBody The Filter Properties of the Query
     * @returns AptLog Returns an array of AppointmentLog
     * @throws ApiError
     */
    public static getApiV1Logs1(
        requestBody?: AptLogFilter,
    ): CancelablePromise<Array<Array<AptLog>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/logs',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * Create an Appointment Log
     * @param requestBody
     * @returns any The created Appointment Log
     * @returns AptLog Created
     * @throws ApiError
     */
    public static postApiV1Logs(
        requestBody?: AptLog,
    ): CancelablePromise<any | AptLog> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/logs',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The given (ClientId || ScheduleId) does not exist`,
            },
        });
    }
    /**
     * Update the Appointment Log with the given Id
     * @param requestBody
     * @returns AptLog The updated Appointment Log
     * @throws ApiError
     */
    public static putApiV1Logs(
        requestBody?: AptLog,
    ): CancelablePromise<AptLog> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/v1/logs',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no AptLog with the given Id`,
            },
        });
    }
}
