using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFases : MonoBehaviour
{
    public Button BtnVoltarRank;
    public Button BtnRank;
    public Button BtnSim;
    public Button BtnNao;
    public Button BtnLogOut;
    public Button BtnFase01;
    public Button BtnFase02;
    public Button BtnFase03;
    public Button BtnFase04;
    public Button BtnFase05;
    public Button BtnFase06;
    public Button Aprovado01;
    public Button Aprovado02;
    public Button Aprovado03;
    public Button Aprovado04;
    public Button Aprovado05;
    public Button Aprovado06;

    public GameObject PainelLogOut;
    public GameObject PainelRank;

    public Text usuario;
    public Text pontos;
    public Text nome01;
    public Text pontos01;
    public Text nome02;
    public Text pontos02;
    public Text nome03;
    public Text pontos03;
    public Text nome04;
    public Text pontos04;
    public Text nome05;
    public Text pontos05;

    private List<string> ranking = new List<string>();

    void FixedUpdate()
    {
        Atualizar();

        StartCoroutine(ConsultaRanking());
    }

    private void Awake()
    {
        Atualizar();

        StartCoroutine(ConsultaRanking());
    }

    IEnumerator ConsultaRanking()
    {
        WWWForm quest = new WWWForm();
        WWW www = new WWW(DBManager.acesso + "ranking.php", quest);

        yield return www;

        int i = 1;

        if (www.text[0] == '0')
        {
            while (i < 11)
            {
                ranking.Add(www.text.Split('\t')[i]);
                i++;
            }

            Debug.Log("Ranking Carregados com Sucesso!");

            nome01.text = ranking[0];
            pontos01.text = ranking[1];

            nome02.text = ranking[2];
            pontos02.text = ranking[3];

            nome03.text = ranking[4];
            pontos03.text = ranking[5];

            nome04.text = ranking[6];
            pontos04.text = ranking[7];

            nome05.text = ranking[8];
            pontos05.text = ranking[9];
        }

        else
        {
            Debug.Log("Falha na Consulta: Erro#" + www.text);
        }
    }

    public void Atualizar()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (DBManager.login)
        {
            usuario.text = DBManager.username;
            pontos.text = DBManager.score.ToString();
        }

        if (DBManager.nivel == 2)
        {
            BtnFase02.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
        }

        else if (DBManager.nivel == 3)
        {
            BtnFase02.interactable = DBManager.login;
            BtnFase03.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
            Aprovado02.gameObject.SetActive(true);
        }

        else if (DBManager.nivel == 4)
        {
            BtnFase02.interactable = DBManager.login;
            BtnFase03.interactable = DBManager.login;
            BtnFase04.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
            Aprovado02.gameObject.SetActive(true);
            Aprovado03.gameObject.SetActive(true);
        }

        else if (DBManager.nivel == 5)
        {
            BtnFase02.interactable = DBManager.login;
            BtnFase03.interactable = DBManager.login;
            BtnFase04.interactable = DBManager.login;
            BtnFase05.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
            Aprovado02.gameObject.SetActive(true);
            Aprovado03.gameObject.SetActive(true);
            Aprovado04.gameObject.SetActive(true);
        }

        else if (DBManager.nivel == 6)
        {
            BtnFase02.interactable = DBManager.login;
            BtnFase03.interactable = DBManager.login;
            BtnFase04.interactable = DBManager.login;
            BtnFase05.interactable = DBManager.login;
            BtnFase06.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
            Aprovado02.gameObject.SetActive(true);
            Aprovado03.gameObject.SetActive(true);
            Aprovado04.gameObject.SetActive(true);
            Aprovado05.gameObject.SetActive(true);
        }
        else if (DBManager.nivel == 7)
        {
            BtnFase02.interactable = DBManager.login;
            BtnFase03.interactable = DBManager.login;
            BtnFase04.interactable = DBManager.login;
            BtnFase05.interactable = DBManager.login;
            BtnFase06.interactable = DBManager.login;
            Aprovado01.gameObject.SetActive(true);
            Aprovado02.gameObject.SetActive(true);
            Aprovado03.gameObject.SetActive(true);
            Aprovado04.gameObject.SetActive(true);
            Aprovado05.gameObject.SetActive(true);
            Aprovado06.gameObject.SetActive(true);
        }
    }

    public void Fase01()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void Fase02()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void Fase03()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }

    public void Fase04()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(10);
    }

    public void Fase05()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(13);
    }

    public void Fase06()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(16);
    }

    public void RankingPainel()
    {
        PainelRank.SetActive(true);

    }

    public void Logout()
    {
        DBManager.logOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LogoutPainel()
    {
        PainelLogOut.SetActive(true);
    }

    public void VoltarPainel()
    {
        PainelLogOut.SetActive(false);
        PainelRank.SetActive(false);
    }
}
