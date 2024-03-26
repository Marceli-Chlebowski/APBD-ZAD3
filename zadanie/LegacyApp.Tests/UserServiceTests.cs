namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        //arrange
        var userService = new UserService();
        //act 
        Action action = () => userService.AddUser(
         null,
         "kowalski",
         "kowalski@gmail.com",
         DateTime.Parse("2000-01-01"),-
         100
        );
        //assert
        Assert.Throws<ArgumentException>(action);
    }
}