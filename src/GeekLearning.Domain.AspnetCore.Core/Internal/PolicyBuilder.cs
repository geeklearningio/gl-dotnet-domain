namespace GeekLearning.Domain.AspnetCore.Internal
{
    using GeekLearning.Domain.AspnetCore.Policy;
    using GeekLearning.Domain.Explanations;
    using System;
    using System.Collections.Generic;
    using System.Net;

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

        public IPolicyBuilder ApplyDefaultPolicy()
        {
            this.MapNull(HttpStatusCode.OK);
            this.Map<Created>(HttpStatusCode.Created);
            this.Map<Updated>(HttpStatusCode.OK);
            this.Map<Deleted>(HttpStatusCode.NoContent);
            this.Map<Unremovable>(HttpStatusCode.BadRequest);
            this.Map<NotFound>(HttpStatusCode.NotFound);
            this.Map<Invalid>(HttpStatusCode.BadRequest);
            this.Map<Duplicated>(HttpStatusCode.Conflict);
            this.Map<Anonymous>(HttpStatusCode.Unauthorized);
            this.Map<Forbidden>(HttpStatusCode.Forbidden);
            this.Map<UnsufficientPrivileges>(HttpStatusCode.Forbidden);

            return this;
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
