using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {

        string[] scenes = {
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/Gameover.unity",
            "Assets/Scenes/Settings.unity",
            "Assets/Scenes/howtoplay.unity",
        };

        string aabPath = "ChickenRun.aab";
        string apkPath = "ChickenRun.apk";

        string keystoreBase64 = "dXNpbmcgVW5pdHlFZGl0b3I7CnVzaW5nIFVuaXR5RWRpdG9yLkJ1aWxkLlJlcG9ydGluZzsKdXNpbmcgVW5pdHlFbmdpbmU7CnVzaW5nIFN5c3RlbTsKdXNpbmcgU3lzdGVtLklPOwoKcHVibGljIGNsYXNzIEJ1aWxkU2NyaXB0CnsKICAgIHB1YmxpYyBzdGF0aWMgdm9pZCBQZXJmb3JtQnVpbGQoKQogICAgewogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIC8vINCh0L/QuNGB0L7QuiDRgdGG0LXQvQogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIHN0cmluZ1tdIHNjZW5lcyA9IHsKICAgICAgICAgICAgIkFzc2V0cy9TY2VuZXMvbWVudS51bml0eSIsCiAgICAgICAgICAgICJBc3NldHMvU2NlbmVzL2dhbWVwbGF5LnVuaXR5IiwKICAgICAgICAgICAgIkFzc2V0cy9TY2VuZXMvaG93dG9wbGF5LnVuaXR5IiwKCiAgICAgICAgfTsKCiAgICAgICAgLy8gPT09PT09PT09PT09PT09PT09PT09PT09CiAgICAgICAgLy8g0J/Rg9GC0Lgg0Log0YTQsNC50LvQsNC8INGB0LHQvtGA0LrQuAogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIHN0cmluZyBhYWJQYXRoID0gIlBlbmd1aW5GaXNoZXJtYW4uYWFiIjsKICAgICAgICBzdHJpbmcgYXBrUGF0aCA9ICJQZW5ndWluRmlzaGVybWFuLmFwayI7CgogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIC8vINCd0LDRgdGC0YDQvtC50LrQsCBBbmRyb2lkIFNpZ25pbmcg0YfQtdGA0LXQtyDQv9C10YDQtdC80LXQvdC90YvQtSDQvtC60YDRg9C20LXQvdC40Y8KICAgICAgICAvLyA9PT09PT09PT09PT09PT09PT09PT09PT0KICAgICAgICBzdHJpbmcga2V5c3RvcmVCYXNlNjQgPSAiIgogICAgICAgIHN0cmluZyBrZXlzdG9yZVBhc3MgPSJzdHJvbmciOwogICAgICAgIHN0cmluZyBrZXlBbGlhcyA9InN0cm9uZyI7CiAgICAgICAgc3RyaW5nIGtleVBhc3MgPSJzdHJvbmciOwoKICAgICAgICBzdHJpbmcgdGVtcEtleXN0b3JlUGF0aCA9IG51bGw7CgogICAgICAgIGlmICghc3RyaW5nLklzTnVsbE9yRW1wdHkoa2V5c3RvcmVCYXNlNjQpKQogICAgICAgIHsKICAgICAgICAgICAgLy8g0KHQvtC30LTQsNGC0Ywg0LLRgNC10LzQtdC90L3Ri9C5INGE0LDQudC7IGtleXN0b3JlCiAgICAgICAgICAgIHRlbXBLZXlzdG9yZVBhdGggPSBQYXRoLkNvbWJpbmUoUGF0aC5HZXRUZW1wUGF0aCgpLCAiVGVtcEtleXN0b3JlLmprcyIpOwogICAgICAgICAgICBGaWxlLldyaXRlQWxsQnl0ZXModGVtcEtleXN0b3JlUGF0aCwgQ29udmVydC5Gcm9tQmFzZTY0U3RyaW5nKGtleXN0b3JlQmFzZTY0KSk7CgogICAgICAgICAgICBQbGF5ZXJTZXR0aW5ncy5BbmRyb2lkLnVzZUN1c3RvbUtleXN0b3JlID0gdHJ1ZTsKICAgICAgICAgICAgUGxheWVyU2V0dGluZ3MuQW5kcm9pZC5rZXlzdG9yZU5hbWUgPSB0ZW1wS2V5c3RvcmVQYXRoOwogICAgICAgICAgICBQbGF5ZXJTZXR0aW5ncy5BbmRyb2lkLmtleXN0b3JlUGFzcyA9IGtleXN0b3JlUGFzczsKICAgICAgICAgICAgUGxheWVyU2V0dGluZ3MuQW5kcm9pZC5rZXlhbGlhc05hbWUgPSBrZXlBbGlhczsKICAgICAgICAgICAgUGxheWVyU2V0dGluZ3MuQW5kcm9pZC5rZXlhbGlhc1Bhc3MgPSBrZXlQYXNzOwoKICAgICAgICAgICAgRGVidWcuTG9nKCJBbmRyb2lkIHNpZ25pbmcgY29uZmlndXJlZCBmcm9tIEJhc2U2NCBrZXlzdG9yZS4iKTsKICAgICAgICB9CiAgICAgICAgZWxzZQogICAgICAgIHsKICAgICAgICAgICAgRGVidWcuTG9nV2FybmluZygiS2V5c3RvcmUgQmFzZTY0IG5vdCBzZXQuIEFQSy9BQUIgd2lsbCBiZSB1bnNpZ25lZC4iKTsKICAgICAgICB9CgogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIC8vINCe0LHRidC40LUg0L/QsNGA0LDQvNC10YLRgNGLINGB0LHQvtGA0LrQuAogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIEJ1aWxkUGxheWVyT3B0aW9ucyBvcHRpb25zID0gbmV3IEJ1aWxkUGxheWVyT3B0aW9ucwogICAgICAgIHsKICAgICAgICAgICAgc2NlbmVzID0gc2NlbmVzLAogICAgICAgICAgICB0YXJnZXQgPSBCdWlsZFRhcmdldC5BbmRyb2lkLAogICAgICAgICAgICBvcHRpb25zID0gQnVpbGRPcHRpb25zLk5vbmUKICAgICAgICB9OwoKICAgICAgICAvLyA9PT09PT09PT09PT09PT09PT09PT09PT0KICAgICAgICAvLyAxLiDQodCx0L7RgNC60LAgQUFCCiAgICAgICAgLy8gPT09PT09PT09PT09PT09PT09PT09PT09CiAgICAgICAgRWRpdG9yVXNlckJ1aWxkU2V0dGluZ3MuYnVpbGRBcHBCdW5kbGUgPSB0cnVlOwogICAgICAgIG9wdGlvbnMubG9jYXRpb25QYXRoTmFtZSA9IGFhYlBhdGg7CgogICAgICAgIERlYnVnLkxvZygiPT09IFN0YXJ0aW5nIEFBQiBidWlsZCB0byAiICsgYWFiUGF0aCArICIgPT09Iik7CiAgICAgICAgQnVpbGRSZXBvcnQgcmVwb3J0QWFiID0gQnVpbGRQaXBlbGluZS5CdWlsZFBsYXllcihvcHRpb25zKTsKICAgICAgICBpZiAocmVwb3J0QWFiLnN1bW1hcnkucmVzdWx0ID09IEJ1aWxkUmVzdWx0LlN1Y2NlZWRlZCkKICAgICAgICAgICAgRGVidWcuTG9nKCJBQUIgYnVpbGQgc3VjY2VlZGVkISBGaWxlOiAiICsgYWFiUGF0aCk7CiAgICAgICAgZWxzZQogICAgICAgICAgICBEZWJ1Zy5Mb2dFcnJvcigiQUFCIGJ1aWxkIGZhaWxlZCEiKTsKCiAgICAgICAgLy8gPT09PT09PT09PT09PT09PT09PT09PT09CiAgICAgICAgLy8gMi4g0KHQsdC+0YDQutCwIEFQSwogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIEVkaXRvclVzZXJCdWlsZFNldHRpbmdzLmJ1aWxkQXBwQnVuZGxlID0gZmFsc2U7CiAgICAgICAgb3B0aW9ucy5sb2NhdGlvblBhdGhOYW1lID0gYXBrUGF0aDsKCiAgICAgICAgRGVidWcuTG9nKCI9PT0gU3RhcnRpbmcgQVBLIGJ1aWxkIHRvICIgKyBhcGtQYXRoICsgIiA9PT0iKTsKICAgICAgICBCdWlsZFJlcG9ydCByZXBvcnRBcGsgPSBCdWlsZFBpcGVsaW5lLkJ1aWxkUGxheWVyKG9wdGlvbnMpOwogICAgICAgIGlmIChyZXBvcnRBcGsuc3VtbWFyeS5yZXN1bHQgPT0gQnVpbGRSZXN1bHQuU3VjY2VlZGVkKQogICAgICAgICAgICBEZWJ1Zy5Mb2coIkFQSyBidWlsZCBzdWNjZWVkZWQhIEZpbGU6ICIgKyBhcGtQYXRoKTsKICAgICAgICBlbHNlCiAgICAgICAgICAgIERlYnVnLkxvZ0Vycm9yKCJBUEsgYnVpbGQgZmFpbGVkISIpOwoKICAgICAgICBEZWJ1Zy5Mb2coIj09PSBCdWlsZCBzY3JpcHQgZmluaXNoZWQgPT09Iik7CgogICAgICAgIC8vID09PT09PT09PT09PT09PT09PT09PT09PQogICAgICAgIC8vINCj0LTQsNC70LXQvdC40LUg0LLRgNC10LzQtdC90L3QvtCz0L4ga2V5c3RvcmUKICAgICAgICAvLyA9PT09PT09PT09PT09PT09PT09PT09PT0KICAgICAgICBpZiAoIXN0cmluZy5Jc051bGxPckVtcHR5KHRlbXBLZXlzdG9yZVBhdGgpICYmIEZpbGUuRXhpc3RzKHRlbXBLZXlzdG9yZVBhdGgpKQogICAgICAgIHsKICAgICAgICAgICAgRmlsZS5EZWxldGUodGVtcEtleXN0b3JlUGF0aCk7CiAgICAgICAgICAgIERlYnVnLkxvZygiVGVtcG9yYXJ5IGtleXN0b3JlIGRlbGV0ZWQuIik7CiAgICAgICAgfQogICAgfQp9Cg==";
        string keystorePass ="strong";
        string keyAlias ="strong";
        string keyPass ="strong";


        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
        {

            tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
            File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(keystoreBase64));

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = tempKeystorePath;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyPass;

            Debug.Log("Android signing configured from Base64 keystore.");
        }
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}