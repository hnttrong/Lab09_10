using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginAutomation;

[TestFixture]
public sealed class LoginTests
{
    private IWebDriver? _driver;

    public static IEnumerable<TestCaseData> ValidCredentials()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "LoginTestData.csv");
        return File.ReadLines(path).Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(','))
            .Select(cells => new TestCaseData(cells[0], cells[1]).SetName($"Login_{cells[0]}_Succeeds"));
    }

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1440,1000");
        options.AddArgument("--no-sandbox");
        _driver = new ChromeDriver(options);
    }

    [TearDown]
    public void TearDown() => _driver?.Quit();

    [TestCaseSource(nameof(ValidCredentials))]
    public void Login_WithValidCredentials_ShouldSucceed(string username, string password)
    {
        NavigateToLogin();
        _driver!.FindElement(By.Id("username")).SendKeys(username);
        _driver.FindElement(By.Id("password")).SendKeys(password);
        _driver.FindElement(By.Id("loginButton")).Click();
        Assert.That(_driver.FindElement(By.Id("loginResult")).GetAttribute("data-status"), Is.EqualTo("success"));
        SaveEvidenceScreenshot(username);
    }

    [Test]
    public void Login_WithInvalidCredentials_ShouldFail()
    {
        NavigateToLogin();
        _driver!.FindElement(By.Id("username")).SendKeys("unknown");
        _driver.FindElement(By.Id("password")).SendKeys("wrong");
        _driver.FindElement(By.Id("loginButton")).Click();
        Assert.That(_driver.FindElement(By.Id("loginResult")).GetAttribute("data-status"), Is.EqualTo("error"));
    }

    private void NavigateToLogin()
    {
        var baseUrl = Environment.GetEnvironmentVariable("LAB_APP_URL") ?? "http://localhost:8080";
        _driver!.Navigate().GoToUrl($"{baseUrl.TrimEnd('/')}/login");
    }

    private void SaveEvidenceScreenshot(string username)
    {
        var captureDirectory = Environment.GetEnvironmentVariable("CAPTURE_DIR");
        if (username != "user1" || string.IsNullOrWhiteSpace(captureDirectory))
        {
            return;
        }

        Directory.CreateDirectory(captureDirectory);
        var screenshot = ((ITakesScreenshot)_driver!).GetScreenshot();
        screenshot.SaveAsFile(Path.Combine(captureDirectory, "04-login-success.png"));
    }
}
