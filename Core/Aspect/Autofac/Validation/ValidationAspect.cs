﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        // Bize validatorType ver -> Attributeler bu yöntem ile ekleniyor.
        public ValidationAspect(Type validatorType)
        {
            // Benim Validator'ım IValidator olması lazım. ProductValidator-> AbstractValidator-> IValidator
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            // Reflection -> Çalışma anında bir şeyleri çalıştırabilmemizi sağlıyor. / CreateInstance instance'ını oluşturuyor.
            // ProductValidator'ın instance'ını oluşturuyor.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            // Product validator'ın base Type' ını bul. Onun generic args'larından ilkini bul
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // Ve bu invocation(methodunun) parametrelerine bak. Validation type eşit olan parametreleri bul örn: Product
            // Validator'ın type'ını eşit olan type'ın parametrelerini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // Parametreleri gez ve Validation Tool'u kullanarak validate et!
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
