using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileHandlerUtility : MonoBehaviour
{
    public static void SaveToJSON<T>(List<T> listData, string filename)
    {
        Debug.Log(GetTextResourcePath(filename));
        string content = JsonSerializerUtility.ToJson<T>(listData.ToArray());
        WriteFile(GetTextResourcePath(filename), content);
    }

    public static List<T> ReadListFromJSON<T>(string filename)
    {
        var jsonTextFile = Resources.Load<TextAsset>($"Text/{filename}");

        if (string.IsNullOrEmpty(jsonTextFile.ToString()) || jsonTextFile.ToString() == "{}")
        {
            return new List<T>();
        }

        return JsonSerializerUtility.FromJson<T>(jsonTextFile.ToString()).ToList();
    }

    private static string GetTextResourcePath(string filename)
    {
        return Application.dataPath + "/Resources/Text/" + filename + ".json";
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }
}
