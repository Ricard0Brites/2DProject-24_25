using System;
using System.IO;
using UnityEngine;

public struct SaveData
{
    public SaveData(bool InHasSeenTutorial)
    {
        HasSeenTutorial = InHasSeenTutorial;
    }
    public bool HasSeenTutorial;
}
public struct SaveFileData
{
    public string FileName;
    public DateTime CreationDateTime;
}
public class SaveSystem : MonoBehaviour
{
    private static string SavesLocation = "Assets/SaveSystem/Saves/";
    public static int SaveToOverride = -1;
    public static int GetNumberOfSaves()
    {
        DirectoryInfo dir = new DirectoryInfo(SavesLocation);
        if (dir.Exists)
        {
            return dir.GetFiles().Length / 2;
        }
        return 0;
    }
    private static int GetSaveToOverride()
    {
        return SaveToOverride >= 0 ? SaveToOverride : Mathf.Min(Mathf.Max(GetNumberOfSaves() - 1, 0), 10);
    }
    public static bool Save(bool HasSeenTutorial)
    {
        //Data        
        SaveData Data = new SaveData(HasSeenTutorial);

        string SavePath = SavesLocation + GetSaveToOverride() + ".json";

        //Try to Create directory
        if (!Directory.CreateDirectory(SavesLocation).Exists)
            return false;

        //Save to file
        string DataToSave = JsonUtility.ToJson(Data);
        File.WriteAllText(SavePath, DataToSave);

        return true;
    }
    public static SaveData GetSavedValues(int SaveIndex)
    {
        SaveData ReturnObject = new SaveData();
		SaveFileData[] Files = GetAllSavesInfo();

		if (Files.Length >= 0 && SaveIndex >= 0 && SaveIndex <= Files.Length - 1)
		{
			StreamReader File = new StreamReader(SavesLocation + Files[SaveIndex].FileName);
            String Data = File.ReadToEnd();
            ReturnObject = JsonUtility.FromJson<SaveData>(Data);
		}
		return ReturnObject;
	}
    public static SaveFileData[] GetAllSavesInfo()
    {
        SaveFileData[] ListToReturn = null;
        DirectoryInfo dir = new DirectoryInfo(SavesLocation);
        if (dir.Exists)
        {
            FileInfo[] Files = dir.GetFiles();
            ListToReturn = new SaveFileData[Files.Length / 2];

            int Index = 0;
            foreach(FileInfo file in Files)
            {
                if (file.Name.Contains(".meta"))
                    continue;
                ListToReturn[Index].CreationDateTime = file.CreationTime;
                ListToReturn[Index++].FileName = file.Name;
            }
        }
        return ListToReturn;
    }
}
