# openapi.model.Subscription

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **String** |  | [optional] 
**businessId** | **String** |  | [optional] 
**businessName** | **String** | The name of the Business or Business' owner. | [optional] 
**businessCNPJ** | **String** |  | [optional] 
**dateTime** | [**DateTime**](DateTime.md) | The date from when the service started being used | [optional] 
**price** | **int** |  | [optional] 
**payed** | **bool** |  | [optional] 
**additionalNote** | **String** | Special information about that month's payment | [optional] 
**timeSpentDeactivated** | **String** | When we deactivate the account, we store the time the user spent, so if he comes back he'll pick up the time left | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


