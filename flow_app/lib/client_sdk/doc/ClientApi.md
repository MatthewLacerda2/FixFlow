# openapi.api.ClientApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiV1ClientGet**](ClientApi.md#apiv1clientget) | **GET** /api/v1/Client | Gets a number of filtered Clients
[**apiV1ClientIdGet**](ClientApi.md#apiv1clientidget) | **GET** /api/v1/Client/{id} | Get Client Record in the Business.  Credentials, but also schedules and logs history
[**apiV1ClientPatch**](ClientApi.md#apiv1clientpatch) | **PATCH** /api/v1/Client | Updates the Client with the given Id
[**apiV1ClientPost**](ClientApi.md#apiv1clientpost) | **POST** /api/v1/Client | Create a Client Account


# **apiV1ClientGet**
> BuiltList<BuiltList<ClientDTO>> apiV1ClientGet(businessId, offset, limit, fullname)

Gets a number of filtered Clients

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getClientApi();
final String businessId = businessId_example; // String | 
final int offset = 56; // int | 
final int limit = 56; // int | 
final String fullname = fullname_example; // String | 

try {
    final response = api.apiV1ClientGet(businessId, offset, limit, fullname);
    print(response);
} catch on DioException (e) {
    print('Exception when calling ClientApi->apiV1ClientGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **businessId** | **String**|  | [optional] 
 **offset** | **int**|  | [optional] 
 **limit** | **int**|  | [optional] 
 **fullname** | **String**|  | [optional] 

### Return type

[**BuiltList&lt;BuiltList&lt;ClientDTO&gt;&gt;**](BuiltList.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1ClientIdGet**
> ClientRecord apiV1ClientIdGet(id, clientId)

Get Client Record in the Business.  Credentials, but also schedules and logs history

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getClientApi();
final String id = id_example; // String | 
final String clientId = clientId_example; // String | 

try {
    final response = api.apiV1ClientIdGet(id, clientId);
    print(response);
} catch on DioException (e) {
    print('Exception when calling ClientApi->apiV1ClientIdGet: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **String**|  | 
 **clientId** | **String**|  | [optional] 

### Return type

[**ClientRecord**](ClientRecord.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1ClientPatch**
> ClientDTO apiV1ClientPatch(clientDTO)

Updates the Client with the given Id

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getClientApi();
final ClientDTO clientDTO = ; // ClientDTO | 

try {
    final response = api.apiV1ClientPatch(clientDTO);
    print(response);
} catch on DioException (e) {
    print('Exception when calling ClientApi->apiV1ClientPatch: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientDTO** | [**ClientDTO**](ClientDTO.md)|  | [optional] 

### Return type

[**ClientDTO**](ClientDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **apiV1ClientPost**
> ClientDTO apiV1ClientPost(clientCreate)

Create a Client Account

### Example
```dart
import 'package:openapi/api.dart';

final api = Openapi().getClientApi();
final ClientCreate clientCreate = ; // ClientCreate | 

try {
    final response = api.apiV1ClientPost(clientCreate);
    print(response);
} catch on DioException (e) {
    print('Exception when calling ClientApi->apiV1ClientPost: $e\n');
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientCreate** | [**ClientCreate**](ClientCreate.md)|  | [optional] 

### Return type

[**ClientDTO**](ClientDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

