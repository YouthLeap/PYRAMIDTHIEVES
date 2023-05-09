using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEditorMenu : MonoBehaviour {
    public void OpenLevelEditor()
    {
        SceneManager.LoadScene("LevelEditScene");
    }
}
