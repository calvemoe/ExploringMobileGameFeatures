using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerUI_s : MonoBehaviour {

	public void GoPlay()
    {
        SceneManager.LoadScene("3DSpace_s");
    }
}
