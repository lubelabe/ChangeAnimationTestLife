using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettingsUser : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private SOSettingsUser soAnimationSelected;
    
    private void Start()
    {
        animatorPlayer.SetInteger("DanceType", soAnimationSelected.animSelection);
    }
}
