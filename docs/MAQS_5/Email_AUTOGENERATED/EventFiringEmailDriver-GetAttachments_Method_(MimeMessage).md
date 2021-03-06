# EventFiringEmailDriver.GetAttachments Method (MimeMessage)
 

Get the list of attachments for the email with the given message

**Namespace:**&nbsp;<a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.BaseEmailTest (in Magenic.Maqs.BaseEmailTest.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public override List<MimeEntity> GetAttachments(
	MimeMessage message
)
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: MimeMessage<br />The message</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/6sh2ey19" target="_blank">List</a>(MimeEntity)<br />The list of attachments

## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Email)]
public void GetAttachments()
{
    string testFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestFiles");

    MimeMessage singleMessage = this.EmailDriver.GetMessage("Test/SubTest", "4");
    List<MimeEntity> attchments = this.EmailDriver.GetAttachments(singleMessage);

    // Make sure we have the correct number of attachments
    Assert.AreEqual(3, attchments.Count, "Expected 3 attachments");

    // Make sure the expected files are included
    foreach (MimePart attachment in attchments)
    {
        Assert.IsTrue(File.Exists(Path.Combine(testFilePath, attachment.FileName)), "Found extra file '" + attachment.FileName + "'");
    }
}
```


## See Also


#### Reference
<a href="#/MAQS_5/Email_AUTOGENERATED/EventFiringEmailDriver_Class">EventFiringEmailDriver Class</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/EventFiringEmailDriver-GetAttachments_Method">GetAttachments Overload</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest Namespace</a><br />