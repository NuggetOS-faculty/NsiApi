namespace NSIProject.Application.Common.Dto.User;

public record UserDetailsDto(string Id, string Username, string Email, string FirstName, string LastName,
    string PhoneNumber);