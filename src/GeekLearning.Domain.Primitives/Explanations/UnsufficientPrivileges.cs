namespace GeekLearning.Domain.Explanations
{
    public class UnsufficientPrivileges : Explanation
    {
        public UnsufficientPrivileges() 
            : base("The current user needs more privileges.")
        {
        }

        public UnsufficientPrivileges(string message)
            : base(message)
        {
        }
    }
}
