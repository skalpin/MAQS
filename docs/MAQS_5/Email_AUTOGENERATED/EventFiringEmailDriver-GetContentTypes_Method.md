# EventFiringEmailDriver.GetContentTypes Method 
 

Get the list of content types for the given message

**Namespace:**&nbsp;<a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.BaseEmailTest (in Magenic.Maqs.BaseEmailTest.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public override List<string> GetContentTypes(
	MimeMessage message
)
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: MimeMessage<br />The message</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/6sh2ey19" target="_blank">List</a>(<a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a>)<br />List of content types

## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Email)]
public void GetTypes()
{
    this.EmailDriver.SelectMailbox("Test/SubTest");

    List<MimeMessage> messages = this.EmailDriver.SearchMessagesSince(new DateTime(2016, 3, 11), false);

    List<string> types = this.EmailDriver.GetContentTypes(messages[0]);

    Assert.IsTrue(types.Count == 2, "Expected 2 content types");
    Assert.IsTrue(types.Contains("text/plain"), "Expected 'text/plain' content types");
    Assert.IsTrue(types.Contains("text/html"), "Expected 'text/html' content types");
}
```


## See Also


#### Reference
<a href="#/MAQS_5/Email_AUTOGENERATED/EventFiringEmailDriver_Class">EventFiringEmailDriver Class</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest Namespace</a><br />