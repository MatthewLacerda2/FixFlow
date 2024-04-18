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
     * @param id
     * @returns AptReminder Success
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
                404: `Not Found`,
            },
        });
    }
    /**
     * @param id
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
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param clientId
     * @param minDateTime
     * @param maxDateTime
     * @param sort
     * @param offset
     * @param limit
     * @returns AptReminder Success
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
     * @param requestBody
     * @returns AptReminder Created
     * @throws ApiError
     */
    public static postApiV1Reminders(
        requestBody?: AptReminder,
    ): CancelablePromise<AptReminder> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/reminders',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns AptReminder Success
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
                400: `Bad Request`,
            },
        });
    }
}
