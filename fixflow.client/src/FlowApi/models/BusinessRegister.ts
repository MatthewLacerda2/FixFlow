/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type BusinessRegister = {
    id: string;
    name: string;
    /**
     * CPF. Must be only precisely 11 numbers
     */
    cpf?: string | null;
    cnpj?: string | null;
    /**
     * NickName. Must not contain spaces
     */
    userName?: string | null;
    /**
     * Phone Number. Must contain only numbers and/or a '+'
     */
    phoneNumber: string;
    email?: string | null;
    /**
     * New Password. Only used when registering the user or changing the password
     * For Logging in, use FlowLoginRequest instead
     */
    newPassword?: string | null;
};

