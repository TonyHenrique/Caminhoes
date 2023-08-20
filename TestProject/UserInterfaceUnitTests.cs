using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;
using OpenQA.Selenium;
using System.Threading.Tasks;
using System;
using OpenQA.Selenium.Support.UI;
using TonyWebApplication;
using OpenQA.Selenium.Support.Extensions;

namespace UserInterfaceUnitTests
{
    /*
    /// Para rodar os Testes, instalar o 
    /// 1) Google Chrome:
    /// https://www.google.com/chrome
    /// 
    /// 2) OpenQA.Selenium chromedriver.exe file does not exist in the current directory or in a directory on the PATH environment variable. The driver can be downloaded at 
    /// http://chromedriver.storage.googleapis.com/index.html
    ///
    */

    [TestClass]
    public class CadastroUnitTest
    {
        const string WebSite = //"https://localhost:44394"// 👈 Put the Web App url here. Then Start the Project ProvaDevNet without Debugging
                                "https://tonyhenriquemeta.azurewebsites.net"
                                ;

        IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver();

        [TestMethod]
        [DoNotParallelize]
        public async Task NovoCaminhaoDadosValidos()
        {
            await Task.CompletedTask;

            driver.Navigate().GoToUrl($"{WebSite}/admin/Caminhao/");

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(w => w.FindElement(By.Id("ButtonNovoCaminhao")));

            driver.FindElement(By.Id("ButtonNovoCaminhao")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(w => w.FindElement(By.Id("Modelo")));

            var CaminhaoID = Guid.Parse(driver.FindElement(By.Name("CaminhaoID")).GetAttribute("value"));

            driver.FindElement(By.Id("Modelo")).HomeAndSendKeys("FH");
            driver.FindElement(By.Id("AnoFabricacao")).SelectAllAndSendKeys("2021");
            driver.FindElement(By.Id("AnoModelo")).SelectAllAndSendKeys("2022");

            driver.FindElement(By.Id("Observacoes")).SelectAllAndSendKeys("Teste de novo cadastro");

            driver.FindElement(By.Id("ButtonSalvar")).Click();

            // Estando tudo certo, vai voltar a tela da listagem com o Botão Novo Caminhão...
            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("ButtonNovoCaminhao")));

            //// TODO: Verifica se os dados gravados ficaram corretos... no Banco de Dados. Como usei o Sqlite omiti por simplicidade.
            //var Caminhao = await Operacoes.Busca(CaminhaoID);

            //Assert.AreEqual(Caminhao.Modelo, "FH");
            //Assert.AreEqual(Caminhao.AnoFabricacao, 2021);
            //Assert.AreEqual(Caminhao.AnoModelo, 2022);
            //Assert.AreEqual(Caminhao.Observacoes, "Teste de novo cadastro");

            driver.Close();
        }

        [TestMethod]
        [DoNotParallelize]
        public void NovoCaminhaoModeloIncorreto()
        {
            driver.Navigate().GoToUrl($"{WebSite}/admin/Caminhao/");

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("ButtonNovoCaminhao")));

            driver.FindElement(By.Id("ButtonNovoCaminhao")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("Modelo")));

            driver.FindElement(By.Id("Modelo")).HomeAndSendKeys("TR");
            driver.FindElement(By.Id("AnoFabricacao")).SelectAllAndSendKeys("2021");
            driver.FindElement(By.Id("AnoModelo")).SelectAllAndSendKeys("2022");

            driver.FindElement(By.Id("Observacoes")).SelectAllAndSendKeys("Teste de novo cadastro");

            driver.FindElement(By.Id("ButtonSalvar")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("MensagemValidacao")));

            var MensagemValidacao = driver.FindElement(By.Id("MensagemValidacao")).Text;

            Assert.AreEqual(MensagemValidacao, "Modelo (Poderá aceitar apenas FH e FM)");

            driver.Close();
        }

        [TestMethod]
        [DoNotParallelize]
        public void NovoCaminhaoAnoFabricacaoNaoAtual()
        {
            driver.Navigate().GoToUrl($"{WebSite}/admin/Caminhao/");

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("ButtonNovoCaminhao")));

            driver.FindElement(By.Id("ButtonNovoCaminhao")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("Modelo")));

            driver.FindElement(By.Id("Modelo")).HomeAndSendKeys("FH");
            driver.FindElement(By.Id("AnoFabricacao")).SelectAllAndSendKeys("2020");
            driver.FindElement(By.Id("AnoModelo")).SelectAllAndSendKeys("2020");

            driver.FindElement(By.Id("Observacoes")).SelectAllAndSendKeys("Teste de novo cadastro");

            driver.FindElement(By.Id("ButtonSalvar")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("MensagemValidacao")));

            var MensagemValidacao = driver.FindElement(By.Id("MensagemValidacao")).Text;

            // Ano Atual e Ano Subsequente
            var AnoAtual = DateTime.Now.Year;
            var AnoSubsequente = AnoAtual + 1;

            Assert.AreEqual(MensagemValidacao, $"Ano de Fabricação (Ano deverá ser o atual {AnoAtual})");

            driver.Close();
        }

        [TestMethod]
        [DoNotParallelize]
        public void NovoCaminhaoAnoFabricacaoAnoModeloInvalido()
        {
            driver.Navigate().GoToUrl($"{WebSite}/admin/Caminhao/");

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("ButtonNovoCaminhao")));

            driver.FindElement(By.Id("ButtonNovoCaminhao")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("Modelo")));

            driver.FindElement(By.Id("Modelo")).HomeAndSendKeys("FH");
            driver.FindElement(By.Id("AnoFabricacao")).SelectAllAndSendKeys("2021");
            driver.FindElement(By.Id("AnoModelo")).SelectAllAndSendKeys("2023");

            driver.FindElement(By.Id("Observacoes")).SelectAllAndSendKeys("Teste de novo cadastro");

            driver.FindElement(By.Id("ButtonSalvar")).Click();

            new WebDriverWait(driver, TimeSpan.FromMinutes(1)).Until(e => e.FindElement(By.Id("MensagemValidacao")));

            var MensagemValidacao = driver.FindElement(By.Id("MensagemValidacao")).Text;

            // Ano Atual e Ano Subsequente
            var AnoAtual = DateTime.Now.Year;
            var AnoSubsequente = AnoAtual + 1;

            Assert.AreEqual(MensagemValidacao, $"Ano Modelo (Poderá ser o atual {AnoAtual} ou o ano subsequente {AnoSubsequente})");

            driver.Close();
        }

    }
}
