﻿namespace HyperTamagotchi_API.Models;

public class DiscountUpdateModel
{
    public List<int> SelectedShoppingItems { get; set; }
    public float? DiscountValue { get; set; }
}
