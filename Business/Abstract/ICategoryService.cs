using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    // Category ile ilgili dış dünyaya ne service etmek istiyorsak, buraya yazıyoruz.
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int categoryId);

    }
}
