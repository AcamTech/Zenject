using System;
using System.Collections.Generic;
using Zenject;
using NUnit.Framework;
using System.Linq;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests.Bindings
{
    [TestFixture]
    public class TestFactoryTo5 : ZenjectUnitTestFixture
    {
        [Test]
        public void TestSelf1()
        {
            Container.BindFactory<string, int, string, float, int, Foo, Foo.Factory>().NonLazy();

            Container.Validate();

            Assert.IsEqual(Container.Resolve<Foo.Factory>().Create("asdf", 2, "a", 4.2f, 6).P1, "asdf");
        }

        [Test]
        public void TestSelf2()
        {
            Container.BindFactory<string, int, string, float, int, Foo, Foo.Factory>().NonLazy();

            Container.Validate();

            Assert.IsEqual(Container.Resolve<Foo.Factory>().Create("asdf", 2, "a", 4.2f, 6).P1, "asdf");
        }

        [Test]
        public void TestConcrete()
        {
            Container.BindFactory<string, int, string, float, int, IFoo, IFooFactory>().To<Foo>().NonLazy();

            Container.Validate();

            var ifoo = Container.Resolve<IFooFactory>().Create("asdf", 2, "a", 4.2f, 6);

            Assert.IsNotNull(ifoo);
            Assert.IsEqual(((Foo)ifoo).P1, "asdf");
        }

        interface IFoo
        {
        }

        class IFooFactory : Factory<string, int, string, float, int, IFoo>
        {
        }

        class Foo : IFoo
        {
            public Foo(string p1, int p2, string p3, float p4, int p5)
            {
                P1 = p1;
            }

            public string P1
            {
                get;
                private set;
            }

            public class Factory : Factory<string, int, string, float, int, Foo>
            {
            }
        }
    }
}


