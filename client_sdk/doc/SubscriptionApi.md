# openapi.api.SubscriptionApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1SubscriptionGet**](SubscriptionApi.md#apiv1subscriptionget) | **GET** /api/v1/Subscription | Gets Idle Periods owned by the Company that start and end within a given time-period
[**apiV1SubscriptionUnpayedGet**](SubscriptionApi.md#apiv1subscriptionunpayedget) | **GET** /api/v1/Subscription/unpayed | Deletes an Idle Period


# **apiV1SubscriptionGet**
> List<Subscription> apiV1SubscriptionGet(startMonth, startYear, endMonth, endYear)

Gets Idle Periods owned by the Company that start and end within a given time-period

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = SubscriptionApi();
final startMonth = 56; // int | 
final startYear = 56; // int | 
final endMonth = 56; // int | 
final endYear = 56; // int | 

try {
    final result = api_instance.apiV1SubscriptionGet(startMonth, startYear, endMonth, endYear);
    print(result);
} catch (e) {
    print('Exception when calling SubscriptionApi->apiV1SubscriptionGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **startMonth** | **int**|  | [optional] 
 **startYear** | **int**|  | [optional] 
 **endMonth** | **int**|  | [optional] 
 **endYear** | **int**|  | [optional] 

### Return type

[**List<Subscription>**](Subscription.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1SubscriptionUnpayedGet**
> List<Subscription> apiV1SubscriptionUnpayedGet()

Deletes an Idle Period

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = SubscriptionApi();

try {
    final result = api_instance.apiV1SubscriptionUnpayedGet();
    print(result);
} catch (e) {
    print('Exception when calling SubscriptionApi->apiV1SubscriptionUnpayedGet: $e\n');
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<Subscription>**](Subscription.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

