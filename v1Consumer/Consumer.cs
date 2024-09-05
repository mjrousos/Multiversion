namespace v1Consumer
{
    public static class Consumer
    {
        public static string Greet(string name) => Greeter.Greeter.Greet(name);
    }
}