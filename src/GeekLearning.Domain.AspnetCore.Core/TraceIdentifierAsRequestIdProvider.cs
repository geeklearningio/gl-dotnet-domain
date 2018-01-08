using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Domain.AspnetCore
{
    public class ActivityIdAsRequestIdProvider: IRequestIdProvider
    {
        public string RequestId => System.Diagnostics.Activity.Current.Id;
    }
}
