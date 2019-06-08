using System;
using Microsoft.ApplicationInsights.Channel;

namespace PingDong.Azure.Telemetry.Testing
{
    /// <summary>
    /// A stub of <see cref="ITelemetryChannel"/>.
    /// </summary>
    internal class TelemetryChannelStub : ITelemetryChannel
    {
        /// <summary>
        /// Initializes a new instance of the TelemetryChannelStub class.
        /// </summary>
        public TelemetryChannelStub()
        {
            OnSend = telemetry => { };
            OnFlush = () => { };
            OnDispose = () => { };
        }

        #region ITelemttryChannel

        /// <summary>
        /// Gets or sets a value indicating whether this channel is in developer mode.
        /// </summary>
        public bool? DeveloperMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the channel's URI. To this URI the telemetry is expected to be sent.
        /// </summary>
        public string EndpointAddress { get; set; }
        
        /// <summary>
        /// Implements the <see cref="ITelemetryChannel.Send"/> method by invoking the <see cref="OnSend"/> callback.
        /// </summary>
        public void Send(ITelemetry item)
        {
            if (ThrowError)
            {
                throw new Exception("test error");
            }

            OnSend(item);
        }

        /// <summary>
        /// Implements  the <see cref="ITelemetryChannel.Flush" /> method.
        /// </summary>
        public void Flush()
        {
            OnFlush();
        }

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether to throw an error.
        /// </summary>
        public bool ThrowError { get; set; }
    
        /// <summary>
        /// Gets or sets the callback invoked by the <see cref="Send"/> method.
        /// </summary>
        public Action<ITelemetry> OnSend { get; set; }

        public Action OnFlush { get; set; }

        public Action OnDispose { get; set; }

        /// <summary>
        /// Implements the <see cref="IDisposable.Dispose"/> method.
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }
    }
}
