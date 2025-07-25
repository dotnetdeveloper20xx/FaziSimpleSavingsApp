
using FaziSimpleSavings.Core.Entities;

namespace FaziSimpleSavings.Test.TestUtilities;

public static class TestUserFactory
{
    public static User Create(
        string firstName = "Test",
        string lastName = "User",
        string email = "test@example.com")
    {
        return new User(firstName, lastName, email);
    }
}
