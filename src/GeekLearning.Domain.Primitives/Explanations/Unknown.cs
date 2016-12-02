
namespace GeekLearning.Domain.Explanations
{
    using System;

    public class Unknown : Explanation
    {
        public Unknown()
            : base("An unknown error has happened")
        {
        }

        public Unknown(Exception exception)
          : base("An unknown error has happened", exception.ToString())
        {
        }
    }
}
