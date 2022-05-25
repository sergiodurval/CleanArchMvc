using Bogus;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {
        Faker fake;
        Category validCategoryObject;

        public CategoryUnitTest1()
        {
            fake = new Faker();
            validCategoryObject = new Category(fake.Random.Number(),fake.Commerce.Product());
        }

        [Fact(DisplayName = "Must create a category with valid parameters")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(validCategoryObject.Id,validCategoryObject.Name);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Must not create a category with invalid id")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(fake.Random.Number(-1,-1), fake.Commerce.Product());
            action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Must not create a category with null name")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(fake.Random.Number(0, 10), null);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name.Name is required");
        }

        [Fact(DisplayName = "Must not create a category with short name")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(fake.Random.Number(0, 10), fake.Random.String(2));
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name , too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Must not create a category with missing name")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(fake.Random.Number(0, 10), "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name.Name is required");
        }
    }
}
