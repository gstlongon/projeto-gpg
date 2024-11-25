using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Tests.System.Features
{
    public class UserProfileTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public UserProfileTests()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        public void Should_Create_Update_And_Delete_User()
        {
            // Navegar até a página de cadastro
            _driver.Navigate().GoToUrl("https://localhost:7119/users/create");

            // Preencher os campos e criar um novo usuário
            _driver.FindElement(By.Id("Name")).SendKeys("Test User");
            _driver.FindElement(By.Id("Email")).SendKeys("testuser@example.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Test@1234");
            _driver.FindElement(By.Id("Submit")).Click();

            // Verificar se o usuário foi criado
            var successMessage = _driver.FindElement(By.Id("SuccessMessage"));
            Assert.NotNull(successMessage);

            // Atualizar o usuário
            _driver.Navigate().GoToUrl("https://localhost:7119/users/edit/1");
            _driver.FindElement(By.Id("Name")).Clear();
            _driver.FindElement(By.Id("Name")).SendKeys("Updated User");
            _driver.FindElement(By.Id("Submit")).Click();

            // Verificar se o nome foi atualizado
            var updatedName = _driver.FindElement(By.Id("Name")).GetAttribute("value");
            Assert.Equal("Updated User", updatedName);

            // Excluir o usuário
            _driver.FindElement(By.Id("Delete")).Click();
        }
    }
}
