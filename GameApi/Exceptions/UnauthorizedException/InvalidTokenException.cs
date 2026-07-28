namespace GameApi.Exceptions.UnauthorizedException
{
    public sealed class InvalidTokenException : UnauthorizedException
    {
        public InvalidTokenException()
            : base(
                "The access token is invalid.",
                "invalid_token")
        {
        }
    }
}
