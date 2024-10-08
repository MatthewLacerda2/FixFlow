# openapi.api.BusinessCalendarDayApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1BusinessCalendarDayGet**](BusinessCalendarDayApi.md#apiv1businesscalendardayget) | **GET** /api/v1/BusinessCalendarDay | Login with an email and password


# **apiV1BusinessCalendarDayGet**
> List<BusinessCalendarDay> apiV1BusinessCalendarDayGet(body)

Login with an email and password

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessCalendarDayApi();
final body = String(); // String | 

try {
    final result = api_instance.apiV1BusinessCalendarDayGet(body);
    print(result);
} catch (e) {
    print('Exception when calling BusinessCalendarDayApi->apiV1BusinessCalendarDayGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

[**List<BusinessCalendarDay>**](BusinessCalendarDay.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

