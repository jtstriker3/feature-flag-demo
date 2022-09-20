using System;
namespace feature_flag_demo.Exceptions
{
    public class CouldNotResolveVersionedServiceException : Exception
    {
        public CouldNotResolveVersionedServiceException(Type type, string flag, string version) : base($"Could not find service for type: {type.FullName} falg: {flag} version: {version}")
        {
        }
    }
}

