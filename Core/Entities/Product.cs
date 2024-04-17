﻿using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
   
}