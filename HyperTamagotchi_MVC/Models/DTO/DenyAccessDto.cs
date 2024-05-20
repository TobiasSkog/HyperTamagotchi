namespace HyperTamagotchi_MVC.Models.DTO;

public class DenyAccessDto(bool isUserAllowed = false)
{
    public bool IsUserAllowed { get; set; } = isUserAllowed;
    public string RedirectUrl { get; set; } = isUserAllowed ? "" : "/Account/AccessDenied";
    public string RedirectAction { get; set; } = isUserAllowed ? "" : "Account";
}
