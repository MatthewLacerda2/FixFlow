# openapi.model.Business

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **String** |  | [optional] 
**userName** | **String** |  | [optional] 
**normalizedUserName** | **String** |  | [optional] 
**email** | **String** |  | [optional] 
**normalizedEmail** | **String** |  | [optional] 
**emailConfirmed** | **bool** |  | [optional] 
**passwordHash** | **String** |  | [optional] 
**securityStamp** | **String** |  | [optional] 
**concurrencyStamp** | **String** |  | [optional] 
**phoneNumber** | **String** |  | [optional] 
**phoneNumberConfirmed** | **bool** |  | [optional] 
**twoFactorEnabled** | **bool** |  | [optional] 
**lockoutEnd** | [**DateTime**](DateTime.md) |  | [optional] 
**lockoutEnabled** | **bool** |  | [optional] 
**accessFailedCount** | **int** |  | [optional] 
**createdDate** | [**DateTime**](DateTime.md) |  | [optional] 
**lastLogin** | [**DateTime**](DateTime.md) |  | [optional] 
**isActive** | **bool** |  | [optional] 
**name** | **String** | The Name of the Business or Business owner | [optional] 
**cnpj** | **String** | CNPJ. Must be on format XX.XXX.XXX/XXXX-XX | [optional] 
**businessDays** | [**BuiltList&lt;BusinessDay&gt;**](BusinessDay.md) | The DateTimes of the week where the business is open | [optional] 
**allowListedServicesOnly** | **bool** |  | [optional] 
**openOnHolidays** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


