using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Tests.System.Features
{
    public class FullFlowTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public FullFlowTests()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        public void Should_Register_Login_And_Edit_Profile()
        {
            // Registro de um novo usuário
            _driver.Navigate().GoToUrl("https://localhost:7119/register");
            _driver.FindElement(By.Id("Name")).SendKeys("Flow User");
            _driver.FindElement(By.Id("Email")).SendKeys("flowuser@example.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Flow@1234");
            _driver.FindElement(By.Id("Submit")).Click();

            // Login do usuário
            _driver.Navigate().GoToUrl("https://localhost:7119/login");
            _driver.FindElement(By.Id("Email")).SendKeys("flowuser@example.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Flow@1234");
            _driver.FindElement(By.Id("Submit")).Click();

            // Editar o perfil
            _driver.Navigate().GoToUrl("https://localhost:7119/profile/edit");
            _driver.FindElement(By.Id("Name")).Clear();
            _driver.FindElement(By.Id("Name")).SendKeys("Updated Flow User");
            _driver.FindElement(By.Id("Submit")).Click();

            // Verificar se o nome foi atualizado
            var updatedName = _driver.FindElement(By.Id("Name")).GetAttribute("value");
            Assert.Equal("Updated Flow User", updatedName);
        }
    }
}
