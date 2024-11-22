# openapi.model.CreateAptLog

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**customerId** | **String** | The Id of the Customer who took the Appointment | 
**scheduleId** | **String** | The Id of the Schedule that precedes this Log, if any | [optional] 
**dateTime** | [**DateTime**](DateTime.md) | The DateTime when the Log was registered | [optional] 
**price** | **double** |  | [optional] 
**service** | **String** |  | [optional] 
**description** | **String** |  | [optional] 
**whenShouldCustomerComeBack** | [**DateTime**](DateTime.md) | The Date when we expect the Customer to schedule another appointment.  We are leaving as DateTime for simplicity but we only need the Date from this class | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


