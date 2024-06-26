/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type ClientRegister = {
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
     * Phone Number. Must contain only numbers
     */
    phoneNumber: string;
    email?: string | null;
    /**
     * Whether or not the Account was registered by a Client
     *
     * If false, the Client didn't insert a password and this account is not supposed to be logged in
     */
    signedUp: boolean;
    password?: string | null;
    /**
     * Must be identical to 'password'
     */
    confirmPassword?: string | null;
};

