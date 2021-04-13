using System;

using OpenTelemetry.Instrumentation;
using OpenTelemetry.Trace;

using Quartz.Logging;
using Quartz.OpenTelemetry.Instrumentation.Implementation;

namespace Quartz.OpenTelemetry.Instrumentation
{
    internal class QuartzJobInstrumentation : IDisposable
    {
        private readonly DiagnosticSourceSubscriber diagnosticSourceSubscriber;

        public QuartzJobInstrumentation()
            : this(new QuartzInstrumentationOptions())
        {
        }

        public QuartzJobInstrumentation(QuartzInstrumentationOptions options)
        {
            var listener = new QuartzDiagnosticListener(DiagnosticHeaders.DefaultListenerName, options);
            diagnosticSourceSubscriber = new DiagnosticSourceSubscriber(listener, null);
            diagnosticSourceSubscriber.Subscribe();
        }

        public void Dispose()
        {
            diagnosticSourceSubscriber?.Dispose();
        }
    }
}
