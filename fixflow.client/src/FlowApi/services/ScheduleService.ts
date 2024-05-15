/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptSchedule } from '../models/AptSchedule';
import type { AptScheduleFilter } from '../models/AptScheduleFilter';
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
     * @param requestBody The Filter Properties of the Query
     * @returns AptSchedule Returns an array of AppointmentSchedule
     * @throws ApiError
     */
    public static getApiV1Schedules1(
        requestBody?: AptScheduleFilter,
    ): CancelablePromise<Array<Array<AptSchedule>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/schedules',
            body: requestBody,
            mediaType: 'application/json',
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
