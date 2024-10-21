# openapi.api.AptLogApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1LogsDelete**](AptLogApi.md#apiv1logsdelete) | **DELETE** /api/v1/logs | Deletes the Appointment Log with the given Id
[**apiV1LogsGet**](AptLogApi.md#apiv1logsget) | **GET** /api/v1/logs | Gets a number of filtered Logs
[**apiV1LogsPatch**](AptLogApi.md#apiv1logspatch) | **PATCH** /api/v1/logs | Update the Appointment Log with the given Id
[**apiV1LogsPost**](AptLogApi.md#apiv1logspost) | **POST** /api/v1/logs | Creates a Log


# **apiV1LogsDelete**
> apiV1LogsDelete(body)

Deletes the Appointment Log with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptLogApi();
final body = String(); // String | 

try {
    api_instance.apiV1LogsDelete(body);
} catch (e) {
    print('Exception when calling AptLogApi->apiV1LogsDelete: $e\n');
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

# **apiV1LogsGet**
> List<List<AptLog>> apiV1LogsGet(client, service, minPrice, maxPrice, minDateTime, maxDateTime, offset, limit)

Gets a number of filtered Logs

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptLogApi();
final client = client_example; // String | 
final service = service_example; // String | 
final minPrice = 3.4; // double | 
final maxPrice = 3.4; // double | 
final minDateTime = 2013-10-20T19:20:30+01:00; // DateTime | 
final maxDateTime = 2013-10-20T19:20:30+01:00; // DateTime | 
final offset = 56; // int | 
final limit = 56; // int | 

try {
    final result = api_instance.apiV1LogsGet(client, service, minPrice, maxPrice, minDateTime, maxDateTime, offset, limit);
    print(result);
} catch (e) {
    print('Exception when calling AptLogApi->apiV1LogsGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **client** | **String**|  | [optional] 
 **service** | **String**|  | [optional] 
 **minPrice** | **double**|  | [optional] 
 **maxPrice** | **double**|  | [optional] 
 **minDateTime** | **DateTime**|  | [optional] 
 **maxDateTime** | **DateTime**|  | [optional] 
 **offset** | **int**|  | [optional] 
 **limit** | **int**|  | [optional] 

### Return type

[**List<List<AptLog>>**](List.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1LogsPatch**
> AptLog apiV1LogsPatch(updateAptLog)

Update the Appointment Log with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptLogApi();
final updateAptLog = UpdateAptLog(); // UpdateAptLog | 

try {
    final result = api_instance.apiV1LogsPatch(updateAptLog);
    print(result);
} catch (e) {
    print('Exception when calling AptLogApi->apiV1LogsPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **updateAptLog** | [**UpdateAptLog**](UpdateAptLog.md)|  | [optional] 

### Return type

[**AptLog**](AptLog.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1LogsPost**
> AptLog apiV1LogsPost(createAptLog)

Creates a Log

Generates a Contact

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptLogApi();
final createAptLog = CreateAptLog(); // CreateAptLog | 

try {
    final result = api_instance.apiV1LogsPost(createAptLog);
    print(result);
} catch (e) {
    print('Exception when calling AptLogApi->apiV1LogsPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **createAptLog** | [**CreateAptLog**](CreateAptLog.md)|  | [optional] 

### Return type

[**AptLog**](AptLog.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

