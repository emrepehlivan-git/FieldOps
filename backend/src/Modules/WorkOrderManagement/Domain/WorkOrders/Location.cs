namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed record Location(
    string AddressLine1,
    string? AddressLine2,
    string City,
    string? StateOrProvince,
    string PostalCode,
    string CountryCode,
    decimal? Latitude,
    decimal? Longitude);

