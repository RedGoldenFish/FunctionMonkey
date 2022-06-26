﻿using Flurl;
using Flurl.Http;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.Storage
{
    public class QueueOutputBindingShould : AbstractHttpFunctionTest
    {
        [Fact]
        public async Task WriteToStorageQueueWhenResponseIsSingular()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };
            var response = await Settings.Host
                .AppendPathSegment("outputBindings")
                .AppendPathSegment("toStorageQueue")
                .SetQueryParam("markerId", marker.MarkerId)
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
            await marker.Assert();
        }

        [Fact]
        public async Task WriteToStorageQueueWhenResponseIsCollection()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };
            var response = await Settings.Host
                .AppendPathSegment("outputBindings")
                .AppendPathSegment("collectionToStorageQueue")
                .SetQueryParam("markerId", marker.MarkerId)
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
            await marker.Assert();
        }
    }
}
