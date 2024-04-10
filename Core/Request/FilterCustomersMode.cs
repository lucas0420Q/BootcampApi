﻿namespace Core.Request;

public class FilterCustomerModel
{
    public int? BirthYearFrom { get; set; }
    public int? BirthYearTo { get; set; }
    public string? FullName { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Mail { get; set; }
    public int? BankId { get; set; }
}

