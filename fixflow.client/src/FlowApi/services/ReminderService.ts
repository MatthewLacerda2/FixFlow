/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptReminder } from '../models/AptReminder';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ReminderService {
    /**
     * Get the Reminder with the given Id
     * @param id The Reminder's Id
     * @returns AptReminder The AppointmentReminder with the given Id
     * @throws ApiError
     */
    public static getApiV1Reminders(
        id: string,
    ): CancelablePromise<AptReminder> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/reminders/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Appointment Reminder with the given Id`,
            },
        });
    }
    /**
     * Deletes the Appointment Reminder with the given Id
     * @param id The Id of the AptReminder to be deleted
     * @returns void
     * @throws ApiError
     */
    public static deleteApiV1Reminders(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/reminders/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Reminder with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Appointment Reminders, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param clientId Filter by a specific Client
     * @param minDateTime The nearest Reminder set up
     * @param maxDateTime The furthest Reminder set up
     * @param sort Orders the result by Client, or DateTime. Add suffix 'desc' to order descending
     * @param offset Offsets the result by a given amount
     * @param limit Limits the result by a given amount
     * @returns AptReminder Returns an array of AppointmentReminder
     * @throws ApiError
     */
    public static getApiV1Reminders1(
        clientId?: string,
        minDateTime?: string,
        maxDateTime?: string,
        sort?: string,
        offset?: number,
        limit?: number,
    ): CancelablePromise<Array<Array<AptReminder>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/reminders',
            query: {
                'ClientId': clientId,
                'minDateTime': minDateTime,
                'maxDateTime': maxDateTime,
                'sort': sort,
                'offset': offset,
                'limit': limit,
            },
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * Create an Appointment Reminder
     * @param requestBody
     * @returns any The created Appointment Reminder
     * @returns AptReminder Created
     * @throws ApiError
     */
    public static postApiV1Reminders(
        requestBody?: AptReminder,
    ): CancelablePromise<any | AptReminder> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/reminders',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The given (ClientId || LogId) does not exist`,
            },
        });
    }
    /**
     * Update the Appointment Reminder with the given Id
     * @param requestBody
     * @returns AptReminder The updated Appointment Reminder
     * @throws ApiError
     */
    public static putApiV1Reminders(
        requestBody?: AptReminder,
    ): CancelablePromise<AptReminder> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/v1/reminders',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no AptReminder with the given Id`,
            },
        });
    }
}
