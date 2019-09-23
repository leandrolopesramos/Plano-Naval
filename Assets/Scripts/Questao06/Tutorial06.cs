﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial06 : MonoBehaviour
{
    public Button BtnContinuar, BtnVoltar, SetaEsq, SetaDir;

    public GameObject Quadrinho01, Quadrinho02, Quadrinho03, Quadrinho04, Quadrinho05, Quadrinho06, Quadrinho07;

    public int x = 1;

    public void Continuar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(17);
    }

    public void Voltar()
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

        }

        if (x == 5)
        {
            Quadrinho04.SetActive(false);
            Quadrinho05.SetActive(true);
        }

        if (x == 6)
        {
            Quadrinho05.SetActive(false);
            Quadrinho06.SetActive(true);
        }

        if (x == 7)
        {
            Quadrinho06.SetActive(false);
            Quadrinho07.SetActive(true);

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

        }

        if (x == 4)
        {
            Quadrinho04.SetActive(true);
            Quadrinho05.SetActive(false);
        }

        if (x == 5)
        {
            Quadrinho05.SetActive(true);
            Quadrinho06.SetActive(false);
        }

        if (x == 6)
        {
            Quadrinho06.SetActive(true);
            Quadrinho07.SetActive(false);

            SetaDir.interactable = true;
        }
    }
}
