using UnityEngine;
using System.Collections;

public class TutorialDialog : MonoBehaviour {
    [SerializeField]
    private Animator anim;


	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Next");
        }
    }

    public void Finish()
    {
        gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }
}
