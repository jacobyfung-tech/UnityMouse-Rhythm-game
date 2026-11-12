using UnityEngine;

public class GameState : MonoBehaviour
{
    public static int levelNumber; //not set
    public AudioClip[] songList;
    public NoteTracker nT;
    public static bool canInput = true;
    public FadeInOut initializeScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void loadJSON(){
        TextAsset file = Resources.Load<TextAsset>($"SongLevels/level_{GameState.levelNumber}");
        JsonUtility.FromJsonOverwrite(file.text, nT);

        
    }
    void Start()
    {
        VectorUtils.Init();
        // 1. Disable VSync (Required, otherwise targetFrameRate is ignored)
        QualitySettings.vSyncCount = 0;

        // 2. Set the target frame rate (e.g., 60 FPS)
        Application.targetFrameRate = 60;

        StartCoroutine(initializeScreen.FadeIn(0.3f));
        Invoke("StartLevel",0.3f);
    }

    void StartLevel()
    {
        loadJSON();

        if (levelNumber == 0){
            canInput = false;
            StartCoroutine(nT.startTutorial(songList[0]));
        }
        else{
            canInput = true;
            StartCoroutine(nT.startLevel(songList[levelNumber]));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
