using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service;
using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class UserServiceTests
{
    private readonly IUserRepository _mockUserRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_mockUserRepository);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnListOfUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "User1", Password = "Password1", Role = "Admin" },
            new User { Id = 2, Username = "User2", Password = "Password2", Role = "User" }
        };
        _mockUserRepository.GetUsers().Returns(users);

        // Act
        var result = await _userService.GetAllUsers();

        // Assert
        result.Should().HaveCount(2);
        result.First().Username.Should().Be("User1");
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "User1", Password = "Password1", Role = "Admin" };
        _mockUserRepository.GetUserByID(1).Returns(user);

        // Act
        var result = await _userService.GetUserById(1);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("User1");
    }

    [Fact]
    public async Task AddUser_ShouldReturnNewUser()
    {
        // Arrange
        var userDto = new UserDTO { Username = "User1", Password = "Password1", Role = "Admin" };
        var user = new User { Id = 1, Username = "User1", Password = "Password1", Role = "Admin" };
        _mockUserRepository.InsertUser(Arg.Any<User>()).Returns(user);

        // Act
        var result = await _userService.AddUser(userDto);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Username.Should().Be("User1");
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnUpdatedUser()
    {
        // Arrange
        var userDto = new UserDTO { Id = 1, Username = "UpdatedUser", Password = "UpdatedPassword", Role = "Admin" };
        var user = new User { Id = 1, Username = "UpdatedUser", Password = "UpdatedPassword", Role = "Admin" };
        _mockUserRepository.UpdateUser(Arg.Any<User>()).Returns(user);

        // Act
        var result = await _userService.UpdateUser(userDto);

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("UpdatedUser");
    }

    [Fact]
    public async Task DeleteUser_ShouldCallRepository()
    {
        // Arrange
        var userId = 1;

        // Act
        await _userService.DeleteUser(userId);

        // Assert
        await _mockUserRepository.Received(1).DeleteUser(userId);
    }

    [Fact]
    public async Task GetUserByUsername_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "User1", Password = "Password1", Role = "Admin" };
        _mockUserRepository.GetUserByUsername("User1").Returns(user);

        // Act
        var result = await _userService.GetUserByUsername("User1");

        // Assert
        result.Should().NotBeNull();
        result.Username.Should().Be("User1");
    }
}
