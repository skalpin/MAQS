# HttpClientWrapper.Patch Method (String, String, String, Encoding, String, Boolean, Boolean)
 

Execute a web service patch

**Namespace:**&nbsp;<a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.MaqsFramework.WebServiceTester (in Magenic.MaqsFramework.WebServiceTester.dll) Version: 4.0.4.0 (4.0.4)

## Syntax

**C#**<br />
``` C#
public string Patch(
	string requestUri,
	string expectedMediaType,
	string content,
	Encoding contentEncoding,
	string postMediaType,
	bool contentAsString = true,
	bool expectSuccess = true
)
```


#### Parameters
&nbsp;<dl><dt>requestUri</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The request uri</dd><dt>expectedMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The type of media being requested</dd><dt>content</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The put content</dd><dt>contentEncoding</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/86hf4sb8" target="_blank">System.Text.Encoding</a><br />How to encode the put content</dd><dt>postMediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The type of the media being put</dd><dt>contentAsString (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />If true pass content as StringContent, else pass as StreamContent</dd><dt>expectSuccess (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Assert a success code was returned</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a><br />The response body as a string

## Examples

**C#**<br />
``` C#
[TestMethod]
[TestCategory(TestCategories.WebService)]
public void PatchStreamWithoutMakeContent()
{
    var result = this.WebServiceWrapper.Patch("/api/String/Patch/1", "text/plain", "Test", Encoding.UTF8, "text/plain", false, true);
    Assert.AreEqual("\"Patched\"", result);
}
```


## See Also


#### Reference
<a href="#/MAQS_4/WebServices_AUTOGENERATED/HttpClientWrapper_Class">HttpClientWrapper Class</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/HttpClientWrapper-Patch_Method">Patch Overload</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest Namespace</a><br />