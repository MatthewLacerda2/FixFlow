/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { FlowLoginRequest } from '../models/FlowLoginRequest';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class LoginService {
    /**
     * Get a Token when Logging in, with either UserName or Email, and Password
     * @param requestBody
     * @returns any Successfull login
     * @throws ApiError
     */
    public static postApiV1Login(
        requestBody?: FlowLoginRequest,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/login',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized login`,
            },
        });
    }
    /**
     * Change password of the User with the given Email
     * @param requestBody
     * @returns string Password change successfull
     * @throws ApiError
     */
    public static patchApiV1Login(
        requestBody?: FlowLoginRequest,
    ): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/login',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Unauthorized`,
                401: `Password change unsucessfull`,
            },
        });
    }
    /**
     * Logout method
     * @returns any Logout successfull
     * @throws ApiError
     */
    public static postApiV1LoginLogout(): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/login/logout',
            errors: {
                401: `Unauthorized`,
                500: `Logout unsucessfull. Probable Internal Server Error`,
            },
        });
    }
}
