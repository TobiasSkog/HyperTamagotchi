namespace HyperTamagotchi_MVC.Services;

//[AuthorizeByRole("Admin")]
public partial class ApiServices
{
    public bool Edit()
    {
        if (!IsUserInRole("Admin"))
        {
            return false;
        }



        return true;

    }
}