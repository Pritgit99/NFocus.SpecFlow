Feature: PlaceOrder

This BDD file consists of two scenarios that test the functionality of an e-commerce website: the first scenario checks whether a coupon code is
applied correctly to the cart, and the second scenario ensures that a placed order appears in the user's order history.


Background:
Given i am logged in on the shop page

Scenario: Apply coupon discount
When I add an item to my cart
And I apply the coupon code "edgewords"
Then a discount of 15% should be applied to my basket total

Scenario: View new order in order history
When I add an item to my cart
And I go through checkout
Then the order number shown should be on the order history page