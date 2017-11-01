using GeekLearning.Domain.Explanations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace GeekLearning.Domain.Tests
{
    public class PolicyTests
    {
        [Theory]
        [ClassData(typeof(StandardPolicyData))]
        public void StandardPolicyHasExpectedBehavior(Explanation explanation, HttpStatusCode expectedCode)
        {
            var builder = new GeekLearning.Domain.AspnetCore.Internal.PolicyBuilder();
            builder.ApplyDefaultPolicy();

            var policy = builder.Build();

            Assert.Equal(expectedCode, policy.GetStatusCode(explanation));

        }

        private class StandardPolicyData : IEnumerable<object[]>
        {

            public IEnumerator<object[]> GetEnumeratorCore()
            {
                yield return new object[] { null, HttpStatusCode.OK };
                yield return new object[] { new NotFound("not found"), HttpStatusCode.NotFound };
                yield return new object[] { new NotFound<object>("not found"), HttpStatusCode.NotFound };
                yield return new object[] { new UnsufficientPrivileges("message"), HttpStatusCode.Forbidden };
                yield return new object[] { new Anonymous("message"), HttpStatusCode.Unauthorized };
                yield return new object[] { new Forbidden(), HttpStatusCode.Forbidden };
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return GetEnumeratorCore();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumeratorCore();
            }
        }
    }
}
