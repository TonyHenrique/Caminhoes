using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;


public static class TestUtilscs
{
    public static void HomeAndSendKeys(this IWebElement webElement, string text)
    {
        webElement.SendKeys(Keys.Home);
        webElement.SendKeys(text);
    }
    public static void SelectAllAndSendKeys(this IWebElement webElement, string text)
    {
        webElement.SendKeys(Keys.LeftControl + "a");
        webElement.SendKeys(text);
    }
}
