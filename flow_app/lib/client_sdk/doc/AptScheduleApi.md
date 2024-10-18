# openapi.api.AptScheduleApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1SchedulesDelete**](AptScheduleApi.md#apiv1schedulesdelete) | **DELETE** /api/v1/schedules | Deletes the Appointment Schedule with the given Id
[**apiV1SchedulesGet**](AptScheduleApi.md#apiv1schedulesget) | **GET** /api/v1/schedules | Gets a number of filtered Schedules
[**apiV1SchedulesPatch**](AptScheduleApi.md#apiv1schedulespatch) | **PATCH** /api/v1/schedules | Update the Appointment Schedule with the given Id
[**apiV1SchedulesPost**](AptScheduleApi.md#apiv1schedulespost) | **POST** /api/v1/schedules | Creates an Appointment Schedule


# **apiV1SchedulesDelete**
> apiV1SchedulesDelete(body)

Deletes the Appointment Schedule with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptScheduleApi();
final body = String(); // String | 

try {
    api_instance.apiV1SchedulesDelete(body);
} catch (e) {
    print('Exception when calling AptScheduleApi->apiV1SchedulesDelete: $e\n');
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

# **apiV1SchedulesGet**
> List<List<AptSchedule>> apiV1SchedulesGet(body)

Gets a number of filtered Schedules

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptScheduleApi();
final body = Object(); // Object | 

try {
    final result = api_instance.apiV1SchedulesGet(body);
    print(result);
} catch (e) {
    print('Exception when calling AptScheduleApi->apiV1SchedulesGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **Object**|  | [optional] 

### Return type

[**List<List<AptSchedule>>**](List.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1SchedulesPatch**
> AptSchedule apiV1SchedulesPatch(aptSchedule)

Update the Appointment Schedule with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptScheduleApi();
final aptSchedule = AptSchedule(); // AptSchedule | 

try {
    final result = api_instance.apiV1SchedulesPatch(aptSchedule);
    print(result);
} catch (e) {
    print('Exception when calling AptScheduleApi->apiV1SchedulesPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **aptSchedule** | [**AptSchedule**](AptSchedule.md)|  | [optional] 

### Return type

[**AptSchedule**](AptSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1SchedulesPost**
> AptSchedule apiV1SchedulesPost(createAptSchedule)

Creates an Appointment Schedule

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = AptScheduleApi();
final createAptSchedule = CreateAptSchedule(); // CreateAptSchedule | 

try {
    final result = api_instance.apiV1SchedulesPost(createAptSchedule);
    print(result);
} catch (e) {
    print('Exception when calling AptScheduleApi->apiV1SchedulesPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **createAptSchedule** | [**CreateAptSchedule**](CreateAptSchedule.md)|  | [optional] 

### Return type

[**AptSchedule**](AptSchedule.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
