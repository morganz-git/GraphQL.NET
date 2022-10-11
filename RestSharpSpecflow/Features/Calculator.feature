Feature: BasicOperation
	Covers Basic API testing operations

	
	
	@smoke
Scenario: Get products
	Given I perform a GET operation of "Product/GetProductById/"
	| ProductId |
	| 1         |
	And I should get the product name as "keyboard"