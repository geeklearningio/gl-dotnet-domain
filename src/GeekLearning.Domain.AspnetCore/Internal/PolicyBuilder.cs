using GeekLearning.Domain.AspnetCore.Policy;
using GeekLearning.Domain.Explanations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace GeekLearning.Domain.AspnetCore.Internal
{
    public class PolicyBuilder : IPolicyBuilder
    {
        List<Func<Explanation, HttpStatusCode?>> mappings = new List<Func<Explanation, HttpStatusCode?>>();

        public IPolicy Build()
        {
            return new Policy(mappings);
        }

        public IPolicyBuilder MapNull(HttpStatusCode status)
        {
            mappings.Add(x => x == null ? (HttpStatusCode?)status : null);

            return this;
        }

        public IPolicyBuilder Map<TExplanation>(HttpStatusCode status) where TExplanation : Explanation
        {
            mappings.Add(x => typeof(TExplanation).IsAssignableFrom(x.GetType()) ? (HttpStatusCode?)status : null);

            return this;
        }

        public IPolicyBuilder Map(HttpStatusCode status, Func<Explanation, bool> predicate)
        {
            mappings.Add(x => predicate(x) ? (HttpStatusCode?)status : null);

            return this;
        }


        public IPolicyBuilder Map<TExplanation>(HttpStatusCode status, Func<TExplanation, bool> predicate) where TExplanation : Explanation
        {
            mappings.Add(x => x.GetType().IsAssignableFrom(typeof(TExplanation)) && predicate((TExplanation)x) ? (HttpStatusCode?)status : null);

            return this;
        }


        public static void ApplyDefaultPolicy(IPolicyBuilder policyBuilder)
        {
            policyBuilder.MapNull(HttpStatusCode.OK);

            policyBuilder.Map<Created>(HttpStatusCode.Created);
            policyBuilder.Map<Updated>(HttpStatusCode.OK);
            policyBuilder.Map<Deleted>(HttpStatusCode.NoContent);
            policyBuilder.Map<Unremovable>(HttpStatusCode.BadRequest);
            policyBuilder.Map<NotFound>(HttpStatusCode.NotFound);
            policyBuilder.Map(HttpStatusCode.BadRequest, explanation => explanation.GetType().IsSubclassOfRawGeneric(typeof(Validation<>)));
            policyBuilder.Map<Duplicated>(HttpStatusCode.Conflict);
            policyBuilder.Map<Anonymous>(HttpStatusCode.Unauthorized);
            policyBuilder.Map<UnsufficientPrivileges>(HttpStatusCode.Forbidden);
        }

        private class Policy : IPolicy
        {
            private readonly IReadOnlyList<Func<Explanation, HttpStatusCode?>> mappings;

            public Policy(IReadOnlyList<Func<Explanation, HttpStatusCode?>> mappings)
            {
                this.mappings = mappings;
            }

            public HttpStatusCode GetStatusCode(Explanation explanation)
            {
                foreach (var mapping in mappings)
                {
                    var result = mapping(explanation);
                    if (result.HasValue)
                    {
                        return result.Value;
                    }
                }

                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
