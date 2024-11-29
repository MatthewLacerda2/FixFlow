# openapi.model.AptContact

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **String** |  | 
**customerId** | **String** | The Id of the Customer to Contact | 
**customer** | [**Customer**](Customer.md) |  | [optional] 
**businessId** | **String** | The Id of the Business who owns this Contact | 
**aptLogId** | **String** | The Id of the Log that precedes this Contact | 
**aptLog** | [**AptLog**](AptLog.md) |  | [optional] 
**dateTime** | [**DateTime**](DateTime.md) | The Date to Contact the Customer  The Time is used because, chances are, there is a better Time of the day to contact the Customer | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


