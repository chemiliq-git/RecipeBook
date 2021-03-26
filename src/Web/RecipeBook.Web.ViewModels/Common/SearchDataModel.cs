namespace RecipeBook.Web.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class SearchDataModel
    {
        [StringLength(100, ErrorMessage = "Text length can't be more than 100.")]
        [RegularExpression(@"^.*[^\d\W]", ErrorMessage = "Text can't contain digits.")]
        public string Text { get; set; }

        public string RecipeTypes { get; set; }

        public string Ingredients { get; set; }

        [Required]
        public SearchDataModeEnum Mode { get; set; }
    }
}
