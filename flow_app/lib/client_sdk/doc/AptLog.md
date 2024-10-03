# openapi.model.AptLog

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **String** |  | 
**customerId** | **String** | The Id of the Customer who took the Appointment | 
**customer** | [**Customer**](Customer.md) |  | [optional] 
**businessId** | **String** | The Id of the Business who owns this Contact | 
**scheduleId** | **String** | The Id of the Schedule that precedes this Log, if any | [optional] 
**dateTime** | [**DateTime**](DateTime.md) | The DateTime when the Log was registered | [optional] 
**service** | **String** |  | [optional] 
**price** | **double** |  | [optional] 
**description** | **String** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


