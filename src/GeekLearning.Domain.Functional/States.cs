using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.Functional
{
    public enum States
    {
        Unknown = 0,
        Unchanged = 1,
        ToBeDeleted = 2,
        ToBeModified = 3,
        ToBeCreated = 4
    }

}
