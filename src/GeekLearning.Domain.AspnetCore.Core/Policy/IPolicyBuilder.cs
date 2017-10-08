using GeekLearning.Domain.Explanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore.Policy
{
    public interface IPolicyBuilder
    {
        IPolicyBuilder ApplyDefaultPolicy();

        IPolicyBuilder MapNull(HttpStatusCode status);

        IPolicyBuilder Map(HttpStatusCode status, Func<Explanation, bool> predicate);

        IPolicyBuilder Map<TExplanation>(HttpStatusCode status) where TExplanation : Explanation;

        IPolicyBuilder Map<TExplanation>(HttpStatusCode status, Func<TExplanation, bool> predicate) where TExplanation : Explanation;
    }
}
