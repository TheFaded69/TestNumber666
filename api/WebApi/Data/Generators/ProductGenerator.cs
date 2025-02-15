using Bogus;
using WebApi.Data.Entities;

namespace WebApi.Data.Generators;

public class ProductGenerator : Faker<Product>
{
    public ProductGenerator(string locale) : base(locale)
    {
        RuleFor(x => x.CreatedAt, f => f.Date.Past(2, DateTime.UtcNow.AddYears(-1)));
        RuleFor(x => x.UpdatedAt, (f, x) => f.Date.Between(x.CreatedAt, DateTime.UtcNow.AddMonths(-6)));
        RuleFor(x => x.DeletedAt, (f, x) => null/* f.Random.Number(10) <= 2 ? f.Date.Between(x.UpdatedAt, DateTime.UtcNow) : null*/);

        RuleFor(x => x.Title, f => f.Commerce.ProductName());
        RuleFor(x => x.Description, f => f.Commerce.ProductDescription());
        RuleFor(x => x.IsSold, f => f.Random.Bool());
    }
}
