/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptLog } from '../models/AptLog';
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
     * @param clientId Filter by a specific Client
     * @param minPrice Minimum Price of the Appointments
     * @param maxPrice Maximum Price of the Appointments
     * @param minDateTime The oldest DateTime the Appointment took place
     * @param maxDateTime The most recent DateTime the Appointment took placet
     * @param sort Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending
     * @param offset Offsets the result by a given amount
     * @param limit Limits the result by a given amount
     * @returns AptLog Returns an array of AppointmentLog
     * @throws ApiError
     */
    public static getApiV1Logs1(
        clientId?: string,
        minPrice?: number,
        maxPrice?: number,
        minDateTime?: string,
        maxDateTime?: string,
        sort?: string,
        offset?: number,
        limit?: number,
    ): CancelablePromise<Array<Array<AptLog>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/logs',
            query: {
                'ClientId': clientId,
                'minPrice': minPrice,
                'maxPrice': maxPrice,
                'minDateTime': minDateTime,
                'maxDateTime': maxDateTime,
                'sort': sort,
                'offset': offset,
                'limit': limit,
            },
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
