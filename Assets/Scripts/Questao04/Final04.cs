using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final04 : MonoBehaviour
{
    public Button BtnContinuar, SetaEsq, SetaDir;

    public GameObject Quadrinho01, Quadrinho02;

    public int x = 1;

    public void Continuar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void SetaDireita()
    {
        x++;

        if (x == 2)
        {
            Quadrinho01.SetActive(false);
            Quadrinho02.SetActive(true);

            SetaEsq.interactable = true;        
            SetaDir.interactable = false;
            BtnContinuar.interactable = true;
        }
    }

    public void SetaEsquerda()
    {
        x--;

        if (x == 1)
        {
            Quadrinho01.SetActive(true);
            Quadrinho02.SetActive(false);

            SetaEsq.interactable = false;
            SetaDir.interactable = true;
        }
    }
}
