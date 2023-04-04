# LegacyApp

#Some steps

Introduce IClientRepository
Change UserDataAccess into UserRepository with interface (and not static) - or if change to UserDataAccess is not allowed then create a wrapper around it

Then split UserService.AddUser into smaller chunks:

- extract validator (IUserValidator)
- extract credit limit provider
-
- consider adding tests

Thinks to look out for

- fixinng the type-o in the UserService.AddUser to "firSTname" is a breaking change if named parameters are used by any client
- injecting the new services (repos, validators etc) from the constructor is a breacking change
- the validator uses the datetime.now which is not ok for testing
