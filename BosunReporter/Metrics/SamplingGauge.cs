﻿using System;
using System.Collections.Generic;
using System.Threading;
using BosunReporter.Infrastructure;

namespace BosunReporter.Metrics
{
    public class SamplingGauge : BosunMetric, IDoubleGauge
    {
        private double _value = Double.NaN;

        public double CurrentValue => _value;

        public override string MetricType => "gauge";

        protected override IEnumerable<string> GetSerializedMetrics(string unixTimestamp)
        {
            var value = _value;
            if (Double.IsNaN(value))
                yield break;

            yield return ToJson("", value, unixTimestamp);
        }

        /// <summary>
        /// Records the current value of the gauge. Use Double.NaN to disable this gauge.
        /// </summary>
        /// <param name="value"></param>
        public void Record(double value)
        {
            AssertAttached();
            Interlocked.Exchange(ref _value, value);
        }
    }
}
