using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        /**
         * Ilgili kullanici icin veri tabanina gidecek, o kullanicinin ilgili claim'lerini bulacak
         * Orada(API'de) bir JWT uretecek ve client'a bunu donecek.
         */
        AccessToken CreateToken(User user, IDataResult<List<OperationClaim>> operation);
    }
}
