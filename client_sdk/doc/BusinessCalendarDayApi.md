# openapi.api.BusinessCalendarDayApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1BusinessCalendarDayGet**](BusinessCalendarDayApi.md#apiv1businesscalendardayget) | **GET** /api/v1/BusinessCalendarDay | Gets all the events for this month


# **apiV1BusinessCalendarDayGet**
> List<BusinessCalendarDay> apiV1BusinessCalendarDayGet(businessId, year, month)

Gets all the events for this month

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = BusinessCalendarDayApi();
final businessId = businessId_example; // String | 
final year = 56; // int | 
final month = 56; // int | 

try {
    final result = api_instance.apiV1BusinessCalendarDayGet(businessId, year, month);
    print(result);
} catch (e) {
    print('Exception when calling BusinessCalendarDayApi->apiV1BusinessCalendarDayGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessId** | **String**|  | [optional] 
 **year** | **int**|  | [optional] 
 **month** | **int**|  | [optional] 

### Return type

[**List<BusinessCalendarDay>**](BusinessCalendarDay.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

