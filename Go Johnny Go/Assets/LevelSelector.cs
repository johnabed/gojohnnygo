using UnityEngine;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour {

    public SceneFader fader;
    public Button[] levelButtons;
    public Sprite sprite1;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached",1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].image.overrideSprite = sprite1;
                levelButtons[i].GetComponentInChildren<Text>().text = "";
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
