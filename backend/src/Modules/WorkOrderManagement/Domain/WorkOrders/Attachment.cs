using FieldOps.BuildingBlocks.Guards;
using FieldOps.BuildingBlocks.Persistence.Entities;

namespace FieldOps.Modules.WorkOrderManagement.Domain.WorkOrders;

public sealed class Attachment : EntityBase<Guid>
{
    public string FileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeBytes { get; private set; }
    public string StorageKey { get; private set; } = string.Empty;
    public Guid UploadedByUserId { get; private set; }
    public DateTimeOffset UploadedAt { get; private set; }

    private Attachment()
    {
    }

    private Attachment(
        Guid id,
        string fileName,
        string contentType,
        long sizeBytes,
        string storageKey,
        Guid uploadedByUserId,
        DateTimeOffset uploadedAt)
        : base(id)
    {
        Guard.ThrowIfNullOrWhiteSpace(fileName);
        Guard.ThrowIfNullOrWhiteSpace(contentType);
        Guard.ThrowIfNotPositive(sizeBytes);
        Guard.ThrowIfNullOrWhiteSpace(storageKey);
        Guard.ThrowIfEmpty(uploadedByUserId);

        FileName = fileName.Trim();
        ContentType = contentType.Trim();
        SizeBytes = sizeBytes;
        StorageKey = storageKey.Trim();
        UploadedByUserId = uploadedByUserId;
        UploadedAt = uploadedAt;
    }

    public static Attachment Create(
        Guid id,
        string fileName,
        string contentType,
        long sizeBytes,
        string storageKey,
        Guid uploadedByUserId,
        DateTimeOffset uploadedAt) =>
        new(id, fileName, contentType, sizeBytes, storageKey, uploadedByUserId, uploadedAt);
}

