using Bogus;

namespace Services.Bogus.Fakers
{
    public abstract class BaseFaker<T> : Faker<T> where T : class
    {
        protected BaseFaker() : base("pl")
        {
            StrictMode(true);
        }
    }
}