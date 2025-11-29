public class ExternalUserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }

    public ExternalUserAddressDto Address { get; set; }
}

public class ExternalUserAddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
}