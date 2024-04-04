using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

struct SaveData
{
    public int PlayerHP; // int because its simpler to work on a life count basis (also lighter if we need to compare values due to lack of floating values that arent 100% accurate)
    public int ItemsCollected;
    #region Items Index
    /*
      0x    0000 0001 - ??
      0x    0000 0010 - ??
      0x    0000 0100 - ??
      0x    0000 1000 - ??
      0x    0001 0000 - ??
      0x    0010 0000 - ??
      0x    0100 0000 - ??
      0x    1000 0000 - ??
      ...
    */
    #endregion
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
    private static int GetNumberOfSaves()
    {
        DirectoryInfo dir = new DirectoryInfo(SavesLocation);
        if (dir.Exists)
        {
            return dir.GetFiles().Length;
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
            ListToReturn = new SaveFileData[Files.Length];

            int Index = 0;
            foreach(FileInfo file in Files)
            {
                ListToReturn[Index].CreationDateTime = file.CreationTime;
                ListToReturn[Index++].FileName = file.Name;
            }
        }
        return ListToReturn;
    }
}
