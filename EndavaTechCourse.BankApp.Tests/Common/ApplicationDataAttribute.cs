namespace EndavaTechCourse.BankApp.Tests.Common
{
    public class ApplicationDataAttribute : InlineAutoDataAttribute
    {
        public ApplicationDataAttribute(params object[] arguments)
            : base(() =>
        {
            var fixture = new Fixture();

            // Get existing ThrowingRecursionBehaviors and remove them
            var throwingRecursionBehaviors = fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList();
            foreach (var behavior in throwingRecursionBehaviors)
            {
                fixture.Behaviors.Remove(behavior);
            }

            // Add OmitOnRecursionBehavior
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // Customize the fixture
            fixture.Customize(new CompositeCustomization(
                new AutoNSubstituteCustomization(),
                new SqliteCustomization
                {
                    AutoOpenConnection = true,
                    OmitDbSets = true,
                    OnCreate = OnCreateAction.EnsureCreated
                }));

            return fixture;
        }, arguments)
        {
        }
    }
}

