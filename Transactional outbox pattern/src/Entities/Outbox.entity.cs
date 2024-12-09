namespace Entities;

using System;

public class Outbox
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public bool Processed { get; set; }
    public DateTime CreatedAt { get; set; }
}
