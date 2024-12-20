# openapi
No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)

This Dart package is automatically generated by the [OpenAPI Generator](https://openapi-generator.tech) project:

- API version: 1.0
- Generator version: 7.8.0
- Build package: org.openapitools.codegen.languages.DartClientCodegen

## Requirements

Dart 2.12 or later

## Installation & Usage

### Github
If this Dart package is published to Github, add the following dependency to your pubspec.yaml
```
dependencies:
  openapi:
    git: https://github.com/GIT_USER_ID/GIT_REPO_ID.git
```

### Local
To use the package in your local drive, add the following dependency to your pubspec.yaml
```
dependencies:
  openapi:
    path: /path/to/openapi
```

## Tests

TODO

## Getting Started

Please follow the [installation procedure](#installation--usage) and then run the following:

```dart
import 'package:openapi/api.dart';


final api_instance = AccountsApi();

try {
    final result = api_instance.apiV1AccountsGet();
    print(result);
} catch (e) {
    print('Exception when calling AccountsApi->apiV1AccountsGet: $e\n');
}

```

## Documentation for API Endpoints

All URIs are relative to *http://localhost*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*AccountsApi* | [**apiV1AccountsGet**](doc//AccountsApi.md#apiv1accountsget) | **GET** /api/v1/accounts | 
*AccountsApi* | [**apiV1AccountsLogoutPost**](doc//AccountsApi.md#apiv1accountslogoutpost) | **POST** /api/v1/accounts/logout | 
*AccountsApi* | [**apiV1AccountsPost**](doc//AccountsApi.md#apiv1accountspost) | **POST** /api/v1/accounts | Login with an email and password
*AptContactApi* | [**apiV1ContactsDelete**](doc//AptContactApi.md#apiv1contactsdelete) | **DELETE** /api/v1/contacts | Deletes the Appointment Contact of the given Id
*AptContactApi* | [**apiV1ContactsGet**](doc//AptContactApi.md#apiv1contactsget) | **GET** /api/v1/contacts | Get a number of Contacts
*AptContactApi* | [**apiV1ContactsPatch**](doc//AptContactApi.md#apiv1contactspatch) | **PATCH** /api/v1/contacts | Update the Appointment Contact's of the given Id
*AptLogApi* | [**apiV1LogsDelete**](doc//AptLogApi.md#apiv1logsdelete) | **DELETE** /api/v1/logs | Deletes the Appointment Log of the given Id
*AptLogApi* | [**apiV1LogsGet**](doc//AptLogApi.md#apiv1logsget) | **GET** /api/v1/logs | Gets a number of filtered Logs
*AptLogApi* | [**apiV1LogsPatch**](doc//AptLogApi.md#apiv1logspatch) | **PATCH** /api/v1/logs | Update the Appointment Log of the given Id
*AptLogApi* | [**apiV1LogsPost**](doc//AptLogApi.md#apiv1logspost) | **POST** /api/v1/logs | Creates a Log
*AptScheduleApi* | [**apiV1SchedulesDelete**](doc//AptScheduleApi.md#apiv1schedulesdelete) | **DELETE** /api/v1/schedules | Deletes the Appointment Schedule with the given Id
*AptScheduleApi* | [**apiV1SchedulesGet**](doc//AptScheduleApi.md#apiv1schedulesget) | **GET** /api/v1/schedules | Gets a number of filtered Schedules
*AptScheduleApi* | [**apiV1SchedulesPatch**](doc//AptScheduleApi.md#apiv1schedulespatch) | **PATCH** /api/v1/schedules | Update the Appointment Schedule of the given Id
*AptScheduleApi* | [**apiV1SchedulesPost**](doc//AptScheduleApi.md#apiv1schedulespost) | **POST** /api/v1/schedules | Creates an Appointment Schedule
*BusinessApi* | [**apiV1BusinessDeactivatePatch**](doc//BusinessApi.md#apiv1businessdeactivatepatch) | **PATCH** /api/v1/Business/deactivate | Deactivates the Business Account of the given Id.  That freezes subscription and stops notifications
*BusinessApi* | [**apiV1BusinessDelete**](doc//BusinessApi.md#apiv1businessdelete) | **DELETE** /api/v1/Business | Deletes the Business
*BusinessApi* | [**apiV1BusinessGet**](doc//BusinessApi.md#apiv1businessget) | **GET** /api/v1/Business | Gets the Business' Data of the given Id.  Used mostly when the User logs-in or opens the app
*BusinessApi* | [**apiV1BusinessPatch**](doc//BusinessApi.md#apiv1businesspatch) | **PATCH** /api/v1/Business | Updates the Business of the given Id
*BusinessApi* | [**apiV1BusinessPost**](doc//BusinessApi.md#apiv1businesspost) | **POST** /api/v1/Business | Registers a Business User
*BusinessCalendarDayApi* | [**apiV1BusinessCalendarDayGet**](doc//BusinessCalendarDayApi.md#apiv1businesscalendardayget) | **GET** /api/v1/BusinessCalendarDay | Gets all the events for this month
*CustomerApi* | [**apiV1CustomerGet**](doc//CustomerApi.md#apiv1customerget) | **GET** /api/v1/Customer | Gets a number of filtered Customers
*CustomerApi* | [**apiV1CustomerPatch**](doc//CustomerApi.md#apiv1customerpatch) | **PATCH** /api/v1/Customer | Updates the Customer's data of the given Id
*CustomerApi* | [**apiV1CustomerPost**](doc//CustomerApi.md#apiv1customerpost) | **POST** /api/v1/Customer | Create a Customer's Account
*CustomerApi* | [**apiV1CustomerRecordGet**](doc//CustomerApi.md#apiv1customerrecordget) | **GET** /api/v1/Customer/record | Get Customer's Record in the Business
*IdlePeriodApi* | [**apiV1IdlePeriodDelete**](doc//IdlePeriodApi.md#apiv1idleperioddelete) | **DELETE** /api/v1/IdlePeriod | Deletes an Idle Period
*IdlePeriodApi* | [**apiV1IdlePeriodGet**](doc//IdlePeriodApi.md#apiv1idleperiodget) | **GET** /api/v1/IdlePeriod | Gets Idle Periods owned by the Company that start and end within a given time-period
*IdlePeriodApi* | [**apiV1IdlePeriodPost**](doc//IdlePeriodApi.md#apiv1idleperiodpost) | **POST** /api/v1/IdlePeriod | Creates an Idle period
*SubscriptionApi* | [**apiV1SubscriptionGet**](doc//SubscriptionApi.md#apiv1subscriptionget) | **GET** /api/v1/Subscription | Gets Idle Periods owned by the Company that start and end within a given time-period
*SubscriptionApi* | [**apiV1SubscriptionUnpayedGet**](doc//SubscriptionApi.md#apiv1subscriptionunpayedget) | **GET** /api/v1/Subscription/unpayed | Deletes an Idle Period


## Documentation For Models

 - [AptContact](doc//AptContact.md)
 - [AptLog](doc//AptLog.md)
 - [AptSchedule](doc//AptSchedule.md)
 - [Business](doc//Business.md)
 - [BusinessCalendarDay](doc//BusinessCalendarDay.md)
 - [BusinessDTO](doc//BusinessDTO.md)
 - [BusinessRegisterRequest](doc//BusinessRegisterRequest.md)
 - [CreateAptLog](doc//CreateAptLog.md)
 - [CreateAptSchedule](doc//CreateAptSchedule.md)
 - [Customer](doc//Customer.md)
 - [CustomerCreate](doc//CustomerCreate.md)
 - [CustomerDTO](doc//CustomerDTO.md)
 - [CustomerRecord](doc//CustomerRecord.md)
 - [FlowLoginRequest](doc//FlowLoginRequest.md)
 - [IdlePeriod](doc//IdlePeriod.md)
 - [ProblemDetails](doc//ProblemDetails.md)
 - [Subscription](doc//Subscription.md)
 - [UpdateAptContact](doc//UpdateAptContact.md)
 - [UpdateAptLog](doc//UpdateAptLog.md)


## Documentation For Authorization

Endpoints do not require authorization.


## Author



