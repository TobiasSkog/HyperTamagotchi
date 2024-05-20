﻿using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_MVC.Models.DTO;

public class LoginRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}