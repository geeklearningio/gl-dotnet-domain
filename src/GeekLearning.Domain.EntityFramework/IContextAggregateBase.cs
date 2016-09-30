using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.EntityFramework
{
    public interface IContextAggregateBase
    {
        States State();
    }
}
