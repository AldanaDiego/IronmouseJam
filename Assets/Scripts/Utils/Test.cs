using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private SceneTransitionManager _sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        _sceneManager = SceneTransitionManager.GetInstance();
    }

    public void OnClick()
    {
        _sceneManager.ChangeToTitleScene();
    }
}
