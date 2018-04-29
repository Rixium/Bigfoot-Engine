namespace Bigfoot.Errors
{
    class NoTextureException : YetiException
    {
        public NoTextureException() : base(YetiErrorLabel + "Texture is null")
        {

        }

    }
}
