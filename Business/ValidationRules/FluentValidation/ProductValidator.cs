using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // p=Product
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            // GreaterThan
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            // İçeçek kategorisindeki ürünlerin minumum fiyatı 10 lira olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryID == 1);
            /**
             * Olmayan bir şeyleri ekleyelim
             * **** Ürünlerimizin ismi A ile başlamalı *****
             */
            //RuleFor(p => p.ProductName).Must(StartwithA).WithMessage("Ürünler A harfi ile başlamalı!");
        }

        // True dönerse kurala uygun, false dönerse patlar!!
        private bool StartwithA(string arg)
        {   // A ile başlıyorsa true döner
            return arg.StartsWith("A");
        }
    }
}
