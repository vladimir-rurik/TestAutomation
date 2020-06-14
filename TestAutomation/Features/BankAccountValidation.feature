Feature: BankAccountValidation
	Validates and evaluates the account in the context of direct debit payment.

Background: 
	Given a sample request with a json content type

@negativeCase
Scenario: Verify an error response to a sample request without a JWT token
	Given the sample request without a JWT token
	When the sample request is posted to api
	Then Api returns "message" name as "Authorization has been denied for this request."

@negativeCase
Scenario: Verify an error response to a sample request with an empty JWT token
	Given the sample request with an empty JWT token
	When the sample request is posted to api
	Then Api returns "message" name as "Authorization has been denied for this request."

#@positiveCase
#Scenario Outline: Verify a valid response to a sample request with a valid JWT token
#	Given the sample request with a valid JWT token
#	When the sample request is posted to api

	#Examples: 
	#	| bankAccount            |
	#	| GB09HAOE91311808002317 |

