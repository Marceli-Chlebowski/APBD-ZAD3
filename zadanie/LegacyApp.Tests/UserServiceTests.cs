namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        //arrange
        var userService = new UserService();
        //act 
        var result = userService.AddUser(
            null,
            "kowalski",
            "kowalski@gmail.com",
            DateTime.Parse("2000-01-01"),
            1
        );
        //assert
        Assert.False(result);
        Assert.False(result);
    } 
}