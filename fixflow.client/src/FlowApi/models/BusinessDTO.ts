/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type BusinessDTO = {
    id: string;
    name: string;
    /**
     * CPF. Must be precisely XXX.XXX.XXX-XX
     */
    cpf?: string | null;
    cnpj?: string | null;
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

