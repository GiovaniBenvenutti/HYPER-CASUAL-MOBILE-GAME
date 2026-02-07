using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
//  POSSO ACESSAR FILE/BUILD SETTINGS/SCENES IN BUILD PARA PEGAR O INDICE DAS MINHAS CENAS  
    public void Load(int i)
    {
        Time.timeScale = 1f;    // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
        SceneManager.LoadScene(i);
    }

    public void Load(string s)
    {
        Time.timeScale = 1f;   // REINICI O TIME SCALE PARA 1 AO CARREGAR CENA
        SceneManager.LoadScene(s);

    }
}
