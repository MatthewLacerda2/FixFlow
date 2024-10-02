# openapi.model.CreateAptLog

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**clientId** | **String** | The Id of the Client who took the Appointment | 
**businessId** | **String** | The Id of the Business who owns this Contact | 
**scheduleId** | **String** | The Id of the Schedule that precedes this Log, if any | [optional] 
**dateTime** | [**DateTime**](DateTime.md) | The DateTime when the Log was registered | [optional] 
**price** | **double** |  | [optional] 
**service** | **String** |  | [optional] 
**description** | **String** |  | [optional] 
**whenShouldClientComeBack** | [**DateTime**](DateTime.md) | The Date when we expect the Client to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


