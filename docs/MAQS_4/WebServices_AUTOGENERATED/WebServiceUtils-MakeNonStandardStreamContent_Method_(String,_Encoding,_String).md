# WebServiceUtils.MakeNonStandardStreamContent Method (String, Encoding, String)
 

Make non-standard http stream content with string body

**Namespace:**&nbsp;<a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.MaqsFramework.WebServiceTester (in Magenic.MaqsFramework.WebServiceTester.dll) Version: 4.0.4.0 (4.0.4)

## Syntax

**C#**<br />
``` C#
public static StreamContent MakeNonStandardStreamContent(
	string body,
	Encoding contentEncoding,
	string mediaType
)
```


#### Parameters
&nbsp;<dl><dt>body</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The content as a string</dd><dt>contentEncoding</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/86hf4sb8" target="_blank">System.Text.Encoding</a><br />How to encode the content</dd><dt>mediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The content type of media, will add Content-Type header</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/hh138119" target="_blank">StreamContent</a><br />The stream content

## Examples

**C#**<br />
``` C#
/// <summary>
/// Verify the string status code
/// </summary>
[TestMethod]
[TestCategory(TestCategories.WebService)]
public void MakeNonStandardStreamContentStringTest()
{
    ////Override stuff
    Func<Uri, string, HttpClient> setupClientConnection = (uri, s) =>
    {
        HttpClient client = this.TestObject.WebServiceWrapper.BaseHttpClient;
        client.BaseAddress = new Uri(url);
        client.DefaultRequestHeaders.Accept.Clear();

        return client;
    };

    this.TestObject.WebServiceWrapper.OverrideSetupClientConnection(setupClientConnection);

    //// Non Standard Content Type stuff
    var randomData = Guid.NewGuid();
    var randomData2 = Guid.NewGuid();
    string formDataBoundary = $"----------{randomData}";

    MultipartFormDataContent multiPartContent = new MultipartFormDataContent(formDataBoundary);

    //// Method to Test
    var content = WebServiceUtils.MakeNonStandardStreamContent(randomData.ToString(), Encoding.ASCII, "multipart/form-data");
    var content2 = WebServiceUtils.MakeNonStandardStreamContent(randomData2.ToString(), Encoding.ASCII, "multipart/form-data");

    multiPartContent.Add(content, "MyResume", "Resume.abc");
    multiPartContent.Add(content2, "MyDefintion", "MyDefintion.def");

    var result = this.TestObject.WebServiceWrapper.Post<FilesUploaded>("api/upload", "application/json", multiPartContent, true);

    var file1 = result.Files.FirstOrDefault();
    var file2 = result.Files.LastOrDefault();

    Assert.IsNotNull(file1);
    Assert.IsNotNull(file2);

    Assert.AreEqual("Resume.abc", file1.FileName, $"File uploaded did not match 'Resume.abc'. Actual is '{file1.FileName}'");
    Assert.AreEqual("MyDefintion.def", file2.FileName, $"File uploaded did not match 'MyDefintion.def'. Actual is '{file2.FileName}'");

    Assert.AreEqual("MyResume", file1.ContentName, $"File uploaded did not match 'MyResume'. Actual is '{file1.ContentName}'");
    Assert.AreEqual("MyDefintion", file2.ContentName, $"File uploaded did not match 'MyDefintion'. Actual is '{file2.ContentName}'");
}
```


## See Also


#### Reference
<a href="#/MAQS_4/WebServices_AUTOGENERATED/WebServiceUtils_Class">WebServiceUtils Class</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/WebServiceUtils-MakeNonStandardStreamContent_Method">MakeNonStandardStreamContent Overload</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest Namespace</a><br />