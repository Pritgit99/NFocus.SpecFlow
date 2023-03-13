Feature: PlaceOrder

A short summary of the feature


Background: 
Given I am on the cart page with an item in my basket

@tag1
Scenario: Check if coupon discount is correct
	When I enter edgewords
	And I click Apply
	Then There should be a 15% discount


@tag1
Scenario: Place successful order
	When I click proceed to checkout
	And I place order
	Then The order number displayed should also be on the orders page







