using HyperTamagotchi_MVC.Models.TamagotchiProperties;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HyperTamagotchi_MVC.Models;

public class Tamagotchi
{
    public int TamagotchiId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 30 characters")]
    public string Name { get; set; }

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
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Image path must be between 3 and 50 characters")]
    public string ImagePath { get; set; }


    [Required]
    [DisplayName("Age")]
    public TamagotchiStage TamagotchiStage { get; set; } = TamagotchiStage.Egg;

    [Required]
    [Range(0, 256, ErrorMessage = "Experience must be between 0 and 256")]
    public byte Experience { get; set; } = 0;
}