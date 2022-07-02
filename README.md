# TripinApi.Console

This project uses [Unchase](!https://github.com/unchase/Unchase.Odata.Connectedservice) to generate a few utility methods to access the TripPin OData services

- Structure
TripPinApi.Console: The interactive console project
TripPinApi.Services: Contains the People service used to encapsulate the logic to access the OData api.

- How to use the application (Guide)
1 - Run the application
2 - Press 1 to list all people, 
3 - Type the number of person shown in the first column `Number` and press `Enter` (for instance type 1) This will display the details of the person
4 - Type 2 to perform a contains search of a list white space separated list of first name, middle name and last name (Case sensitive search)
5- Type 3 to search a person using a specific username

- Thoughts
The TripPinApi.Services could be extended to contain more services like the TripService, The friendService, etc. It could probably be used to unit test but unit testing has not been yet implemented