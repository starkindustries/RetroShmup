using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void DidPressStartButton()
    {
        LevelChanger.Instance.FadetoScene(sceneIndex: 1);
    }
}
