using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

struct SaveData
{
    public int PlayerHP; // int because its simpler to work on a life count basis (also lighter if we need to compare values due to lack of floating values that arent 100% accurate)
    public ECollectibles ItemsCollected;
    public Scene CurrentScene; //Level in which the player is located
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
    public static bool SaveProgress(Player InPlayer)
    {
        //Data
        Scene CurrentScene = SceneManager.GetActiveScene();
        
        SaveData Data = new SaveData();
        Data.PlayerHP = InPlayer.GetHealth();
        Data.ItemsCollected = InPlayer.GetCurrentItems();
        Data.CurrentScene = CurrentScene;

        string SavePath = SavesLocation + GetSaveToOverride() + ".json";

        //Try to Creade directory
        if (!Directory.CreateDirectory(SavesLocation).Exists)
            return false;

        //Save to file
        string PlayerDataToSave = JsonUtility.ToJson(Data);
        File.WriteAllText(SavePath, PlayerDataToSave);

        return true;
    }
    public static bool LoadGame(int SaveIndexToLoad)
    {
        string FilePath = SavesLocation + SaveIndexToLoad + ".json";
        if (File.Exists(FilePath))
        {
            string JSON = File.ReadAllText(FilePath);
            SaveData SaveInfo = JsonUtility.FromJson<SaveData>(JSON);

            // TODO If GD comes back
            // load scene 
            // set character hp
            // set character items collected
            // Remove Collected Items from the scene
            return true;
        }
        return false;
    }
    public static bool DeleteSaveFile(int SaveIndexToLoad)
    {
        string FilePath = SavesLocation + SaveIndexToLoad + ".json";
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
            return true;
        }
        return false;
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
