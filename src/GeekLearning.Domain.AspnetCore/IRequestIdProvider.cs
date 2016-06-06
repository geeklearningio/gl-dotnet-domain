using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    public interface IRequestIdProvider
    {
        string RequestId { get; }
    }
}
