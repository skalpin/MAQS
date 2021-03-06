# EventFiringEmailDriver.MoveMailMessage Method (String, String)
 

Move the email with the given unique identifier to the destination mailbox

**Namespace:**&nbsp;<a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.BaseEmailTest (in Magenic.Maqs.BaseEmailTest.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public override void MoveMailMessage(
	string uid,
	string destinationMailbox
)
```


#### Parameters
&nbsp;<dl><dt>uid</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The unique identifier for the email</dd><dt>destinationMailbox</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The destination mailbox</dd></dl>

## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Email)]
public void MoveMessage()
{
    // Test is ignored for CI test run
    string uniqueSubject = Guid.NewGuid().ToString();
    this.SendTestEmail(uniqueSubject);

    GenericWait.Wait<bool, string>(this.IsEmailThere, new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 30), uniqueSubject);
    Thread.Sleep(1000);

    List<MimeMessage> messageHeaders = this.EmailDriver.GetAllMessageHeaders();

    foreach (MimeMessage message in messageHeaders)
    {
        Thread.Sleep(1000);
        if (message.Subject.Equals(uniqueSubject))
        {
            // Move by message
            this.EmailDriver.MoveMailMessage(message, "Test");

            break;
        }
    }

    if (!GenericWait.WaitUntil<string>(this.HasBeenRemoved, uniqueSubject))
    {
        Assert.Fail("Message " + uniqueSubject + " was not removed by message");
    }

    Thread.Sleep(1000);
    this.EmailDriver.SelectMailbox("Test");
    messageHeaders = this.EmailDriver.GetAllMessageHeaders();

    foreach (MimeMessage message in messageHeaders)
    {
        if (message.Subject.Equals(uniqueSubject))
        {
            // Move by unique identifier
            this.EmailDriver.MoveMailMessage(message, "AA");
            break;
        }
    }

    if (!GenericWait.WaitUntil<string>(this.HasBeenRemoved, uniqueSubject))
    {
        Assert.Fail("Message " + uniqueSubject + " was not removed by uid");
    }

    Thread.Sleep(100);
    this.EmailDriver.SelectMailbox("AA");
    messageHeaders = this.EmailDriver.GetAllMessageHeaders();

    foreach (MimeMessage message in messageHeaders)
    {
        if (message.Subject.Equals(uniqueSubject))
        {
            this.EmailDriver.DeleteMessage(message);
            return;
        }
    }

    Assert.Fail("Message " + uniqueSubject + " was moved to new folder");
}
```


## See Also


#### Reference
<a href="#/MAQS_5/Email_AUTOGENERATED/EventFiringEmailDriver_Class">EventFiringEmailDriver Class</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/EventFiringEmailDriver-MoveMailMessage_Method">MoveMailMessage Overload</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest Namespace</a><br />