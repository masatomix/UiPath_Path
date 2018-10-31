This library is a custom activity for UiPath.

When using UiPath, I often want to call methods of .NET, such as getting the name of the current directory, base64 encode the file,These activities are the activities that call those methods.
It provides the following functions.

- [Combine](#combine-activity): Combines an array of strings into a path.
- [Current Dir](#current-dir-activity): Gets the current working directory.
- [Path Utils](#path-utils-activity): Returns the absolute path for the specified path string ,checks the existence of the file ,etc.
- [Base64 Encode](#base64-encode-activity): Base64 Encode.
- [Base64 Decode](#base64-decode-activity): Base64 Decode.
- [Base64 Encode From File](#base64-encode-from-file-activity): Base64 Encode from File
- [ConvertCRLF](#convert-crlf-activity): Convert the line feed code of the given text.
- [ToJSONString](#to-json-string-activity): An activity that converts arbitrary objects to JSON strings.

## Path Utils
### Combine Activity
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/53dddd49-a08f-e13c-0703-71c24d42aa4b.png)

 Combines an array of strings into a path. That is as follows:

```
{"c:\temp","hogehoge"} → c:\temp\hogehoge
```

calls `System.IO.Path.Combine` method . And calls `String.Replace("/", "\\")`  method to use '/' .

### Current Dir Activity

![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/1ce599fd-4d0c-8aed-57ad-f3f5b2a43e36.png)

Gets the current working directory. only calls `Directory.GetCurrentDirectory` method.


### Path Utils Activity
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/09863a71-fee7-1a84-2c29-24d947e5d206.png)

Specify relative/absolute path and get the following:

|Properties|Description|
|-----------------|------------------|
| DirectoryName   |Basically it returns the same as `Path.GetDirectoryName()` ※.|
| DirExists       |returns true if the specified Directory exists (returns false even if it exists if it is a File).|
| FileExists      |returns true if the specified File exists (returns false even if it exists if it is a Directory).|
| FullPath        |returns full Path.|

※ Basically it returns the same as `Path.GetDirectoryName()`. If an existing directory is specified, even if there is no "\\" at the end, the directory name is returned.


## String Utils

### Base64 Encode Activity

![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/bf3b7468-8f40-513d-dc55-bd57f110c896.png)

Converts a String to its equivalent string representation that is encoded with base-64 digits.
That is as follows:

```C#
 var result = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(text));
```


### Base64 Decode Activity
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/d7803b16-36c8-6feb-79cf-f6ab567d6b83.png)

Converts the specified string, which encodes binary data as base-64 digits.
That is as follows:

```C#
var resut = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(text));
```



### Base64 Encode From File Activity
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/3748c0a0-dbe6-3bdf-fce4-0cd1eaec1543.png)

Converts the String From a file to its equivalent string representation that is encoded with base-64 digits.
That is as follows:

```C#
var path = context.GetValue(this.Path);
var bytes = ReadFile(path);
var result = Convert.ToBase64String(bytes);

public byte[] ReadFile(string filePath)
{
    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    {
        var buffer = new byte[fs.Length];
        fs.Read(buffer, 0, buffer.Length);
        return buffer;
    }
}

```


### Convert CRLF Activity
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/caefc700-fad9-1f43-7d87-78d3449dbb03.png)


Convert the line feed code of the given text.
Regardless of whether the line feed code of data is LR / LF / CR + LF,It outputs with a line feed code with a check in "Set line feed code".
It first converts to CR / CR + LF → LF, and then converts LF to the specified line feed code.
That is as follows:

```C#
 // It first converts to CR / CR + LF → LF
 trimData = trimData.Replace("\r\n", "\n");
 trimData = trimData.Replace("\r", "\n");

 if (CR) {
     trimData = trimData.Replace("\n", "\r\n");
 } else {
     // do nothing
 }

 if (LF) {
     // do nothing
 } else {
     trimData = trimData.Replace("\n", "");
 }
```


### To JSON String Activity
An activity that converts arbitrary objects to JSON strings.
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/e223b3e9-c4eb-b630-380c-8039e3789593.png)

For example,when you generate and pass the Dictionary object, the Key / Value is output as a JSON string.
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/3eff4cb6-5ac8-8c8d-932e-04b66eefbadb.png)

That is as follows:

```C#
String jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented);
```
