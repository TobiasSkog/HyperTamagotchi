using HyperTamagotchi_SharedModels.Models.TamagotchiProperties;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_SharedModels.Models;

public class Tamagotchi : ShoppingItem
{
    [Required]
    [DisplayName("Color")]
    public TamagotchiColor TamagotchiColor { get; set; }

    [Required]
    [DisplayName("Personality")]
    public TamagotchiType TamagotchiType { get; set; }

    [Required]
    [DisplayName("Mood")]
    public TamagotchiMood Mood { get; set; } = TamagotchiMood.Hungry;

    [Required]
    [DisplayName("Age")]
    public TamagotchiStage TamagotchiStage { get; set; } = TamagotchiStage.Egg;

    [Required]
    [Range(0, 256, ErrorMessage = "Experience must be between 0 and 256")]
    public byte Experience { get; set; } = 0;
}