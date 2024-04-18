/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type ClientDTO = {
    id: string;
    fullName: string;
    /**
     * CPF. Must be only precisely 11 numbers
     */
    cpf?: string | null;
    /**
     * Special information about the Client, if applicable
     */
    additionalNote?: string | null;
    /**
     * NickName. Must not contain spaces
     */
    userName?: string | null;
    /**
     * Phone Number. Must contain only numbers, and may be preceded by a '+'
     */
    phoneNumber: string;
    email?: string | null;
};

