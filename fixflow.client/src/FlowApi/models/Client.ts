/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
export type Client = {
    id?: string | null;
    userName?: string | null;
    normalizedUserName?: string | null;
    email?: string | null;
    normalizedEmail?: string | null;
    emailConfirmed?: boolean;
    passwordHash?: string | null;
    securityStamp?: string | null;
    concurrencyStamp?: string | null;
    phoneNumber?: string | null;
    phoneNumberConfirmed?: boolean;
    twoFactorEnabled?: boolean;
    lockoutEnd?: string | null;
    lockoutEnabled?: boolean;
    accessFailedCount?: number;
    createdDate?: string;
    lastLogin?: string;
    fullName?: string | null;
    cpf?: string | null;
    additionalNote?: string | null;
    /**
     * Whether or not the Account was registered by a Client
     *
     * If not, this value is false,
     * thus Client didn't insert a password and this account is not supposed to be logged in
     */
    signedUp?: boolean;
};

