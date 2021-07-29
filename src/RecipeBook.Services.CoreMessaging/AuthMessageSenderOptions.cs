using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public class AuthMessageSenderOptions
    {
        public string Name { get; set; }
        public string APIKey { get; set; }
        public string APISecret { get; set; }
        public string Email { get; set; }
    }
}
