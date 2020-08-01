using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GeekLearning.Domain;
using GeekLearning.Domain.AspnetCore;
using GeekLearning.Domain.Explanations;
using Microsoft.Extensions.DependencyModel;
using Xunit;

namespace GeekLearning.Domain.Tests
{
    public class MaybeExtensionsTests
    {

        private class DataClass
        {
            public Guid Id { get; set; } = Guid.NewGuid();
        }

        private class DataTransformedClass
        {
            public String Id { get; set; }
        }

        [Fact]
        public async Task MapAsync_Value()
        {
            var source = new DataClass();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => new DataTransformedClass { Id = x.Id.ToString() });

            Assert.True(examinee.HasValue);
            Assert.Equal(source.Id, Guid.Parse(examinee.Value.Id));
        }

        [Fact]
        public async Task MapAsync_NoValue()
        {
            var source = new NotFound();
            var task = Task.FromResult((Maybe<DataClass>)source);

            var examinee = await task.MapAsync(x => new DataTransformedClass { Id = x.Id.ToString() });

            Assert.False(examinee.HasValue);
            Assert.Equal(source, examinee.Explanation);
        }

        [Fact]
        public async Task MapAsync_MaybeValueWithoutTransformationValue()
        {
            var source = new DataClass();
            var tranformationResult = new NotFound();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => (Maybe<DataTransformedClass>)tranformationResult);

            Assert.False(examinee.HasValue);
            Assert.Equal(tranformationResult, examinee.Explanation);

        }

        [Fact]
        public async Task MapAsync_MaybeValue()
        {
            var source = new DataClass();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => Maybe.Some(new DataTransformedClass { Id = x.Id.ToString() }));

            Assert.True(examinee.HasValue);
            Assert.Equal(source.Id, Guid.Parse(examinee.Value.Id));
        }

        [Fact]
        public async Task MapAsync_MaybeNoValue()
        {
            var source = new NotFound();
            var task = Task.FromResult((Maybe<DataClass>)source);

            var examinee = await task.MapAsync(x => Maybe.Some(new DataTransformedClass { Id = x.Id.ToString() }));

            Assert.False(examinee.HasValue);
            Assert.Equal(source, examinee.Explanation);
        }

        [Fact]
        public async Task MapAsync_AsyncTranformationValue()
        {
            var source = new DataClass();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => Task.FromResult(new DataTransformedClass { Id = x.Id.ToString() }));

            Assert.True(examinee.HasValue);
            Assert.Equal(source.Id, Guid.Parse(examinee.Value.Id));
        }

        [Fact]
        public async Task MapAsync_AsyncTransformationNoValue()
        {
            var source = new NotFound();
            var task = Task.FromResult((Maybe<DataClass>)source);

            var examinee = await task.MapAsync(x => Task.FromResult(new DataTransformedClass { Id = x.Id.ToString() }));

            Assert.False(examinee.HasValue);
            Assert.Equal(source, examinee.Explanation);
        }

        [Fact]
        public async Task MapAsync_MaybeAsyncTranformationValue()
        {
            var source = new DataClass();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => Task.FromResult(Maybe.Some(new DataTransformedClass { Id = x.Id.ToString() })));

            Assert.True(examinee.HasValue);
            Assert.Equal(source.Id, Guid.Parse(examinee.Value.Id));
        }

        [Fact]
        public async Task MapAsync_MaybeAsyncTransformationNoValue()
        {
            var source = new NotFound();
            var task = Task.FromResult((Maybe<DataClass>)source);

            var examinee = await task.MapAsync(x => Task.FromResult(Maybe.Some(new DataTransformedClass { Id = x.Id.ToString() })));

            Assert.False(examinee.HasValue);
            Assert.Equal(source, examinee.Explanation);
        }


        [Fact]
        public async Task MapAsync_MapAsync_MaybeAsyncTranformationValueWithoutTransformationValue()
        {
            var source = new DataClass();
            var tranformationResult = new NotFound();
            var task = Task.FromResult(Maybe.Some(source));

            var examinee = await task.MapAsync(x => Task.FromResult((Maybe<DataTransformedClass>)tranformationResult));

            Assert.False(examinee.HasValue);
            Assert.Equal(tranformationResult, examinee.Explanation);

        }

    }
}
