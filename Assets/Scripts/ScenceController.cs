using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenceController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button restartBtn;
    void Start()
    {
        Button btn = restartBtn.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        switch (SceneMgr.currentScene)
        {
            case SceneName.MezScene:
                SceneManager.LoadScene("MezScene");
                break;
            case SceneName.MawinScene:
                SceneManager.LoadScene("Stage1Scene");
                break;
            case SceneName.TunScene:
                SceneManager.LoadScene("TunScene");
                break;
            case SceneName.FightScene:
                SceneManager.LoadScene("FightScene");
                break;
            case SceneName.ZezeScene:
                SceneManager.LoadScene("ZezeScene");
                break;
        }
	}
}
