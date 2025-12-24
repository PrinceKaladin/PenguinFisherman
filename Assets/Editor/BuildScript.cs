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
            "Assets/Scenes/howtoplay.unity",
        };

        string aabPath = "PenguinFisherman.aab";
        string apkPath = "PenguinFisherman.apk";

        string keystoreBase64 = "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";
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
