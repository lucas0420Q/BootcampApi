﻿namespace Core.Models;

public class EnterpriseDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; } = null;
    public ICollection<PromotionDTO> Promotions { get; set; } = new List<PromotionDTO>();
}
