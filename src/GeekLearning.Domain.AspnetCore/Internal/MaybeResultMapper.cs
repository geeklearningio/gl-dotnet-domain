namespace GeekLearning.Domain.AspnetCore.Internal
{
    using Explanations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class MaybeResultMapper
    {
        public int GetResult(IEnumerable<Explanation> explanations)
        {
            if (!explanations.Any())
            {
                return (int)HttpStatusCode.OK;
            }

            if (explanations.All(x => x is UpdatedExplanation))
            {
                return (int)HttpStatusCode.OK;
            }

            if (explanations.All(x => x is CreatedExplanation))
            {
                return (int)HttpStatusCode.Created;
            }

            if (explanations.All(x => x is DeletedExplanation))
            {
                return (int)HttpStatusCode.NoContent;
            }

            if (explanations.Any(x => x is NotFoundExplanation))
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
