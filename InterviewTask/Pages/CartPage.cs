using InterviewTask.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace InterviewTask.Pages;

public class CartPage
{
    private readonly FirefoxDriver _driver;
    private readonly WebDriverWait _wait;

    public CartPage(FirefoxDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    private IWebElement ShopTable => _driver.FindElement(By.TagName("tbody"));

    private List<IWebElement> LowestPricedProduct =>
        ShopTable.FindElement(By.XPath($"//span[text() = '{GetLowestItemPrice()}']/parent::td/parent::tr")).FindElements(By.TagName("td")).ToList();

    private List<IWebElement> ProductPrices => ShopTable.FindElements(By.ClassName("product-price")).ToList();

    public int CartItemCount => ProductPrices.Count;

    private By RemoveItemByPriceLocator(decimal itemPrice)
    {
        return By.XPath($"//span[text() = '{itemPrice}']/parent::td/parent::tr/descendant::a[@class = 'remove']");
    }

    public void RemoveLowestPriceItem()
    {
        var removeItemElement = ShopTable.FindElement(RemoveItemByPriceLocator(GetLowestItemPrice()));
        removeItemElement.Click();

        var updatedProductCount = ProductPrices.Count - 1;
        _wait.Until(_ => ProductPrices.Count.Equals(updatedProductCount));
    }

    public ProductModel GetLowestPricedItemDetails()
    {
        var lowestPriceProductName =
            LowestPricedProduct.Single(x => x.GetAttribute("class").Equals("product-name")).Text;
        var lowestPricedProductPrice =
            LowestPricedProduct.Single(x => x.GetAttribute("class").Equals("product-price")).Text;

        return new ProductModel
        {
            Name = lowestPriceProductName,
            Price = lowestPricedProductPrice
        };
    }

    private decimal GetLowestItemPrice()
    {
        var priceList = ProductPrices.Select(x => x.Text.Remove(0, 1)).ToList();
        var decimalPriceList = new List<decimal>();

        priceList.ForEach(x =>
        {
            decimal.TryParse(x, out var convertedPrice);
            decimalPriceList.Add(convertedPrice);
        });

        return decimalPriceList.Min();
    }
}