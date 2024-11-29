# openapi.api.AptContactApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1ContactsDelete**](AptContactApi.md#apiv1contactsdelete) | **DELETE** /api/v1/contacts | Deletes the Appointment Contact with the given Id
[**apiV1ContactsGet**](AptContactApi.md#apiv1contactsget) | **GET** /api/v1/contacts | Gets a number of filtered Contacts
[**apiV1ContactsPatch**](AptContactApi.md#apiv1contactspatch) | **PATCH** /api/v1/contacts | Update the Appointment Contact's DateTime with the given Id


# **apiV1ContactsDelete**
> apiV1ContactsDelete(body)

Deletes the Appointment Contact with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptContactApi();
final body = String(); // String | 

try {
    api_instance.apiV1ContactsDelete(body);
} catch (e) {
    print('Exception when calling AptContactApi->apiV1ContactsDelete: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1ContactsGet**
> List<AptContact> apiV1ContactsGet(businessId, clientName, minDateTime, maxDateTime, offset, limit)

Gets a number of filtered Contacts

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptContactApi();
final businessId = businessId_example; // String | 
final clientName = clientName_example; // String | 
final minDateTime = 2013-10-20T19:20:30+01:00; // DateTime | 
final maxDateTime = 2013-10-20T19:20:30+01:00; // DateTime | 
final offset = 56; // int | 
final limit = 56; // int | 

try {
    final result = api_instance.apiV1ContactsGet(businessId, clientName, minDateTime, maxDateTime, offset, limit);
    print(result);
} catch (e) {
    print('Exception when calling AptContactApi->apiV1ContactsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessId** | **String**|  | [optional] 
 **clientName** | **String**|  | [optional] 
 **minDateTime** | **DateTime**|  | [optional] 
 **maxDateTime** | **DateTime**|  | [optional] 
 **offset** | **int**|  | [optional] 
 **limit** | **int**|  | [optional] 

### Return type

[**List<AptContact>**](AptContact.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1ContactsPatch**
> AptContact apiV1ContactsPatch(updateAptContact)

Update the Appointment Contact's DateTime with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptContactApi();
final updateAptContact = UpdateAptContact(); // UpdateAptContact | 

try {
    final result = api_instance.apiV1ContactsPatch(updateAptContact);
    print(result);
} catch (e) {
    print('Exception when calling AptContactApi->apiV1ContactsPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **updateAptContact** | [**UpdateAptContact**](UpdateAptContact.md)|  | [optional] 

### Return type

[**AptContact**](AptContact.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

