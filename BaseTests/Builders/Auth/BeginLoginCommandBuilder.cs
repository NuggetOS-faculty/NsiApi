using NSIProject.Application.Commands.Auth;

namespace BaseTests.Builders.Auth;

public class BeginLoginCommandBuilder
{
    private string _email = "";

    public BeginLoginCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public BeginLoginCommand Build()
    {
        return new BeginLoginCommand(_email);
    }
}