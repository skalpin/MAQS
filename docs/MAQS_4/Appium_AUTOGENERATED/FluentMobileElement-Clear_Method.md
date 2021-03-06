# FluentMobileElement.Clear Method 
 

Clear the fluent element

**Namespace:**&nbsp;<a href="#/MAQS_4/Appium_AUTOGENERATED/Magenic-MaqsFramework-BaseAppiumTest_Namespace">Magenic.MaqsFramework.BaseAppiumTest</a><br />**Assembly:**&nbsp;Magenic.MaqsFramework.BaseAppiumTest (in Magenic.MaqsFramework.BaseAppiumTest.dll) Version: 4.0.4.0 (4.0.4)

## Syntax

**C#**<br />
``` C#
public void Clear()
```


## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Selenium)]
public void FluentElementClear()
{
    // Make sure we can set the value
    this.InputBox.SendKeys("test");
    Assert.AreEqual("test", this.InputBox.GetAttribute("value"));

    // Make sure the value is cleared
    this.InputBox.Clear();
    Assert.AreEqual(string.Empty, this.InputBox.GetAttribute("value"));
}
```


## See Also


#### Reference
<a href="#/MAQS_4/Appium_AUTOGENERATED/FluentMobileElement_Class">FluentMobileElement Class</a><br /><a href="#/MAQS_4/Appium_AUTOGENERATED/Magenic-MaqsFramework-BaseAppiumTest_Namespace">Magenic.MaqsFramework.BaseAppiumTest Namespace</a><br />