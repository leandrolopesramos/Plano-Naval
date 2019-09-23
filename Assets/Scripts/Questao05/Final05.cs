using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final05 : MonoBehaviour
{
    public Button BtnContinuar, SetaEsq, SetaDir;

    public GameObject Painel01, Quadrinho01, Quadrinho02, Quadrinho03, Quadrinho04;

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
        }

        if (x == 3)
        {
            Quadrinho02.SetActive(false);
            Quadrinho03.SetActive(true);
        }

        if (x == 4)
        {
            Quadrinho03.SetActive(false);
            Quadrinho04.SetActive(true);

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
        }

        if (x == 2)
        {
            Quadrinho02.SetActive(true);
            Quadrinho03.SetActive(false);

        }

        if (x == 3)
        {
            Quadrinho03.SetActive(true);
            Quadrinho04.SetActive(false);

            SetaDir.interactable = true;
        }
    }
}
