# openapi.model.CustomerRecord

## Load the model package
```dart
import 'package:openapi/api.dart';
```

## Properties
Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**fullName** | **String** |  | 
**phoneNumber** | **String** | Phone Number. Must contain only numbers | 
**email** | **String** |  | [optional] 
**cpf** | **String** |  | [optional] 
**additionalNote** | **String** | Special information about the Customer, if applicable | [optional] 
**firstLog** | [**DateTime**](DateTime.md) |  | [optional] 
**lastLog** | [**DateTime**](DateTime.md) |  | [optional] 
**logs** | [**List<AptLog>**](AptLog.md) |  | [optional] [default to const []]
**numSchedules** | **int** |  | [optional] 
**avgTimeBetweenSchedules** | **int** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


