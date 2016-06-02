using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GeekLearning.Domain.AspnetCore
{
    public static class GeekLearningDomainAspnetCoreControllerExtensions
    {
        public static MaybeResult<T> Maybe<T>(this ControllerBase controller, Maybe<T> result) where T : class
        {
            return new MaybeResult<T>(result);
        }
    }
}
