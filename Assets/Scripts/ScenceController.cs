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
		Debug.Log ("load scence");
        SceneManager.LoadScene(0);
	}
}
