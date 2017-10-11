using System.Security.Cryptography.X509Certificates;
using Moq;
using Ploeh.AutoFixture.NUnit2;
using Xunit;

namespace EnergyTrayTests
{
    public class AnotherFoo
    {
        public int IntProp { get; set; }
    }

    public class Foo
    {
        public int IntProp { get; set; }

        public double DoubleProp { get; set; }
        public AnotherFoo Child { get; set; }
    }

    public interface IFooGetter
    {
        Foo GetFoo(int param1, double param2);
        void GetNoFoo(int param1, double param2);
    }

    public class FooExecutioner
    {
        private readonly IFooGetter _fooGetter;

        public FooExecutioner(IFooGetter fooGetter)
        {
            _fooGetter = fooGetter;
        }

        public Foo DoSth(int param1, double param2)
        {
            return _fooGetter.GetFoo(param1, param2);
        }

        public void DoSthElse(int param1, double param2)
        {
            _fooGetter.GetNoFoo(param1, param2);
        }
    }

    public class MockExtensionsTests
    {
        [Fact]
        public void SetupAllTest()
        {
            var mock = new Mock<IFooGetter>();
            mock.SetupAll();
            var exec = new FooExecutioner(mock.Object);

            var result = exec.DoSth(1, 2.2);

            mock.Verify(i => i.GetFoo(It.Is<int>(j => j == 1), It.Is<double>(j => j == 2.2)));
            Assert.NotNull(result.Child);
            Assert.NotNull(result);
        }

        [Fact]
        public void SetupAllTest_Null()
        {
            var mock = new Mock<IFooGetter>();
            var exec = new FooExecutioner(mock.Object);

            var result = exec.DoSth(1, 2.2);

            mock.Verify(i => i.GetFoo(It.Is<int>(j => j == 1), It.Is<double>(j => j == 2.2)));
            Assert.Null(result);
        }

        [Fact]
        public void SetupAllTest_NoReturnValue()
        {
            var mock = new Mock<IFooGetter>();
            var exec = new FooExecutioner(mock.Object);

            exec.DoSthElse(1, 2.2);

            mock.Verify(i => i.GetNoFoo(It.Is<int>(j => j == 1), It.Is<double>(j => j == 2.2)));
        }
    }
}