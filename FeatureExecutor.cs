using System;
namespace feature_flag_demo
{
    public class FeatureExecutor
    {
        private readonly FeatureFlagClient featureFlagClient;

        public FeatureExecutor(FeatureFlagClient featureFlagClient)
        {
            this.featureFlagClient = featureFlagClient;
        }

        public T? Execute<T, TFlag>(string key, bool throwIfNoLogicFound = false, params (TFlag flag, Func<T> logic)[] impls) where T : class
        {
            var flagValue = featureFlagClient.GetFlagValue<TFlag?>(key);
            var impl = impls.FirstOrDefault(x => EqualityComparer<TFlag>.Default.Equals(x.flag, flagValue));

            if (!EqualityComparer<(TFlag flag, Func<T> logic)>.Default.Equals(impl, default))
                return impl.logic.Invoke();

            if (throwIfNoLogicFound)
                throw new Exception("cannot find impl");

            return default;
        }

        public void Execute<TFlag>(string key, bool throwIfNoLogicFound = false, params (TFlag flag, Action logic)[] impls)
        {
            var flagValue = featureFlagClient.GetFlagValue<TFlag>(key);
            var impl = impls.FirstOrDefault(x => EqualityComparer<TFlag>.Default.Equals(x.flag, flagValue));

            if (!EqualityComparer<(TFlag flag, Action logic)>.Default.Equals(impl, default))
                impl.logic.Invoke();

            if (throwIfNoLogicFound)
                throw new Exception("cannot find impl");
        }


    }
}

