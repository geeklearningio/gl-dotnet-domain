using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekLearning.Domain.Explanations;

namespace GeekLearning.Domain.AspnetCore.Internal
{
    public class MaybeResultMapper
    {
        public int GetResult(IEnumerable<Explanation> explanations)
        {
            if (explanations.Any())
            {
                if (explanations.Any(x=> x is NotFoundExplanation))
                {
                    return 404;
                }
                return 500;
            }
            else
            {
                return 200;
            }
        }
    }
}
