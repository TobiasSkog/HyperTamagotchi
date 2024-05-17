namespace HyperTamagotchi_SharedModels.Helpers;

public static class DiscountConversionHelper
{
    public static float ConvertFromUserInputToShoppingItem(float userInput)
    {
        if (userInput == 0)
        {
            return 1.0f;
        }

        float discountValue = 1 - userInput / 100;
        return (float)Math.Round(discountValue, 2);
    }

    public static float ConvertFromShoppingItemToDiscountDisplayDiscount(float shopingItemDiscount)
    {
        if (shopingItemDiscount == 1)
        {
            return 0f;
        }

        float discountPercentage = (1 - shopingItemDiscount) * 100;
        return (float)Math.Round(discountPercentage, 2);
    }
}
