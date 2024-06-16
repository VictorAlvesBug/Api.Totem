namespace Api.Totem.Helpers.DataAnnotations.Attributes
{
	public sealed class WhenRequiredErrorMessageAttribute : Attribute
	{
        public string ErrorMessage { get; }

        public WhenRequiredErrorMessageAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
