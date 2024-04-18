/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { EmployeeDTO } from '../models/EmployeeDTO';
import type { EmployeeRegister } from '../models/EmployeeRegister';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class EmployeeService {
    /**
     * Get the Employee with the given Id
     * @param id The Client's Id
     * @returns EmployeeDTO The Employee's DTO
     * @throws ApiError
     */
    public static getApiV1Employee(
        id: string,
    ): CancelablePromise<Array<EmployeeDTO>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/employee/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Employee with the given Id`,
            },
        });
    }
    /**
     * Deletes the Employee with the given Id
     * @param id The Id of the Employee to be deleted
     * @returns any Employee was found, and thus deleted
     * @throws ApiError
     */
    public static deleteApiV1Employee(
        id: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/employee/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Employee with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Employees, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param username Filters results to only Users whose username contains this string
     * @param offset Offsets the result by a given amount
     * @param limit Limits the number of results
     * @param sort Orders the result by a given field. Does not order if the field does not exist
     * @returns EmployeeDTO Returns an array of EmployeeDTO
     * @throws ApiError
     */
    public static getApiV1Employee1(
        username?: string,
        offset?: number,
        limit?: number,
        sort?: string,
    ): CancelablePromise<Array<Array<EmployeeDTO>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/employee',
            query: {
                'username': username,
                'offset': offset,
                'limit': limit,
                'sort': sort,
            },
        });
    }
    /**
     * Creates a Employee User
     * @param requestBody
     * @returns EmployeeDTO EmployeeDTO
     * @throws ApiError
     */
    public static postApiV1Employee(
        requestBody?: EmployeeRegister,
    ): CancelablePromise<EmployeeDTO> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/employee',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The Client's (PhoneNumber || CPF || Email) does not exist`,
                500: `Server Error`,
            },
        });
    }
    /**
     * Updates the Employee with the given Id
     * @param requestBody
     * @returns EmployeeDTO Updated Employee's DTO
     * @throws ApiError
     */
    public static patchApiV1Employee(
        requestBody?: EmployeeRegister,
    ): CancelablePromise<EmployeeDTO> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/employee',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no Employee with the given Id`,
            },
        });
    }
}
