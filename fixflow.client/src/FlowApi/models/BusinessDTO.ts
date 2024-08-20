/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type BusinessDTO = {
    id: string;
    /**
     * NickName. Must not contain spaces
     */
    name: string;
    /**
     * CPF. Must be on format XXX.XXX.XXX-XX
     */
    cpf?: string | null;
    /**
     * CNPJ. Must be on format XX.XXX.XXX/XXXX-XX
     */
    cnpj?: string | null;
    description?: string | null;
    /**
     * Phone Number. Must contain only numbers
     */
    phoneNumber: string;
    email?: string | null;
};

