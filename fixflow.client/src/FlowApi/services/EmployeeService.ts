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
     * @param id
     * @returns EmployeeDTO Success
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
                404: `Not Found`,
            },
        });
    }
    /**
     * @param id
     * @returns void
     * @throws ApiError
     */
    public static deleteApiV1Employee(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/employee/{Id}',
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
     * @returns EmployeeDTO Success
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
     * @param requestBody
     * @returns EmployeeDTO Success
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
                400: `Bad Request`,
                500: `Server Error`,
            },
        });
    }
    /**
     * @param requestBody
     * @returns EmployeeDTO Success
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
                400: `Bad Request`,
            },
        });
    }
}
