namespace RecipeBook.Web.ViewModels.Home
{
    using RecipeBook.Data.Models;

    public class VoteInputModel
    {
        public string RecipeId { get; set; }

        public VoteTypeEnm Type { get; set; }

        public int Value { get; set; }
    }
}
