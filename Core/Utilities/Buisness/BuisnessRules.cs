using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Buisness
{
    public class BuisnessRules
    {
        // params keyword'ü ile istediğimiz kadar iş kuralını Run methoduna yollayabileceğiz.
        // List<IResult>
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                // List<IResult> errorResults = new List<IResult>();
                // logic'in Success durumu başarısız ise bunu döndürebiliriz.
                if (!logic.Success)
                {
                    // ErrorResult & ErrorDataResult
                    return logic;

                    // errorResults.Add(logic);
                }
            }

            return null;
        }
    }
}
