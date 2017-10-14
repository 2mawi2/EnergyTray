using System;
using System.Collections.Generic;
using System.Linq;
using EnergyTray;
using EnergyTray.Application.Model;
using EnergyTray.Application.Utils;
using Xunit;

namespace EnergyTrayTests
{
    public class StringUtilsTest
    {
        [Theory]
        [InlineData("Power Scheme GUID", true)]
        [InlineData("Pofdsafjkls", false)]
        public void IsPowerSchemeOutputTest(string input, bool expected)
        {
            var result = StringUtils.IsPowerSchemeOutput(input);
            Assert.Equal(result, expected);
        }


        [Fact]
        public void GetSchemeIdTest()
        {
            var result = StringUtils.GetSchemeId("Power Scheme GUID: 49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b  (Dell)");
            Assert.Equal(result, "49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b");
        }


        [Fact]
        public void GetAllSchemesTest()
        {
            var inputString = @"
            Existing Power Schemes (* Active)
            -----------------------------------
            Power Scheme GUID: 0688d228-2803-44ee-917d-2e544c763797  (Download)
            Power Scheme GUID: 381b4222-f694-41f0-9685-ff5bb260df2e  (Balanced)
            Power Scheme GUID: 49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b  (Dell) *
            Power Scheme GUID: 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c  (High performance)
            Power Scheme GUID: a1841308-3541-4fab-bc81-f71556f20b4a  (Power saver)
            ";

            var shemes = StringUtils.GetAllSchemes(inputString).ToList();

            Assert.Contains(shemes, scheme => scheme.Id == "0688d228-2803-44ee-917d-2e544c763797");
            Assert.Contains(shemes, scheme => scheme.Id == "381b4222-f694-41f0-9685-ff5bb260df2e");
            Assert.Contains(shemes, scheme => scheme.Id == "49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b");
            Assert.Contains(shemes, scheme => scheme.Id == "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
            Assert.Contains(shemes, scheme => scheme.Id == "a1841308-3541-4fab-bc81-f71556f20b4a");
        }


        [Fact]
        public void GetAllSchemesTest_FullOutput()
        {
            var inputString =
                @"Microsoft Windows [Version 10.0.15063]
                (c) 2017 Microsoft Corporation. All rights reserved.
                
                C:\Users\Marius\RiderProjects\EnergyTray\EnergyTray\bin\Debug>powercfg.exe /list
                
                Existing Power Schemes (* Active)
                -----------------------------------
                Power Scheme GUID: 0688d228-2803-44ee-917d-2e544c763797  (Download)
                Power Scheme GUID: 381b4222-f694-41f0-9685-ff5bb260df2e  (Balanced)
                Power Scheme GUID: 49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b  (Dell)
                Power Scheme GUID: 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c  (High performance) *
                Power Scheme GUID: a1841308-3541-4fab-bc81-f71556f20b4a  (Power saver)
                
                C:\Users\Marius\RiderProjects\EnergyTray\EnergyTray\bin\Debug>exit
                ";
            var shemes = StringUtils.GetAllSchemes(inputString).ToList();

            Assert.Contains(shemes, scheme => scheme.Id == "0688d228-2803-44ee-917d-2e544c763797");
            Assert.Contains(shemes, scheme => scheme.Id == "381b4222-f694-41f0-9685-ff5bb260df2e");
            Assert.Contains(shemes, scheme => scheme.Id == "49ef8fc0-bb7f-488e-b6a0-f1fc77ec649b");
            Assert.Contains(shemes, scheme => scheme.Id == "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
            Assert.Contains(shemes, scheme => scheme.Id == "a1841308-3541-4fab-bc81-f71556f20b4a");
        }

        [Fact]
        public void GetAllSchemesTest_ValueIsEmty()
        {
            var inputString = String.Empty;
            var shemes = StringUtils.GetAllSchemes(inputString).ToList();
            Assert.NotNull(shemes);
        }
        
        [Fact]
        public void GetAllSchemesTest_ValueIsNull()
        {
            string inputString = null;
            var shemes = StringUtils.GetAllSchemes(inputString).ToList();
            Assert.NotNull(shemes);
        }
    }
}