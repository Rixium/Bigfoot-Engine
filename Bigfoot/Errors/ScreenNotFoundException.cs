namespace Bigfoot.Errors
{
    class ScreenNotFoundException : YetiException
    {
        public ScreenNotFoundException() : base(YetiErrorLabel + "Screen does not exist.")
        {
        }
    }
}
