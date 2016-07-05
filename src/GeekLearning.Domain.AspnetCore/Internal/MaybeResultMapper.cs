namespace GeekLearning.Domain.AspnetCore.Internal
{
    using Explanations;
    using System.Net;

    public class MaybeResultMapper
    {
        public int GetResult(Explanation explanation)
        {
            return (int)GetHttpStatusCodeResult(explanation);
        }

        private static HttpStatusCode GetHttpStatusCodeResult(Explanation explanation)
        {
            if (explanation == null)
            {
                return HttpStatusCode.OK;
            }

            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(Updated<>)))
            {
                return HttpStatusCode.OK;
            }

            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(Created<>)))
            {
                return HttpStatusCode.Created;
            }

            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(Deleted<>)))
            {
                return HttpStatusCode.NoContent;
            }

            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(NotFound<>)))
            {
                return HttpStatusCode.NotFound;
            }

            if (explanation.GetType().IsSubclassOfRawGeneric(typeof(Validation<>)))
            {
                return HttpStatusCode.BadRequest;
            }

            if (explanation is Duplicated)
            {
                return HttpStatusCode.Conflict;
            }

            if (explanation is ShouldProvideIdentity)
            {
                return HttpStatusCode.Unauthorized;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
