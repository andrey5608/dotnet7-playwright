using Microsoft.Playwright;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Dotnet7Playwright
{
    [Parallelizable(ParallelScope.Self)]
    public class GoogleSearchTests : PageTest
    {
        [Test]
        public async Task SearchTestQueryAndCheckResultsList()
        {
            const string url = "https://google.com";
            const string queryFieldSelector = "[name='q']";
            const string resultSelector = "#search > div> #rso > .g";
            const string agreeButtonSelector = "button > div[role='none'] >> nth=-1";
            const string picPath = "pics/screenshot.png";
            const string query = "test query";

            // Arrange
            await Page.GotoAsync(url);
            await Page.Locator(agreeButtonSelector).ClickAsync();

            // Act
            await Page.FillAsync(queryFieldSelector, query);
            await Page.PressAsync(queryFieldSelector, "Enter");
            await Page.Locator($"{resultSelector} >> nth=0").WaitForAsync(); // wait for the first result
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = picPath });

            // Assert
            var results = await Page.QuerySelectorAllAsync(resultSelector);
            Assert.Greater(results.Count, 1);
        }
    }
}
