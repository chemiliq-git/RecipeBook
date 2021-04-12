using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Services.Data.Tests
{
    public class MockHttpContext : Mock<IHttpContextAccessor>
    {
        private const string FIRST_USER_AUTHENTICATION_TYPE = "Mock";
        private const string CUSTOM_CLAIM = "custom-claim";
        private const string CLAIM_VALUE = "claim value";

        private DefaultHttpContext context = new DefaultHttpContext();

        public void SetContextUser(string userName, string userUID, ref DefaultHttpContext context)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userUID),
                new Claim(CUSTOM_CLAIM, CLAIM_VALUE),
            }, FIRST_USER_AUTHENTICATION_TYPE));

            context.User = user;
        }

        public void SetupMockContext(ref DefaultHttpContext context)
        {
            Setup(x => x.HttpContext).Returns(context);
        }
    }
}
