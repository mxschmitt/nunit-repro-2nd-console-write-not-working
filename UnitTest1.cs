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
	}

	[Test]
	public async Task TC2()
	{
		Page.Response += Listener;
		await Page.GotoAsync("https://example.com");
		Console.WriteLine($"Expected: 2 - Actual: {amountOfResponses}");
        Console.WriteLine("Why is the 2nd Listener() call not printed to the Console?");
	}

	private void Listener(object? sender, IResponse response)
	{
		Console.WriteLine($"Listener() - Test: {TestContext.CurrentContext.Test.Name} with URL: {response.Url}");
		amountOfResponses++;
	}
}