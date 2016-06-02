using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    public class DefaultRequestIdProvider : IRequestIdProvider
    {
        public DefaultRequestIdProvider()
        {
            var idGen = new D64.TimebasedId(false);
            this.RequestId = idGen.NewId();
        }

        public string RequestId
        {
            get;
        }
    }
}
