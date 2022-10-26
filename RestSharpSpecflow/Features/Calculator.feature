Feature: BasicOperation
Covers Basic API testing operations

    @smoke
    Scenario: Get products
        Given I perform a GET operation of "Product/GetProductById/{id}"
          | ProductId |
          | 1         |
    #        And I add header with authorization
    #        And  I enter the body details
        And I should get the product name as "Keyboard"