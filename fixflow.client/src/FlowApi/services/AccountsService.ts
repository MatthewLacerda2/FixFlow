/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { FlowLoginRequest } from '../models/FlowLoginRequest';
import type { PasswordReset } from '../models/PasswordReset';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class AccountsService {
    /**
     * Get a Token when Logging in, with either UserName or Email, and Password
     * @param requestBody
     * @returns any Successfull login
     * @throws ApiError
     */
    public static postApiV1Accounts(
        requestBody?: FlowLoginRequest,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/accounts',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                401: `Unauthorized login`,
            },
        });
    }
    /**
     * Sends an email with the link for resetting the password
     * @param requestBody
     * @returns string OK
     * @throws ApiError
     */
    public static postApiV1AccountsResetEmail(
        requestBody?: string,
    ): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/accounts/reset/email',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
                404: `Email not found`,
            },
        });
    }
    /**
     * Changes password for the User who wanted to reset it
     * User must have the link sent in the 'Password Reset' email
     * @param requestBody
     * @returns string Password change successfull
     * @throws ApiError
     */
    public static patchApiV1AccountsResetRequest(
        requestBody?: PasswordReset,
    ): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/accounts/reset/request',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `Bad Request`,
                401: `Password change unsucessfull`,
            },
        });
    }
    /**
     * Returns the PasswordReset belonging to that token
     * Used when the User tries to access a 'Password Reset' link
     * @param token The Token for the Password Reset Request
     * @returns PasswordReset OK
     * @throws ApiError
     */
    public static patchApiV1AccountsResetLink(
        token?: string,
    ): CancelablePromise<PasswordReset> {
        return __request(OpenAPI, {
            method: 'PATCH',
            url: '/api/v1/accounts/reset/link',
            query: {
                'token': token,
            },
            errors: {
                400: `Bad Request`,
            },
        });
    }
    /**
     * Logout method
     * @returns any Logout successfull
     * @throws ApiError
     */
    public static postApiV1AccountsLogout(): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/accounts/logout',
            errors: {
                401: `Unauthorized`,
                500: `Logout unsucessfull. Probable Internal Server Error`,
            },
        });
    }
}
