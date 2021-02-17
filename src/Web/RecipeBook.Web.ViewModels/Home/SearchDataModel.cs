namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

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
