using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace SeleniumAdvancedNov2020.Framework.BrowserInstance
{
    public class Browser
    {
        public static IWebDriver Instance { get; set; }

        public static void StartBrowser(BrowserTypes browserTypes = BrowserTypes.Chrome, string url = null)
        {
            switch (browserTypes)
            {
                case BrowserTypes.Chrome:
                    Instance = new ChromeDriver();
                    break;
                case BrowserTypes.RemoteWebdriver:
                    Instance = new RemoteWebDriver(new Uri(url), new ChromeOptions());
                    break;
            }
        }

        public static void SaveScreenShot(string className, string testMethodName)
        {
            string fileName = null;
            string screenShotPath = null;

            try
            {
                screenShotPath = @"C:\work\";
                var now = DateTime.Now;
                fileName = $"{className}_{testMethodName}_{now.Day}_{now.Month}_{now.Year}_{now.Hour}_{now.Second}_{now.Millisecond}.png";
                //Log.Info(fileName);
                var attemptNo = 0;
                var success = false;

                do
                {
                    success = TakeScreenShot($"{screenShotPath}{fileName}");
                    attemptNo++;
                    //Log.Info($"Saving file: {fileName} attempt {attemptNo}");
                } while (!success && attemptNo < 5);


            }
            catch (Exception e)
            {
                //Log.Error(e);
            }
        }

        private static bool TakeScreenShot(string fileName)
        {
            try
            {
                var screenShot = ((ITakesScreenshot)Instance).GetScreenshot();
                screenShot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                return true;
            }
            catch (Exception e)
            {
                //Log.Error($"Error when saving screenshot '{fileName}'", e);
            }

            return false;
        }
    }
}
