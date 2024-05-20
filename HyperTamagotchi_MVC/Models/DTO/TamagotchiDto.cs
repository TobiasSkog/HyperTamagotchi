using HyperTamagotchi_MVC.Models.TamagotchiProperties;

namespace HyperTamagotchi_API.Models.DTO;

public class TamagotchiDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte Stock { get; set; }
    public float Price { get; set; }
    public string ImagePath { get; set; }
    public TamagotchiColor TamagotchiColor { get; set; }
    public TamagotchiType TamagotchiType { get; set; }
    public TamagotchiMood Mood { get; set; }
    public TamagotchiStage TamagotchiStage { get; set; }
}
