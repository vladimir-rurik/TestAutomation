﻿Feature: BankAccountValidation
	Validates and evaluates the account in the context of direct debit payment.


Background: 
	Given a sample request with a json content type


@negativeCase
Scenario Outline: Verify an error response to a sample request WITHOUT a JWT token and different bank accounts
	Given the sample request without a JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns StatusCode name as "401"
		And Api returns "message" name as "Authorization has been denied for this request."

	Examples: 
		| Description  | bankAccount            | 
		| valid IBAN   | GB09HAOE91311808002317 | 
		| invalid IBAN | GB09HAOE91311808002318 | 


@negativeCase
Scenario Outline: Verify an error response to a sample request with an EMPTY JWT token and different bank accounts
	Given the sample request with an empty JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns StatusCode name as "401"
		And Api returns "message" name as "Authorization has been denied for this request."

	Examples: 
		| Description  | bankAccount            | 
		| valid IBAN   | GB09HAOE91311808002317 | 
		| invalid IBAN | GB09HAOE91311808002318 | 


@lenthValidation 
Scenario Outline: Verify a length based validation for a sample request with a VALID JWT token and different bank accounts
	Given the sample request with a valid JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns StatusCode name as "<statusCode>"
		And Api returns "*code" name as "<*code>"

	Examples: 
		| Description	    | bankAccount						   | statusCode | *code    |
		| IBAN length > 34  | GB2345678901234567890123456789012345 | 400        | 400\.00. |
		| IBAN length < 7   | GB345								   | 400        | 400\.00. |


@externalValidation 
Scenario Outline: Verify an external validation for a sample request with a VALID JWT token and different bank accounts
	Given the sample request with a valid JWT token and "<bankAccount>"
	When the sample request is posted to api
	Then Api returns StatusCode name as "<statusCode>"
		And Api returns "isValid" name as "<isValid>"
		And Api returns "*riskCheckMessages" name as "<*riskCheckMessages>"

	Examples: 
		| Description  | bankAccount              | statusCode | isValid | *riskCheckMessages |
		| valid IBAN   | GB09HAOE91311808002317   | 200        | True    |                    |
		| invalid IBAN | GB09HAOE91311808002318   | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | DE87123456781234567890   | 200        | True    |                    |
		| invalid IBAN | DE87123456781234567891   | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | DK5000400440116243       | 200        | True    |                    |
		| invalid IBAN | DK5000400440116244       | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | AT611904300234573201     | 200        | True    |                    |
		| invalid IBAN | AT6119043002345732013    | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | CH9300762011623852957    | 200        | True    |                    |
		| invalid IBAN | CH9300762011623852958    | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | FI2112345600000785       | 200        | True    |                    |
		| invalid IBAN | FI2112345600000786       | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | SE4550000000058398257466 | 200        | True    |                    |
		| invalid IBAN | SE4550000000058398257467 | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | NO9386011117947		  | 200        | True    |                    |
		| invalid IBAN | NO9386011117948		  | 200        | False   | ^(?!\s*$).+        |
		| valid IBAN   | MZ97123412341234123412341| 200        | True    |                    |
		| invalid IBAN | MZ9712341234123412341234 | 200        | False   | ^(?!\s*$).+        |