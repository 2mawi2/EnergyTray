using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Moq;
using Moq.Language.Flow;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace EnergyTrayTests
{
    public class SetupAllExtender : MockRelay
    {
        public static void SetupAllMethodsOfInterface<TType>(Mock<TType> mock) where TType : class
        {
            typeof(SetupAllExtender).GetMethod(nameof(SetupMockWithInterfaceGeneric))
                                    .MakeGenericMethod(typeof(TType)).Invoke(null, new object[] {mock});
        }

        public static void SetupMockWithInterfaceGeneric<TType>(Mock<TType> mock) where TType : class
        {
            foreach (var methodInfo in typeof(TType).GetMethods())
            {
                if (methodInfo.ReturnType != typeof(void))
                {
                    var setup = typeof(SetupAllExtender).GetMethod(nameof(SetupAsGenericFunc))
                                                        .MakeGenericMethod(typeof(TType), methodInfo.ReturnType)
                                                        .Invoke(null, new object[] {methodInfo, mock});

                    typeof(SetupAllExtender).GetMethod(nameof(SetupReturnsAsGenericFunc))
                                            .MakeGenericMethod(typeof(TType), methodInfo.ReturnType)
                                            .Invoke(null, new[] {setup});
                }
                else
                {
                    var setup = typeof(SetupAllExtender).GetMethod(nameof(SetupAsGenericAction))
                                                        .MakeGenericMethod(typeof(TType))
                                                        .Invoke(null, new object[] {methodInfo, mock});
                }
            }
        }

        public static ISetup<TMock, TResult> SetupAsGenericFunc<TMock, TResult>(MethodInfo method, Mock<TMock> mock)
            where TMock : class
        {
            var input = Expression.Parameter(typeof(TMock));
            var parameters = method.GetParameters();
            Expression<Func<TMock, TResult>> setup;

            if (parameters.Length > 0)
            {
                var properties = parameters.Select(
                    pi => Expression.Call(typeof(It), nameof(It.IsAny), new[] {pi.ParameterType}));

                setup = Expression.Lambda<Func<TMock, TResult>>(
                    Expression.Call(input, method, properties), input);
            }
            else
            {
                setup = Expression.Lambda<Func<TMock, TResult>>(
                    Expression.Call(input, method), input);
            }

            return mock.Setup(setup);
        }

        public static void SetupReturnsAsGenericFunc<TMock, TResult>(ISetup<TMock, TResult> setup)
            where TMock : class
        {
            setup.Returns(() => new Fixture().Create<TResult>());
        }

        public static ISetup<TMock> SetupAsGenericAction<TMock>(MethodInfo method, Mock<TMock> mock)
            where TMock : class
        {
            var input = Expression.Parameter(typeof(TMock));
            var parameters = method.GetParameters();
            Expression<Action<TMock>> setup;

            if (parameters.Length > 0)
            {
                var properties = parameters.Select(
                    pi => Expression.Call(typeof(It), nameof(It.IsAny), new[] {pi.ParameterType}));

                setup = Expression.Lambda<Action<TMock>>(
                    Expression.Call(input, method, properties), input);
            }
            else
            {
                setup = Expression.Lambda<Action<TMock>>(
                    Expression.Call(input, method), input);
            }

            return mock.Setup(setup);
        }
    }
}