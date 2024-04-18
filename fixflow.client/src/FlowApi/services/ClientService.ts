/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ClientDTO } from '../models/ClientDTO';
import type { ClientRegister } from '../models/ClientRegister';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class ClientService {
    /**
     * @param id
     * @returns ClientDTO Success
     * @throws ApiError
     */
    public static getApiV1Client(
        id: string,
    ): CancelablePromise<Array<ClientDTO>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/client/{Id}',
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
    public static deleteApiV1Client(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/client/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * @param username
     * @param offset
     * @param limit
     * @param sort
     * @returns ClientDTO Success
     * @throws ApiError
     */
    public static getApiV1Client1(
        username?: string,
        offset?: number,
        limit?: number,
        sort?: string,
    ): CancelablePromise<Array<Array<ClientDTO>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/client',
            query: {
                'username': username,
                'offset': offset,
                'limit': limit,
                'sort': sort,
            },
        });
    }
    /**
     * @param requestBody
     * @returns ClientDTO Created
     * @throws ApiError
     */
    public static postApiV1Client(
        requestBody?: ClientRegister,
    ): CancelablePromise<ClientDTO> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/client',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
                500: `Server Error`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns ClientDTO Success
     * @throws ApiError
     */
    public static patchApiV1Client(
        requestBody?: ClientRegister,
    ): CancelablePromise<ClientDTO> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/client',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
            },
        });
    }
}
