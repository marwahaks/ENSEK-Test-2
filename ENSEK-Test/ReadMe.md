# Overview
## Structure
The structure of the test project has been done in a way where I have the low level logic within the method classes 
and it is called by the testfixture calling class, 'Tests.cs'.
I have done this, so that the code remains maintainable and easy to read and each method has its own responsibility.

Currently 2 tests are failing, detailed below in the Defect Considerations section

## Defect Considerations

//DEFECT: RETURNS WRONG ORDER
Endpoint = $"/ENSEK/orders/{orderId}" - fetches the wrong order

//DEFECT: Internal Server Error
The Delete enpoint throws and internal server error

//DEFECT: Not sure what the body id refers to, passing in order id or energy id does not work returns 500 error
The update order endpoint: ENSEK/orders/{orderId} returns a 500 error, also we need to specify wqhat the ID field relates
to in the payload body.

//DEFECT: in the message purchase amount and remaining quantity are flipped
In the purchase update endpoint: "/ENSEK/buy/{id}/{quantity}" the message is incorrect, 
the purchase amount and remaining quantity are flipped, which results in a failure as I am checking the message against the
Get Energy endpoint.

//DEFECT: in the message is inconsistant for oil type than the other types messages.
In the purchase update endpoint: "/ENSEK/buy/{id}/{quantity}" the message is inconsistant, 
for Oil there isn't a space between the text orderid and for the rest of the fuels it is (apart from Nuclear as i havent't been able to purchase and cannot update)

//DEFECT: Purchase is allowed even if quantity is less than 0.
In the purchase update endpoint: "/ENSEK/buy/{id}/{quantity}" the endpoint allows you to buy energy if less than 0.
Nulear doesn't allow me to buy maybe due the quantity is 0 not less than 0 - needs investigation.
