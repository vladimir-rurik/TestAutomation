Feature: ValidateBankAccount
	Validates and evaluates the account in the context of direct debit payment.

@negativeCase
Scenario: Verify an error response to a sample request without a JWT token
	Given a sample request without a JWT token
	When the sample request is posted to api
	Then Api returns "message" name as "Authorization has been denied for this request."

@negativeCase
Scenario: Verify an error response to a sample request with an empty JWT token
	Given a sample request without a JWT token
	When the sample request is posted to api
	Then Api returns "message" name as "Authorization has been denied for this request."