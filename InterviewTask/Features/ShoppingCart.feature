Feature: CartActions
Shopping cart allows to view items added to it.

    Scenario: Verify items in the cart
        Given a user adds 4 random items to their cart
        When user views their cart
        Then user finds a total of 4 items listed in their cart
        When user searches for lowest price item
        And user is able to remove the lowest price item from their cart
        Then user is able to verify 3 items in their cart