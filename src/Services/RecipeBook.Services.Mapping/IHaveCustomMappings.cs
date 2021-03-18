namespace RecipeBook.Services.Mapping
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration, IHttpContextAccessor httpContextAccessor);
    }
}
