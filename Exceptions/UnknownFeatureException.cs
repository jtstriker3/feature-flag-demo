using System;
namespace feature_flag_demo.Exceptions
{
    public class UnknownFeatureException : Exception
    {
        public UnknownFeatureException(string key) : base($"Could not found feature flag for {key}")
        {
        }
    }
}

