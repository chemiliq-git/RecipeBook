namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RecipeBook.Data.Common.Models;

    public class CookingHistory : BaseModel<string>
    {
        public CookingHistory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int RecipeTasteRate { get; set; }

        public int RecipeEasyRate { get; set; }

        public DateTime LastCooked { get; set; }
    }
}
