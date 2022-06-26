﻿using Flurl;
using Flurl.Http;
using FunctionMonkey.Tests.Integration.Common;
using FunctionMonkey.Tests.Integration.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FunctionMonkey.Tests.Integration.EventHub
{
    // Note that the output binding service bus functions write to a queue that is listened to by another
    // function in the integration tests that listens to this queue. This then sets the marker in the table.
    public class OutputBindingShould : AbstractHttpFunctionTest
    {
        [Fact]
        public async Task WriteToEventHub()
        {
            MarkerMessage marker = new MarkerMessage
            {
                MarkerId = Guid.NewGuid()
            };
            var response = await Settings.Host
                .AppendPathSegment("outputBindings")
                .AppendPathSegment("toEventHub")
                .SetQueryParam("markerId", marker.MarkerId)
                .GetAsync();

            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
            await marker.Assert();
        }
    }
}
