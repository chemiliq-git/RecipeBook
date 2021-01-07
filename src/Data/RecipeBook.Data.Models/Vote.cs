namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using RecipeBook.Data.Common.Models;

    public class Vote : BaseModel<string>
    {
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteTypeEnm Type { get; set; }

        public int Value { get; set; }
    }
}
