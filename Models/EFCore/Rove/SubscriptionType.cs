using System.ComponentModel.DataAnnotations;

namespace GestorViajes.Models.EFCore.Rove
{
    public enum SubscriptionType
    {
        [Display(Name = "Básico")]
        Basic,

        [Display(Name = "Premium")]
        Premium
    }

}
