using Moq;

namespace EnergyTrayTests
{
    public static class MockExtensions
    {
        public static void SetupAll<TType>(this Mock<TType> mock) where TType : class
        {
            SetupAllExtender.SetupAllMethodsOfInterface(mock);
        }
    }
}