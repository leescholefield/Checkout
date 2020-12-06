# Checkout
An implementation of a Checkout Kata.

### Design Pattern

I went with a Service/Repository design pattern. Briefly, this pattern consists of two parts
- **Repositories** : this is an abstraction over our data-store. Each business model class (i.e. Item and Promotion) has an associated IRepository implementation. In a real app I would access this data from a SQL database, with seperate tables for Items and Promotions.
- **Services** : this is where our business logic lives. A Service retrieves the necessary data from a Repository and then performs any operations it needs to on it, so it can be presented to the user.

This design pattern provides several benefits; firstly, it decouples the business logic from data-access. This allows them to be changed independently and, when it comes to testing, to be swapped out completely. Secondly, if we change the data source all that would be required is writing a Repository implementation: it could be a local SQL-based database, data we retrieve from the network, etc. Lastly, if we add more business model classes we can just write a Repository to access them and provide it to whatever Service needs it.

The main drawback is it requires a lot of code, especially for a simple exercise like this. In a real application, however, the requirements are likely to be a great deal more complex, and this pattern gives us the versatility to change the system in the future if the need arises.

### Promotion model

To represent a Promotion I've created a simple abstract class containing the Item SKU it should be applied to, and the number of items that are required to activate it. Extending this are two additional classes: PercentagePromotion, which gives a percentage based discount, and SetPricePromotion, which offers a fixed price when you buy the required number of items. If I have the need to offer any different type of promotions in the future I can just create another child-class.

### Testing

I've used two types of testing for this project: Unit Testing, and Functional testing. In the Unit Tests I've used the Moq library to test each individual component separately. This means if a test fails I know the problem is with the class I'm actually testing. In the Functional tests I check that the whole system is working.
