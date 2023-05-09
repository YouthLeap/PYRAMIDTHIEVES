using UnityEngine;
using System.Collections;

public class Toolbox : MonoBehaviour {
    [SerializeField]
    private GameObject toolbox;
    public void Toogle()
    {
        toolbox.SetActive(!toolbox.activeSelf);
    }
}
