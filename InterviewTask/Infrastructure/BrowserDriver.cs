using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace InterviewTask.Infrastructure;

public class BrowserDriver
{
    public IWebDriver Driver;

    public BrowserDriver()
    {
        Driver = CreateDriver();
    }

    private FirefoxDriver CreateDriver()
    {
        var firefoxDriver = new FirefoxDriver();
        return firefoxDriver;
    }
}