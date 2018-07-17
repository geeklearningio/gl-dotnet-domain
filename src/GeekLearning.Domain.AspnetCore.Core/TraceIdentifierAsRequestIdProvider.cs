namespace GeekLearning.Domain.AspnetCore
{
    public class ActivityIdAsRequestIdProvider : IRequestIdProvider
    {
        public string RequestId => System.Diagnostics.Activity.Current.Id;
    }
}
