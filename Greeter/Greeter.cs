namespace Greeter
{
    public static class Greeter
    {
        public static string Greet(string name)
        {
            return $"Hello{(string.IsNullOrEmpty(name) ? string.Empty : ", " + name)}, from Greeter v2.0!";
        }
    }
}
