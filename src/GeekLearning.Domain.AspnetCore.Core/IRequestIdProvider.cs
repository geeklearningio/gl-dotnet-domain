using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    interface IRequestIdProvider
    {
        string RequestId { get; }
    }
}
