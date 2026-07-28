namespace GameApi.Exceptions.UnauthorizedException
{
    public sealed class InvalidCredentialsException : UnauthorizedException
    {
        public InvalidCredentialsException()
            : base(
                "The username or password is incorrect.",
                "invalid_credentials")
        {
        }
    }
}
