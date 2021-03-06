このライブラリは、UiPath向けのカスタムアクティビティです。

UiPathをやっていると、今いるディレクトリ名を取得したり、ファイルをBase64 Encodeしたりなど、.NETのメソッドを呼びたいことがちょくちょくあるのですが、それらを呼び出すアクティビティになっています。

具体的には以下の機能を提供します。

- [Combine](#combine-アクティビティ): パスを連結します
- [Current Dir](#current-dir-アクティビティ): 現在のディレクトリを返します
- [Path Utils](#path-utils-アクティビティ): ファイルの存在チェックなどを行います
- [Base64 Encode](#base64-encode-アクティビティ): Base64 Encodeします
- [Base64 Decode](#base64-decode-アクティビティ): Base64 Decodeします
- [Base64 Encode From File](#base64-encode-from-file-アクティビティ): ファイルをBase64 Encodeします
- [ConvertCRLF](#convert-crlf-アクティビティ): 改行コードを変換します
- [ToJSONString](#to-json-string-アクティビティ): 任意のオブジェクトをJSON文字列化します

## Path Utils
### Combine アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/1a2d1f2a-1363-8c3b-8f04-88c1fad673d7.png)

指定されたパスの配列を連結して、パスを生成します。いわゆる

```
{"c:¥temp","hogehoge"} → c:¥temp¥hogehoge
{"temp","hogehoge"} → temp\hogehoge
{"te/mp","hoge\hoge"} → te\mp\hoge\hoge
```

のような処理です。ふつうに文字列連結をすると `c:¥temphogehoge`などになりますが .NETの `System.IO.Path.Combine`  を呼び出して処理しています。ついでに、スラッシュ(/)もディレクトリの区切りとして処理したかったので、`String.Replace("/", "\\")` も入れてます。


### Current Dir アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/69f66769-0dba-a6f2-82e1-1487bc2812ba.png)

現在のディレクトリのフルパスを返すアクティビティです。ただただ `Directory.GetCurrentDirectory`をしているだけ。

### Path Utils アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/ce895d06-8232-335c-efbb-9fbea652b179.png)

相対パス・絶対パスを指定して、以下を取得するアクティビティです。

|項目名|内容|
|-----------------|------------------|
| DirectoryPath   | 原則 Path.GetDirectoryName() とおなじものを返します※|
| DirExists       |指定したディレクトリが存在すればTrue(ファイルだったら存在してもFalse)|
| FileExists      |指定したファイルが存在すればTrue(ディレクトリだったら存在してもFalse)|
| FullPath        |フルパスが返ります|

※ 基本、`Path.GetDirectoryName()` とおなじものを返しますが、存在するディレクトリを指定したときは末尾に"¥"がなくても、そのディレクトリパスを返します。 `Path.GetDirectoryName()`は末尾が"¥"で終わってないと、その上のディレクトリパスを返してしまうようですが。

例:

```
Path: c:/temp/existsDir/
→
FullPath: c:\temp\existsDir\
DirectoryPath: c:\temp\existsDir


Path: c:/temp/existsDir
→
FullPath: c:\temp\existsDir
DirectoryPath: c:\temp\existsDir  ←.NETのメソッドは、c:\temp\ を返しますが、。


Path: c:/temp/notExistsDir/
→
FullPath: c:\temp\notExistsDir\
DirectoryPath: c:\temp\notExistsDir


Path: c:/temp/notExistsDir
→
FullPath: c:\temp\notExistsDir
DirectoryPath: c:\temp


Path: c:/temp/existsOrNotExistsFile.txt
→
FullPath: c:\temp\existsOrNotExistsFile.txt
DirectoryPath: c:\temp
```

## String Utils

### Base64 Encode アクティビティ

![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/bf3b7468-8f40-513d-dc55-bd57f110c896.png)

文字列をUTF-8とみなしBase64 Encodeします。

処理的には

```C#
 var result = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(text));
```

です。


### Base64 Decode アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/d7803b16-36c8-6feb-79cf-f6ab567d6b83.png)

Base64 Encodeされた文字列をDecodeし、UTF-8で文字列にします。

処理的には

```C#
var resut = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(text));
```

です。


### Base64 Encode From File アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/3748c0a0-dbe6-3bdf-fce4-0cd1eaec1543.png)

ファイルをバイナリで読取り、Base64 Encodeします。

処理的には

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
こんな感じ。

### Convert CRLF アクティビティ
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/caefc700-fad9-1f43-7d87-78d3449dbb03.png)

与えられたテキストの改行コードを変換します。入力されたデータの改行コードが CR/LF/CR+LF どれであるかに関わらず「セットする改行コード」にチェックを入れた改行コードで出力します。処理的には、いったん CR/CR+LF → LF へ変換し、そのあとLFを指定された改行コードに変換しています。


```C#
 // まず、LFだけにしてしまう
 trimData = trimData.Replace("\r\n", "\n");
 trimData = trimData.Replace("\r", "\n");

 if (CR) {
     trimData = trimData.Replace("\n", "\r\n");
 } else {
     // CRはすでに除去済みなのでなにもしない
 }

 if (LF) {
     // LFはすでに付与済みなのでなにもしない
 } else {
     trimData = trimData.Replace("\n", "");
 }
```

こんな感じ。

### To JSON String アクティビティ
任意のオブジェクトをJSON文字列化するアクティビティです。
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/e223b3e9-c4eb-b630-380c-8039e3789593.png)

例としてはDictionaryオブジェクトを生成して渡してみると、Key/ValueがJSON文字列として出力されています。
![image.png](https://qiita-image-store.s3.amazonaws.com/0/73777/3eff4cb6-5ac8-8c8d-932e-04b66eefbadb.png)


処理的には

```C#
String jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented);
```

です。
