using Mappers.UseCases;
using NUnit.Framework;

namespace Mappers
{
    [TestFixture]
    internal class SimpleCasesTests
    {

        [Test]
        public void ManualMapper()
        {
            var simpleCases = new SimpleCases();

            var result = simpleCases.ManualMappingReference();

            Assert.IsNotNull(result);

        }

        [Test]
        public void AutoMapper() 
        {
            var simpleCases = new SimpleCases();

            var result = simpleCases.AutoMapper();

            Assert.IsNotNull(result);

        }

        [Test]
        public void MapsterMapper()
        {
            var simpleCases = new SimpleCases();

            var result = simpleCases.Mapster();

            Assert.IsNotNull(result);

        }

        [Test]
        public void MapperlyMapper()
        {
            var simpleCases = new SimpleCases();

            var result = simpleCases.Mapperly();

            Assert.IsNotNull(result);

        }

        [Test]
        public void TinyMapper()
        {
            var simpleCases = new SimpleCases();

            var result = simpleCases.TinyMapper();

            Assert.IsNotNull(result);

        }
    }
}
