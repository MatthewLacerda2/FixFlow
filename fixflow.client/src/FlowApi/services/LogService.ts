/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptLog } from '../models/AptLog';
import type { CompletedStatus } from '../models/CompletedStatus';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class LogService {
    /**
     * @param id
     * @returns AptLog Success
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
                404: `Not Found`,
            },
        });
    }
    /**
     * @param id
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
     * @param status
     * @param sort
     * @param offset
     * @param limit
     * @returns AptLog Success
     * @throws ApiError
     */
    public static getApiV1Logs1(
        clientId?: string,
        minPrice?: number,
        maxPrice?: number,
        minDateTime?: string,
        maxDateTime?: string,
        status?: CompletedStatus,
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
                'status': status,
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
     * @returns AptLog Created
     * @throws ApiError
     */
    public static postApiV1Logs(
        requestBody?: AptLog,
    ): CancelablePromise<AptLog> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/logs',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns AptLog Success
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
                400: `Bad Request`,
            },
        });
    }
}
