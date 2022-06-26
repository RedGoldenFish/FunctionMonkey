using Azure.Data.Tables;
using System;
using System.Threading.Tasks;

namespace FunctionMonkey.Tests.Integration.Common
{
    public class MarkerMessage
    {
        public Guid MarkerId { get; set; }

        public int? Value { get; set; }

        public async Task Assert()
        {
            const int delayIncrement = 750;
            const int maximumDelay = delayIncrement * 120;
            TableClient markerTable = new TableClient(AbstractIntegrationTest.Settings.StorageConnectionString, "markers");

            int totalDelay = 0;
            MarkerTableEntity marker = null;
            do
            {
                await Task.Delay(delayIncrement);
                totalDelay += delayIncrement;
                marker = await markerTable.GetEntityAsync<MarkerTableEntity>(MarkerId.ToString(), string.Empty);
            } while (totalDelay < maximumDelay && marker == null);

            Xunit.Assert.NotNull(marker);
            Xunit.Assert.Equal(Value, marker.Value);
        }
    }
}
