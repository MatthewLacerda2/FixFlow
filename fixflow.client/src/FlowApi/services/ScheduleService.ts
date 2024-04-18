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
     * @param id
     * @returns AptSchedule Success
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
                404: `Not Found`,
            },
        });
    }
    /**
     * @param id
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
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param clientId
     * @param minPrice
     * @param maxPrice
     * @param minDateTime
     * @param maxDateTime
     * @param sort
     * @param offset
     * @param limit
     * @returns AptSchedule Success
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
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns AptSchedule Created
     * @throws ApiError
     */
    public static postApiV1Schedules(
        requestBody?: AptSchedule,
    ): CancelablePromise<AptSchedule> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/schedules',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns AptSchedule Success
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
                400: `Bad Request`,
            },
        });
    }
}
