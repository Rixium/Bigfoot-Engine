namespace Bigfoot.Errors
{
    internal class DrawErrorException : YetiException
    {
        public DrawErrorException() : base(YetiErrorLabel + "Draw not began.")
        {
        }

    }
}
