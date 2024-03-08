using Entitities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation;

public class ProductValidator:AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty();
        RuleFor(p => p.ProductName).Length(5,15);                                                   //productname 5'den küçük 15'den büyük olamaz.
        RuleFor(p => p.UnitPrice).NotEmpty();
        RuleFor(p => p.UnitPrice).GreaterThan(0);                                                   //unitprice sıfırdan büyük olmalı
        RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryID == 1);          //içecek kategorisi fiyatı max 10 olmalı
        
        
        RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı.");              //Ürün ismi A ile başlamalı diye kendim bir metot oluşturuyorum(Özel validasyon yazma işlemi)
    }

    private bool StartWithA(string arg)                             //Ürün ismi A ile başlamalı diye kendim bir metot oluşturuyorum(Özel validasyon yazma işlemi)
    {
        return arg.StartsWith("A");
    }
}
