namespace EnergyTray
{
    public class StringUtils
    {
        public static bool IsPowerSchemeOutput(string powerSchemeGuid) => powerSchemeGuid.Contains("Power Scheme GUID");

        public static string GetSchemeId(string outputLine)
        {
            outputLine = outputLine.Replace("Power Scheme GUID: ", "");
            outputLine = outputLine.Remove(36, outputLine.ToCharArray().Length - 36);
            return outputLine;
        }
    }
}