/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AptContact } from '../models/AptContact';
import type { AptContactFilter } from '../models/AptContactFilter';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class AptContactService {
    /**
     * Get the Contact with the given Id
     * @param id The Contact's Id
     * @returns AptContact The AppointmentContact with the given Id
     * @throws ApiError
     */
    public static getApiV1Contacts(
        id: string,
    ): CancelablePromise<AptContact> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/contacts/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                404: `There was no Appointment Contact with the given Id`,
            },
        });
    }
    /**
     * Deletes the Appointment Contact with the given Id
     * @param id The Id of the AptContact to be deleted
     * @returns void
     * @throws ApiError
     */
    public static deleteApiV1Contacts(
        id: string,
    ): CancelablePromise<void> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/v1/contacts/{Id}',
            path: {
                'Id': id,
            },
            errors: {
                400: `There was no Contact with the given Id`,
            },
        });
    }
    /**
     * Gets a number of Appointment Contacts, with optional filters
     * Does not return Not Found, but an Array of size 0 instead
     * @param requestBody The Filter Properties of the Query
     * @returns AptContact Returns an array of AppointmentContact
     * @throws ApiError
     */
    public static getApiV1Contacts1(
        requestBody?: AptContactFilter,
    ): CancelablePromise<Array<Array<AptContact>>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/v1/contacts',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * Create an Appointment Contact
     * @param requestBody
     * @returns any The created Appointment Contact
     * @returns AptContact Created
     * @throws ApiError
     */
    public static postApiV1Contacts(
        requestBody?: AptContact,
    ): CancelablePromise<any | AptContact> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/v1/contacts',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `The given (ClientId || LogId) does not exist`,
            },
        });
    }
    /**
     * Update the Appointment Contact with the given Id
     * @param requestBody
     * @returns AptContact The updated Appointment Contact
     * @throws ApiError
     */
    public static putApiV1Contacts(
        requestBody?: AptContact,
    ): CancelablePromise<AptContact> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/v1/contacts',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                400: `There was no AptContact with the given Id`,
            },
        });
    }
}
