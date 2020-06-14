Feature: BankAccountValidation
	Validates and evaluates the account in the context of direct debit payment.


Background: 
	Given a sample request with a json content type


@negativeCase
Scenario Outline: Verify an error response to a sample request WITHOUT a JWT token and different bank accounts
	Given the sample request without a JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns "message" name as "Authorization has been denied for this request."

	Examples: 
		| bankAccount            | errorKey | errorValue                                      |
		| GB09HAOE91311808002317 | message  | Authorization has been denied for this request. |
		| GB09HAOE91311808002318 | message  | Authorization has been denied for this request. |


@negativeCase
Scenario Outline: Verify an error response to a sample request with an EMPTY JWT token and different bank accounts
	Given the sample request with an empty JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns "<errorKey>" name as "<errorValue>"

	Examples: 
		| bankAccount            | errorKey | errorValue                                      |
		| GB09HAOE91311808002317 | message  | Authorization has been denied for this request. |
		| GB09HAOE91311808002318 | message  | Authorization has been denied for this request. |


@positiveCase
Scenario Outline: Verify a valid response to a sample request with a VALID JWT token and different bank accounts
	Given the sample request with a valid JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns "<errorKey>" name as "<errorValue>"

	Examples: 
		| bankAccount            | errorKey | errorValue |
		| GB09HAOE91311808002317 | isValid  | True	     |
		| GB09HAOE91311808002318 | isValid  | True       |

