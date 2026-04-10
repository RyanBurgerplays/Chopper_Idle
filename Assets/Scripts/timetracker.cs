using System.IO;
using UnityEngine;

public class timetracker : MonoBehaviour
{
    private string filePath;
    private float sessionStartTime;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "time_log.txt");

        sessionStartTime = Time.time;

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Session Playtime Log\n");
            Debug.Log("Created new session log file.");
        }
    }

    void OnApplicationQuit()
    {
        SaveSessionTime();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSessionTime(); 
        }
    }

    void SaveSessionTime()
    {
        float sessionDuration = Time.time - sessionStartTime;

        string entry =  sessionDuration.ToString("F2") + " seconds\n";

        File.AppendAllText(filePath, entry);

        Debug.Log("Session saved: " + entry);
    }
}
