using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTestsNUnit;

[TestFixture]
[Parallelizable(ParallelScope.Default)]
public class MyTest : PageTest
{
	static int amountOfResponses = 0;

	[Test]
	public async Task TC1()
	{
		Page.Response += Listener;
		await Page.GotoAsync("https://example.com");
		Console.WriteLine($"Expected: 1 - Actual: {amountOfResponses}");
  		Page.Response -= Listener; // unsubscribe or you are still active on the first listener when you are trying to fire the 2nd Listener
	}

	[Test]
	public async Task TC2()
	{
		Page.Response += Listener;
		await Page.GotoAsync("https://example.com");
		Console.WriteLine($"Expected: 2 - Actual: {amountOfResponses}");
  		Page.Response -= Listener; 
        Console.WriteLine("Why is the 2nd Listener() call not printed to the Console?");
	}

	private void Listener(object? sender, IResponse response)
	{
		Console.WriteLine($"Listener() - Test: {TestContext.CurrentContext.Test.Name} with URL: {response.Url}");
		amountOfResponses++;
	}
}
