The controller has a service that's injected to communicate with the Repository.

The repo it's only a Dictionary with the nick of the user as a key. (I thought about it like a videogame, a player's nick can't be changed, but any other info can)

The repo is agnostic about any errors that the operations may cause.

UserManagementService will handle any exception that the repo might throw.

The user model knows how to validate itself and will throw custom exceptions if the input is not propperly formatted.

UserManagementService has logging to track opperations and errors.

UserManagementService.ValidateUser was extracted to not clutter the other methods with catches, but delegates the validation of the user to itself, to keep single responsibility.
