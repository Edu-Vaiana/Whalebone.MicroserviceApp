﻿using System.ComponentModel.DataAnnotations;

namespace Whalebone.MicroserviceApp.Models;

public class Person
{
    [Key]
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
