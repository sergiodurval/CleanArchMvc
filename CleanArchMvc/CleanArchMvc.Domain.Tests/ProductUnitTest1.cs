using Bogus;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        Faker fake;
        Product validProductObject;

        public ProductUnitTest1()
        {
            fake = new Faker();
            validProductObject = new Product
            (
                fake.Random.Number(0,10),
                fake.Commerce.ProductName(),
                fake.Commerce.ProductDescription(),
                Convert.ToDecimal(fake.Commerce.Price(1,999,2)),
                fake.Random.Number(1,99),
                fake.Random.String(100)
            );
        }

        [Fact(DisplayName = "Must create a product with valid parameters")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, validProductObject.Image);
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Must not create a product with invalid id")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(fake.Random.Number(-1, -1), validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, validProductObject.Image);
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");    
        }

        [Fact(DisplayName = "Must not create a product with short name")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(validProductObject.Id,fake.Random.String(2), validProductObject.Description,validProductObject.Price, validProductObject.Stock, validProductObject.Image);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name , too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Must not create a product with long image name")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, fake.Random.String(300));
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name , too long , maximum 250 characters");
        }

        [Fact(DisplayName = "Must not create a product with null image name")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, null);
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Must not create a product with null image name and throw null exception")]
        public void CreateProduct_WithNullImageName_NullException()
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, null);
            action.Should().NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Must not create a product with empty image name")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, validProductObject.Stock, "");
            action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Theory(DisplayName = "Must not create a product with invalid stock value")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, validProductObject.Price, value, validProductObject.Image);
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }

        [Theory(DisplayName = "Must not create a product with invalid price value")]
        [InlineData(-99.9)]
        public void CreateProduct_InvalidProcuctValue_ExceptionDomainNegativeValue(decimal price)
        {
            Action action = () => new Product(validProductObject.Id, validProductObject.Name, validProductObject.Description, price,validProductObject.Stock,validProductObject.Image);
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }
    }
}
