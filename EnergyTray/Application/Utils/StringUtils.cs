using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using EnergyTray.Application.Model;

namespace EnergyTray.Application.Utils
{
    public class StringUtils
    {
        public static bool IsPowerSchemeOutput(string text) => text.Contains("Power Scheme GUID");

        public static string GetSchemeId(string outputLine)
        {
            outputLine = outputLine.Replace("Power Scheme GUID: ", "");
            outputLine = outputLine.Remove(36, outputLine.ToCharArray().Length - 36);
            return outputLine;
        }

        public static IEnumerable<PowerScheme> GetAllSchemes(string inputString)
        {
            var schemes = new List<PowerScheme>();

            if (inputString != null)
            {
                using (var reader = new StringReader(inputString))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ProcessLine(line, schemes);
                    }
                }
            }
            return schemes;
        }

        private static void ProcessLine(string line, ICollection<PowerScheme> schemes)
        {
            if (IsPowerSchemeOutput(line))
            {
                line = TrimString(line);
                var isActive = CheckIfActiveAndRemoveStar(ref line);
                schemes.Add(new PowerScheme
                {
                    Id = line.Remove(36, line.ToCharArray().Length - 36),
                    Name = line.Remove(0, 36),
                    IsActive = isActive
                });
            }
        }

        private static string TrimString(string line)
        {
            line = line.Trim();
            line = line.Replace("Power Scheme GUID: ", "");
            line = line.Replace("  (", "");
            line = line.Replace(")", "");
            line = line.Replace(")", "");
            return line;
        }

        private static bool CheckIfActiveAndRemoveStar(ref string line)
        {
            var isActive = false;

            if (line.Contains("*"))
            {
                isActive = true;
                line = line.Replace("*", "");
            }
            return isActive;
        }
    }
}