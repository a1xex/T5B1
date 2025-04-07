using System;

namespace Trainer_v5.Utils
{
    // Provides general utility methods.
	public static class GeneralUtils
	{
		public static string GetGameVersion()
		{
#if !SWINCBETA && !SWINCRELEASE
			return "1.6";
#elif SWINCBETA1_7
			return "1.7";
#elif SWINCBETA1_8
			return "1.8";
#elif SWINCBETA1_9
			return "1.9";
#elif SWINCBETA1_10
			return "1.10";
#else
			return "UNKNOWN";
#endif
		}

        public static void TryExecute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                // Call the LogException extension method from Logger class
                Logger.LogException(ex);
            }
        }
	}
} 