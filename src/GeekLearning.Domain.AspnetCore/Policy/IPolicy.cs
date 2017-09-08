using GeekLearning.Domain.Explanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore.Policy
{
    public interface IPolicy
    {
        HttpStatusCode GetStatusCode(Explanation explanation);
    }
}
