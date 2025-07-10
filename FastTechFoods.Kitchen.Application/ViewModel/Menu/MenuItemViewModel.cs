﻿using FastTechFoods.Kitchen.Application.ViewModel.Base;

namespace FastTechFoods.Kitchen.Application.ViewModel.Menu;
public class MenuItemViewModel : ViewModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
}
