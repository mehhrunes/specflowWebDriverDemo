using FluentAssertions;
using InterviewTask.Pages;
using TechTalk.SpecFlow;

namespace InterviewTask.Steps;

[Binding]
public class CartSteps
{
    private readonly CartPage _cartPage;
    private readonly HomePage _homePage;

    public CartSteps(HomePage homePage, CartPage cartPage)
    {
        _homePage = homePage;
        _cartPage = cartPage;
    }

    [When(@"user views their cart")]
    public void WhenUserViewsTheirCart()
    {
        _homePage.ClickCartLink();
    }

    [Then(@"user finds a total of (.*) items listed in their cart")]
    public void ThenUserFindsATotalOfItemsListedInTheirCart(int itemCount)
    {
        _cartPage.CartItemCount.Should().Be(itemCount);
    }

    [When(@"user searches for lowest price item")]
    public void WhenUserSearchesForLowestPriceItem()
    {
        const string expectedName = "expected product name";
        const string expectedPrice = "expected product price";

        var lowestPricedItem = _cartPage.GetLowestPricedItemDetails();

        //Would assert values here, but won't assert random ones
        Console.WriteLine($"Lowest priced item name: {lowestPricedItem.Name}");
        Console.WriteLine($"Lowes priced item price: {lowestPricedItem.Price}");
    }

    [When(@"user is able to remove the lowest price item from their cart")]
    public void WhenUserIsAbleToRemoveTheLowestPriceItemFromTheirCart()
    {
        _cartPage.RemoveLowestPriceItem();
    }

    [Then(@"user is able to verify (.*) items in their cart")]
    public void ThenUserIsAbleToVerifyItemsInTheirCart(int itemCount)
    {
        _cartPage.CartItemCount.Should().Be(itemCount);
    }
}