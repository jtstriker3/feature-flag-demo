using System;
using System.ComponentModel;
using feature_flag_demo.Exceptions;
using Microsoft.Extensions.Configuration;

namespace feature_flag_demo
{
    public class FeatureFlagClient
    {
        private readonly IConfiguration configuration;

        public FeatureFlagClient(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string? GetFlagValue(string key) =>
                configuration.GetValue<string?>(key);
    }
}

