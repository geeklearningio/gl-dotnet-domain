﻿namespace GeekLearning.Domain.Explanations
{
    public class Optional<T> : Explanation 
    {
        public Optional(string message = "Item is optional and wasn't provided"): base(message)
        {
        }
    }
}