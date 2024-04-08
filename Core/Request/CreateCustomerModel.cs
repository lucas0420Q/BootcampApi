﻿namespace Core.Request;

public class CreateCustomerModel
{

    public string Name { get; set; } = string.Empty;

    public string? Lastname { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string? Address { get; set; }

    public string? Mail { get; set; }

    public string? Phone { get; set; }

    public int BankId { get; set; }

    public DateTime? Birth { get; set; }

    public string? CustomerStatus { get; set; } = string.Empty;
}
