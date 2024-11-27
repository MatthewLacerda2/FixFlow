class AptFilters {
  AptFilters({
    required this.businessId,
    this.clientId,
    this.service,
    this.minPrice = 0,
    required this.maxPrice,
    required this.minDateTime,
    required this.maxDateTime,
    this.offset = 0,
    this.limit = 100,
  });

  String businessId;
  String? clientId;
  String? service;
  double minPrice;
  double maxPrice;
  DateTime minDateTime;
  DateTime maxDateTime;
  int offset;
  int limit;

  @override
  String toString() {
    return 'AptFilters(businessId: $businessId, clientName: $clientId, service: $service, minPrice: $minPrice, maxPrice: $maxPrice, minDateTime: $minDateTime, maxDateTime: $maxDateTime, offset: $offset, limit: $limit)';
  }
}
