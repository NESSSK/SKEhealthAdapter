md binary\CC
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:binary\CC\SSLKlient.exe src\IeHealthSyncService.cs src\StreamExtensions.cs src\Program.cs src\CallHelper.cs /reference:External\Microsoft.IdentityModel.dll /reference:External\CC\Iam.Client.CryptoController.dll /reference:System.IdentityModel.dll /resource:src/services_preprod_npz_sk.cer,SSLKlient.services_preprod_npz_sk.cer /define:CC
Xcopy external\*.dll  binary\CC /Y
Xcopy external\CC\*.dll  binary\CC /Y
ECHO F|Xcopy src\app.config binary\CC\SSLKlient.exe.config /Y