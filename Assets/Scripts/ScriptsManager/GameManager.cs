using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string _nameNextScene;
    [SerializeField] private SOSettingsUser _soAnimationSelected;
    [SerializeField] private Animator _animatorPlayer;
    
    public void FunctionChangeAnimation(int idAnimation)
    {
        _animatorPlayer.SetInteger("DanceType", idAnimation);
        _soAnimationSelected.animSelection = idAnimation;
    }

    public void ButtonAnimationConfirmation()
    {
        StartCoroutine(NextSceneLoad());
    }

    private IEnumerator NextSceneLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_nameNextScene);
    }
}
