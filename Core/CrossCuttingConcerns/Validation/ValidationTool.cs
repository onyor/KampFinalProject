using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        // Tek bir instance oluşturulur ve çalışma süresi boyunca uygulama belleği o örneği kullanır.
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            //// Product için doğrulama yapacağız. 
            //var context = new ValidationContext<Product>(product);
            //// productValidator bu bize lazım 
            //ProductValidator productValidator = new ProductValidator();
            //// --> o class' da ki kurallar için
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
        }
    }
}
