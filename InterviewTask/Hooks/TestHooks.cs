using BoDi;
using InterviewTask.Infrastructure;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace InterviewTask.Hooks;

[Binding]
public class TestHooks
{
    private const string Url = "https://cms.demo.katalon.com/";

    [BeforeScenario]
    public void SetUpDriver(IObjectContainer container)
    {
        var driver = new BrowserDriver().Driver;
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var actions = new Actions(driver);
        container.RegisterInstanceAs(driver);
        container.RegisterInstanceAs(wait);
        container.RegisterInstanceAs(actions);

        driver.Manage().Window.Maximize();
        driver.Url = Url;
    }

    [AfterScenario]
    public void TearDownDriver(IObjectContainer container)
    {
        container.Resolve<FirefoxDriver>().Quit();
    }
}