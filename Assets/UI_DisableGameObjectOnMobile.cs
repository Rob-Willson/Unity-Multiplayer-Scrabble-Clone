using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DisableGameObjectOnMobile : MonoBehaviour
{

    private void Start()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            this.gameObject.SetActive(false);
        }
    }


}
