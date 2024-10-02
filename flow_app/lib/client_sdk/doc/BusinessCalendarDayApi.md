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
> BuiltList<BusinessCalendarDay> apiV1BusinessCalendarDayGet(body)

Login with an email and password

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getBusinessCalendarDayApi();
final String body = body_example; // String | 

try {
    final response = api.apiV1BusinessCalendarDayGet(body);
    print(response);
} catch on DioException (e) {
    print('Exception when calling BusinessCalendarDayApi->apiV1BusinessCalendarDayGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | **String**|  | [optional] 

### Return type

[**BuiltList&lt;BusinessCalendarDay&gt;**](BusinessCalendarDay.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

