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
     * Get a Client with the given Id
     * @param id The Client's Id
     * @returns ClientDTO The ClientDTO
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
                404: `There was no Client with the given Id`,
            },
        });
    }
    /**
     * Deletes the Client with the given Id
     * @param id The Id of the Client to be deleted
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
                400: `There was no Client with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Clients, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param fullname Filter the Clients whose fullname contain the given string
     * @param offset Offsets the result by a given amount
     * @param limit Limits the result by a given amount
     * @param sort Orders the result by Client, Price or DateTime. Add suffix 'desc' to order descending
     * @returns ClientDTO Returns an array of ClientDTO
     * @throws ApiError
     */
    public static getApiV1Client1(
        fullname?: string,
        offset?: number,
        limit?: number,
        sort?: string,
    ): CancelablePromise<Array<Array<ClientDTO>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/client',
            query: {
                'fullname': fullname,
                'offset': offset,
                'limit': limit,
                'sort': sort,
            },
        });
    }
    /**
     * Create a Client Account
     * @param requestBody
     * @returns any The created Client's DTO
     * @returns ClientDTO Created
     * @throws ApiError
     */
    public static postApiV1Client(
        requestBody?: ClientRegister,
    ): CancelablePromise<any | ClientDTO> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/client',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The Client's (PhoneNumber || CPF || Email) does not exist`,
                500: `Server Error`,
            },
        });
    }
    /**
     * Updates the Client with the given Id
     * @param requestBody
     * @returns ClientDTO Updated Client's DTO
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
                400: `There was no Client with the given Id`,
            },
        });
    }
}
