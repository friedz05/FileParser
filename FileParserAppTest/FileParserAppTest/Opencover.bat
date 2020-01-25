"%~dp0..\packages\OpenCover.4.7.922\OpenCover.Console.exe" ^
-register:user ^
-target:"%C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\mstest.exe" ^
-targetargs:"/testcontainer:\"C:\Users\Echo\OneDrive\Documents\repo\fileparser\FileParserAppTest\FileParserAppTest\bin\Debug\netcoreapp3.1\FileParserAppTest.dll\" /resultsfile:\"FileParserTestResults.trx\"" ^
-filter:"-[FileParserAppTest]*
-mergebyhash ^
-skipautoprops ^
-output:"%~dp0\GeneratedReports\FileParserTestResults.xml"