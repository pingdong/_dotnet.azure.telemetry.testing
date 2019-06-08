using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Azure.Telemetry.Testing
{
    /// <summary>
    /// Create a TelemetryClient for unit testing
    /// </summary>
    public class TelemetryClientProxy
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public TelemetryClientProxy()
        {
            Repository = new List<ITelemetry>();

            var channel = new TelemetryChannelStub
            {
                DeveloperMode = true,
                OnSend = item => Repository.Add(item)
            };
            
            var configuration = new TelemetryConfiguration
            {
                TelemetryChannel = channel,
                InstrumentationKey = Guid.NewGuid().ToString()
            };
            configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            Client = new TelemetryClient(configuration);
        }

        /// <summary>
        /// TelemetryClient
        /// </summary>
        public TelemetryClient Client { get; }
        
        /// <summary>
        /// Access all added telemetry samples
        /// </summary>
        public List<ITelemetry> Repository { get; }
    }
}
