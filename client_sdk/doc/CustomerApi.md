# openapi.api.CustomerApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1CustomerGet**](CustomerApi.md#apiv1customerget) | **GET** /api/v1/Customer | Gets a number of filtered Customers
[**apiV1CustomerPatch**](CustomerApi.md#apiv1customerpatch) | **PATCH** /api/v1/Customer | Updates the Customer's data of the given Id
[**apiV1CustomerPost**](CustomerApi.md#apiv1customerpost) | **POST** /api/v1/Customer | Create a Customer's Account
[**apiV1CustomerRecordGet**](CustomerApi.md#apiv1customerrecordget) | **GET** /api/v1/Customer/record | Get Customer's Record in the Business


# **apiV1CustomerGet**
> List<CustomerDTO> apiV1CustomerGet(offset, limit, fullname)

Gets a number of filtered Customers

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = CustomerApi();
final offset = 56; // int | 
final limit = 56; // int | 
final fullname = fullname_example; // String | 

try {
    final result = api_instance.apiV1CustomerGet(offset, limit, fullname);
    print(result);
} catch (e) {
    print('Exception when calling CustomerApi->apiV1CustomerGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **offset** | **int**|  | [optional] 
 **limit** | **int**|  | [optional] 
 **fullname** | **String**|  | [optional] 

### Return type

[**List<CustomerDTO>**](CustomerDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1CustomerPatch**
> CustomerDTO apiV1CustomerPatch(customerDTO)

Updates the Customer's data of the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = CustomerApi();
final customerDTO = CustomerDTO(); // CustomerDTO | 

try {
    final result = api_instance.apiV1CustomerPatch(customerDTO);
    print(result);
} catch (e) {
    print('Exception when calling CustomerApi->apiV1CustomerPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **customerDTO** | [**CustomerDTO**](CustomerDTO.md)|  | [optional] 

### Return type

[**CustomerDTO**](CustomerDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1CustomerPost**
> CustomerDTO apiV1CustomerPost(customerCreate)

Create a Customer's Account

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = CustomerApi();
final customerCreate = CustomerCreate(); // CustomerCreate | 

try {
    final result = api_instance.apiV1CustomerPost(customerCreate);
    print(result);
} catch (e) {
    print('Exception when calling CustomerApi->apiV1CustomerPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **customerCreate** | [**CustomerCreate**](CustomerCreate.md)|  | [optional] 

### Return type

[**CustomerDTO**](CustomerDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1CustomerRecordGet**
> CustomerRecord apiV1CustomerRecordGet(customerId)

Get Customer's Record in the Business

### Example
```dart
import 'package:openapi/api.dart';

final api_instance = CustomerApi();
final customerId = customerId_example; // String | 

try {
    final result = api_instance.apiV1CustomerRecordGet(customerId);
    print(result);
} catch (e) {
    print('Exception when calling CustomerApi->apiV1CustomerRecordGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **customerId** | **String**|  | [optional] 

### Return type

[**CustomerRecord**](CustomerRecord.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

