/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptSchedule } from '../models/AptSchedule';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ScheduleService {
    /**
     * Get the Schedule with the given Id
     * @param id The Schedule's Id
     * @returns AptSchedule The AppointmentSchedule with the given Id
     * @throws ApiError
     */
    public static getApiV1Schedules(
        id: string,
    ): CancelablePromise<AptSchedule> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/schedules/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Appointment Schedule with the given Id`,
            },
        });
    }
    /**
     * Deletes the Appointment Schedule with the given Id
     * @param id The Id of the AptSchedule to be deleted
     * @returns void
     * @throws ApiError
     */
    public static deleteApiV1Schedules(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/schedules/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Schedule with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Appointment Schedules, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param clientId Filter by a specific Client
     * @param minPrice Minimum Price of the Appointments
     * @param maxPrice Maximum Price of the Appointments
     * @param minDateTime The nearest Contact set up
     * @param maxDateTime The furthest Contact set up
     * @param sort Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending
     * @param offset Offsets the result by a given amount
     * @param limit Limits the result by a given amount
     * @returns AptSchedule Returns an array of AppointmentSchedule
     * @throws ApiError
     */
    public static getApiV1Schedules1(
        clientId?: string,
        minPrice?: number,
        maxPrice?: number,
        minDateTime?: string,
        maxDateTime?: string,
        sort?: string,
        offset?: number,
        limit: number = 10,
    ): CancelablePromise<Array<Array<AptSchedule>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/schedules',
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
     * Create an Appointment Schedule
     * @param requestBody
     * @returns any The created Appointment Schedule
     * @returns AptSchedule Created
     * @throws ApiError
     */
    public static postApiV1Schedules(
        requestBody?: AptSchedule,
    ): CancelablePromise<any | AptSchedule> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/schedules',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The given (ClientId || ContactId) does not exist`,
            },
        });
    }
    /**
     * Update the Appointment Schedule with the given Id
     * @param requestBody
     * @returns AptSchedule The updated Appointment Schedule
     * @throws ApiError
     */
    public static putApiV1Schedules(
        requestBody?: AptSchedule,
    ): CancelablePromise<AptSchedule> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/v1/schedules',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no AptSchedule with the given Id`,
            },
        });
    }
}
