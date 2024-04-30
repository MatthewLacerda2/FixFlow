/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { BusinessDTO } from '../models/BusinessDTO';
import type { BusinessRegister } from '../models/BusinessRegister';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class BusinessService {
    /**
     * Get the Business with the given Id
     * @param id The Client's Id
     * @returns BusinessDTO The Business's DTO
     * @throws ApiError
     */
    public static getApiV1Business(
        id: string,
    ): CancelablePromise<Array<BusinessDTO>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/business/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Business with the given Id`,
            },
        });
    }
    /**
     * Deletes the Business with the given Id
     * @param id The Id of the Business to be deleted
     * @returns any Business was found, and thus deleted
     * @throws ApiError
     */
    public static deleteApiV1Business(
        id: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/business/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Business with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Business, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param username Filters results to only Users whose username contains this string
     * @param offset Offsets the result by a given amount
     * @param limit Limits the number of results
     * @param sort Orders the result by a given field. Does not order if the field does not exist
     * @returns BusinessDTO Returns an array ofBusinessDTO
     * @throws ApiError
     */
    public static getApiV1Business1(
        username?: string,
        offset?: number,
        limit?: number,
        sort?: string,
    ): CancelablePromise<Array<Array<BusinessDTO>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/business',
            query: {
                'username': username,
                'offset': offset,
                'limit': limit,
                'sort': sort,
            },
        });
    }
    /**
     * Creates a Business User
     * @param requestBody
     * @returns BusinessDTO BusinessDTO
     * @throws ApiError
     */
    public static postApiV1Business(
        requestBody?: BusinessRegister,
    ): CancelablePromise<BusinessDTO> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/business',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The Client's (PhoneNumber || CPF || Email) does not exist`,
                500: `Server Error`,
            },
        });
    }
    /**
     * Updates the Business with the given Id
     * @param requestBody
     * @returns BusinessDTO Updated Business's DTO
     * @throws ApiError
     */
    public static patchApiV1Business(
        requestBody?: BusinessRegister,
    ): CancelablePromise<BusinessDTO> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/business',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no Business with the given Id`,
            },
        });
    }
}
