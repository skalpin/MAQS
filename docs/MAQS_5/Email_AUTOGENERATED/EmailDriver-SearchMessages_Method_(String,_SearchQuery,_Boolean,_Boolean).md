# EmailDriver.SearchMessages Method (String, SearchQuery, Boolean, Boolean)
 

Get a list of messages that meet the search criteria

**Namespace:**&nbsp;<a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest</a><br />**Assembly:**&nbsp;Magenic.Maqs.BaseEmailTest (in Magenic.Maqs.BaseEmailTest.dll) Version: 5.3.0

## Syntax

**C#**<br />
``` C#
public virtual List<MimeMessage> SearchMessages(
	string mailBox,
	SearchQuery condition,
	bool headersOnly = true,
	bool markRead = false
)
```


#### Parameters
&nbsp;<dl><dt>mailBox</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The mailbox</dd><dt>condition</dt><dd>Type: SearchQuery<br />The search condition</dd><dt>headersOnly (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Only get header data</dd><dt>markRead (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Mark the email as read</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/6sh2ey19" target="_blank">List</a>(MimeMessage)<br />The list of messages that match the search criteria

## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Email)]
public void GetNoMessagesWithCompoundCondition()
{
    this.EmailDriver.SelectMailbox("Test/SubTest");

    SearchQuery condition = SearchQuery.NotSeen.And(SearchQuery.SubjectContains("RTF"));

    List<MimeMessage> messages = this.EmailDriver.SearchMessages(condition);
    Assert.AreEqual(messages.Count, 0, "Expected 0 message in 'Test/SubTest' between the given dates but found " + messages.Count);
}
```

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.Email)]
public void GetMessagesHeadersSince()
{
    this.EmailDriver.SelectMailbox("Test/SubTest");

    List<MimeMessage> messages = this.EmailDriver.SearchMessagesSince(new DateTime(2016, 3, 11));
    Assert.AreEqual(messages.Count, 1, "Expected 1 message in 'Test/SubTest' after the given date but found " + messages.Count);
    Assert.IsNull(messages[0].Body, "Expected the message header only, not the entire message");
}
```


## See Also


#### Reference
<a href="#/MAQS_5/Email_AUTOGENERATED/EmailDriver_Class">EmailDriver Class</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/EmailDriver-SearchMessages_Method">SearchMessages Overload</a><br /><a href="#/MAQS_5/Email_AUTOGENERATED/Magenic-Maqs-BaseEmailTest_Namespace">Magenic.Maqs.BaseEmailTest Namespace</a><br />