using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace InterviewTask.Pages;

public class HomePage
{
    private readonly Actions _actions;
    private readonly FirefoxDriver _driver;
    
    public HomePage(FirefoxDriver driver, Actions actions)
    {
        _driver = driver;
        _actions = actions;
    }

    private IWebElement ProductsElement => _driver.FindElement(By.XPath("//ul[contains(@Class, 'products')]"));

    private List<IWebElement> AddToCartButtons =>
        ProductsElement.FindElements(By.XPath("//a[contains(@aria-label, 'to your cart')]")).ToList();

    private IWebElement PrimaryMenu => _driver.FindElement(By.Id("primary-menu"));
    private IWebElement CartLink => PrimaryMenu.FindElement(By.XPath("//a[text() = 'Cart']"));

    public void ClickCartLink()
    {
        CartLink.Click();
    }

    public void AddRandomItemsToCart(int itemCount)
    {
        var generatedIndexes = GenerateNonDuplicateRandomNumbers(AddToCartButtons.Count, itemCount);

        foreach (var index in generatedIndexes)
        {
            var parentElement = AddToCartButtons[index].FindElement(By.XPath("./.."));
            ScrollIntoView(parentElement);
            
            _actions.MoveToElement(parentElement)
                .Build()
                .Perform();
            
            AddToCartButtons[index].Click();
        }
    }

    private List<int> GenerateNonDuplicateRandomNumbers(int maxValue, int numberCount)
    {
        var indexList = new List<int>();

        for (var i = 0; i < numberCount; i++)
            while (indexList.Count != numberCount)
            {
                var randomIndex = new Random().Next(0, maxValue);
                if (!indexList.Contains(randomIndex))
                {
                    indexList.Add(randomIndex);
                }
            }

        return indexList;
    }

    private void ScrollIntoView(IWebElement element)
    {
        _driver.ExecuteScript("arguments[0].scrollIntoView();", element);
    }
}