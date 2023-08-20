using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        //[DoNotParallelize]
        public async Task NovoCaminhao()
        {
            // Arrange
            var mock = new Mock<IRepositorio>();

            mock.Setup(library => library.Busca(Guid.Parse("8b9c6296-1d94-4b6b-9537-52b7118c0d7c")))
                .Returns(Task.FromResult<Caminhao>(                
                    new Caminhao()
                    {
                        Id = Guid.Parse("8b9c6296-1d94-4b6b-9537-52b7118c0d7c"),
                        AnoFabricacao = 2023,
                        AnoModelo = 2024,
                        Observacoes = "Teste de novo caminhão"
                    }
                ));

            // Act
            IRepositorio repositorio = mock.Object;
            var c = await repositorio.Busca(Guid.Parse("8b9c6296-1d94-4b6b-9537-52b7118c0d7c"));

            // Assert
            Assert.AreEqual(c.Id, Guid.Parse("8b9c6296-1d94-4b6b-9537-52b7118c0d7c"));
            Assert.AreEqual(c.AnoFabricacao, 2023);
            Assert.AreEqual(c.AnoModelo, 2024);
        }
    }
}
