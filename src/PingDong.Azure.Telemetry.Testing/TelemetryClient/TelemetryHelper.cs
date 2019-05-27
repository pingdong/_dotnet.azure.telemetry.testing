using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Azure.Telemetry.Testing
{
    public sealed class TelemetryClientHelper
    {
        public static TelemetryClient CreateStub()
        {
            var configuration = new TelemetryConfiguration();
            var sendItems = new List<ITelemetry>();
            configuration.TelemetryChannel = new TelemetryChannelStub { OnSend = item => sendItems.Add(item) };
            configuration.InstrumentationKey = Guid.NewGuid().ToString();
            configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            return new TelemetryClient(configuration);
        }
    }
}
