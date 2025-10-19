#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Text;

public class Utf8OnCreate : AssetModificationProcessor
{
    public static void OnWillCreateAsset(string assetPath)
    {
        assetPath = assetPath.Replace(".meta", "");
        if (!assetPath.EndsWith(".cs")) return;

        var fullPath = Path.GetFullPath(assetPath);
        if (!File.Exists(fullPath)) return;

        byte[] bytes = File.ReadAllBytes(fullPath);

        if (HasUtf8Bom(bytes)) return;

        if (TryGetUtf8Text(bytes, out string utf8Text))
        {
            File.WriteAllText(fullPath, utf8Text, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
            return;
        }

        string ansiText = Encoding.Default.GetString(bytes);
        File.WriteAllText(fullPath, ansiText, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }

    static bool HasUtf8Bom(byte[] b) =>
        b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF;

    static bool TryGetUtf8Text(byte[] bytes, out string text)
    {
        try
        {
            var utf8Strict = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            int offset = HasUtf8Bom(bytes) ? 3 : 0;
            text = utf8Strict.GetString(bytes, offset, bytes.Length - offset);
            return true;
        }
        catch
        {
            text = null;
            return false;
        }
    }
}
#endif