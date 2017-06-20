
namespace NumericSequenceWorldNomadsTest.Framework
{
    public class Host
    {
        internal static readonly SeleniumApplication Instance = new SeleniumApplication();
        static Host()
        {
            Instance.Run("NumericSequenceWorldNomads", 5886);
        }
    }
}
