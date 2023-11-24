using System.IO;
using UnityEditor;
using UnityEngine;

public class DebugLogs : MonoBehaviour
{
    [SerializeField] TextAsset logfile;
    [SerializeField] string pathofFile, writePath;
    [SerializeField, TextArea(10, 30)] string LOGS;
    public static DebugLogs Ins { get; private set; } //singelton

    private void Awake()
    {
        #region singelton
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        PathCreator();
    }

    void PathCreator()
    {
        pathofFile = Application.persistentDataPath + "/_prefabs/";
#if UNITY_EDITOR
        pathofFile = Application.dataPath + "/_prefabs/";
#endif
        if (!Directory.Exists(pathofFile)) Directory.CreateDirectory(pathofFile);
        writePath = pathofFile + "logs.txt";
        if (!File.Exists(writePath)) File.Create(writePath);
        writePath.ToLog();
    }

    public void ToLog(string text)
    {
        LOGS += "[" + Time.realtimeSinceStartup + "] - " + text + "\n";
    }

    void OnApplicationQuit()
    {
        "---------------------App Ended---------------------".ToLog();
        LogYaz();
    }
    void LogYaz()
    {
        StreamWriter writer = new StreamWriter(writePath, false);
        writer.Write(LOGS);
        writer.Flush();
        writer.Close();
    }
}
