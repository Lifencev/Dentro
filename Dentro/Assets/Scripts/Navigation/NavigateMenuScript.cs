using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class NavigateMenuScript : MonoBehaviour
{
    public static List<string> allLevelNames = new() { "LazinessLevel", "AngerLevel", "FearLevel" };
    public static List<string> leftLevelNames = new() { "LazinessLevel", "AngerLevel", "FearLevel" };
    public static int level = 1;
    public static string currentLevel;

    public void Start()
    {
        FindObjectOfType<AudioManager>().PlayS("main_theme");
    }

    public static string RandomLevel()
    {
        int random = Random.Range(0, leftLevelNames.Count);
        string randomName = leftLevelNames[random];
        leftLevelNames.RemoveAt(random);
        return randomName;
    }

    public static void LoadLevel(bool LoadNewGame) {
        FindObjectOfType<AudioManager>().Play("click");
        if (LoadNewGame || level == 1)
        {
            level = 1;
            leftLevelNames = new List<string>(allLevelNames);

            currentLevel = RandomLevel();
        }
        else
        {
            string path = Application.persistentDataPath + "/prevgame.sav";
            if(File.Exists(path))
                currentLevel = LoadGame();
            else 
                Debug.Log("Couldn't load stuff");
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene");
    }

    public static void LoadNextLevel(bool LoadNewGame){
        if (LoadNewGame)
        {
            level = 1;
            leftLevelNames = new List<string>(allLevelNames);
            SceneManager.LoadScene(RandomLevel());
        }
        else{
        
        leftLevelNames.Remove(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(RandomLevel());
        }
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsMenuScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
    }

    public static void SaveGame(){
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/prevgame.sav";
        FileStream stream = new(path, FileMode.Create);
        GameData data = new() { levelnum = level };
        data.setcurrentstats(FindObjectOfType<Health_System>().currentStats);
        data.setstandardstats(FindObjectOfType<Health_System>().getstandardstats());
        data.setLeftNames(leftLevelNames);
        data.currentLevel = SceneManager.GetActiveScene().name; 
        formatter.Serialize(stream, data);
    }

    public static string LoadGame(){
        string path = Application.persistentDataPath + "/prevgame.sav";
        if(File.Exists(path)){
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open); 
             GameData data = formatter.Deserialize(stream) as GameData;
             stream.Close();
             leftLevelNames = data.getLeftNames();
             FindObjectOfType<Health_System>().currentStats = data.currentstats;
             FindObjectOfType<Health_System>().setstandardstats(data.standardstats);
             level = data.levelnum;
             return data.currentLevel;
        }else{
            Debug.LogError("Save not found");
            return "some string";
        }
    }

    public void QuitGame() {
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Quit");
        Application.Quit();
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }
}
