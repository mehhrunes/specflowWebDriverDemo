using InterviewTask.Pages;
using TechTalk.SpecFlow;

namespace InterviewTask.Steps;

[Binding]
public class HomeSteps
{
    private readonly HomePage _homePage;

    public HomeSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    [Given(@"a user adds (.*) random items to their cart")]
    public void GivenAUserAddsRandomItemsToTheirCart(int itemCount)
    {
        _homePage.AddRandomItemsToCart(itemCount);
    }
}