namespace Backend.Features.Account;

public class AccountGroup : Group
{
    public AccountGroup()
    {
        Configure("account", ep => ep.Description(x => x.WithTags("Account")));
    }
}