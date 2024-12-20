using Entities.Users;

namespace Services.Controllers.OperationResults;

public abstract record CreationUserResult
{
    public sealed record Success(User CreatedUser) : CreationUserResult;

    public sealed record UserWithThisNameExist : CreationUserResult;
}