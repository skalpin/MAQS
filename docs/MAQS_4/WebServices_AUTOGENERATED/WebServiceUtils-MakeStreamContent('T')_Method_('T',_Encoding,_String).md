# WebServiceUtils.MakeStreamContent(*T*) Method (*T*, Encoding, String)
 

Make http stream content

**Namespace:**&nbsp;<a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest</a><br />**Assembly:**&nbsp;Magenic.MaqsFramework.WebServiceTester (in Magenic.MaqsFramework.WebServiceTester.dll) Version: 4.0.4.0 (4.0.4)

## Syntax

**C#**<br />
``` C#
public static StreamContent MakeStreamContent<T>(
	T body,
	Encoding contentEncoding,
	string mediaType
)

```


#### Parameters
&nbsp;<dl><dt>body</dt><dd>Type: *T*<br />The content as a string</dd><dt>contentEncoding</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/86hf4sb8" target="_blank">System.Text.Encoding</a><br />How to encode the content</dd><dt>mediaType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The type of media</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>The body object type</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/hh138119" target="_blank">StreamContent</a><br />The stream content

## See Also


#### Reference
<a href="#/MAQS_4/WebServices_AUTOGENERATED/WebServiceUtils_Class">WebServiceUtils Class</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/WebServiceUtils-MakeStreamContent_Method">MakeStreamContent Overload</a><br /><a href="#/MAQS_4/WebServices_AUTOGENERATED/Magenic-MaqsFramework-BaseWebServiceTest_Namespace">Magenic.MaqsFramework.BaseWebServiceTest Namespace</a><br />