#if UNITY_IOS
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class PostProcessCameraPlist
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS) return;

        string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        PlistElementDict rootDict = plist.root;

        // Добавляем описание для использования камеры
        const string cameraUsageKey = "NSCameraUsageDescription";
        const string cameraUsageDescription = "This app requires access to the camera to capture photos or video.";

        if (!rootDict.values.ContainsKey(cameraUsageKey))
        {
            rootDict.SetString(cameraUsageKey, cameraUsageDescription);
        }

        // Сохраняем изменения
        File.WriteAllText(plistPath, plist.WriteToString());
    }
}
#endif