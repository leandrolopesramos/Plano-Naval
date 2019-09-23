using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questao3 : MonoBehaviour
{
    public Text Questao, AlternativaCorreta, Alternativa1, Alternativa2, Alternativa3, Alternativa4, Alternativaescolhida, NumeroQuestao, TxtAcerto, TxtQtdQuestoesRespond, AcertosFinal;
    public GameObject Mapa01, Mapa02, Mapa03, PainelComecar, PainelResposta, PainelMeio, PainelFinal, Correto, Incorreto, Aprovado, Reprovado;
    private int Nivel = 3, Subnivel = 1, NumQuestao = 1, Acertos = 0, Pontos = 0, x = 0;
    private string idQuestão;
    private List<string> ids = new List<string>();

    private void Start()
    {
        ConsultarIds();
    }

    public void Comecar()
    {
        PainelComecar.SetActive(false);
        PainelMeio.SetActive(false);

        NumeroQuestao.text = NumQuestao.ToString();
        ChamarQuestao();
    }

    public void ChamarQuestao() { StartCoroutine(ConsultaQuestao()); }
    public void ConsultarIds() { StartCoroutine(ConsultaIds()); }
    public void gravarSemNivel() { StartCoroutine(SemNivel()); }
    public void gravarComNivel() { StartCoroutine(ComNivel()); }

    IEnumerator ConsultaIds()
    {
        WWWForm quest = new WWWForm();

        quest.AddField("nivel", Nivel);
        quest.AddField("subnivel", Subnivel);

        WWW www = new WWW(DBManager.acesso + "ids.php", quest);
        yield return www;

        int i = 1;

        if (www.text[0] == '0')
        {
            while (i < 6)
            {
                ids.Add(www.text.Split('\t')[i]);
                i++;
            }

            Debug.Log("Ids Carregados com Sucesso!");

            idQuestão = ids[0];
        }

        else
        {
            Debug.Log("Falha na Consulta: Erro#" + www.text);
        }
    }

    IEnumerator ConsultaQuestao()
    {
        WWWForm quest = new WWWForm();

        quest.AddField("id", idQuestão.ToString());

        WWW www = new WWW(DBManager.acesso + "questao.php", quest);
        yield return www;

        if (www.text[0] == '0')
        {
            Questao.text = www.text.Split('\t')[1];
            AlternativaCorreta.text = www.text.Split('\t')[2];
            Alternativa1.text = www.text.Split('\t')[3];
            Alternativa2.text = www.text.Split('\t')[4];
            Alternativa3.text = www.text.Split('\t')[5];
            Alternativa4.text = www.text.Split('\t')[6];

            Debug.Log("Questão Carregada com Sucesso!");
        }

        else
        {
            Debug.Log("Falha na Consulta: Erro#" + www.text);
        }
    }

    IEnumerator ComNivel()
    {
        Nivel++;
        DBManager.nivel = Nivel;

        WWWForm form = new WWWForm();

        form.AddField("id", DBManager.id.ToString());
        form.AddField("pontos", Pontos.ToString());
        form.AddField("nivel", Nivel.ToString());

        WWW www = new WWW(DBManager.acesso + "save.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Pontuação salva!");
        }
        else
        {
            Debug.Log("Falha na Consulta: Erro#" + www.text);
        }
    }

    IEnumerator SemNivel()
    {
        Nivel = DBManager.nivel;

        WWWForm form = new WWWForm();

        form.AddField("id", DBManager.id.ToString());
        form.AddField("pontos", Pontos.ToString());
        form.AddField("nivel", Nivel.ToString());

        WWW www = new WWW(DBManager.acesso + "save.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Pontuação salva!");
        }
        else
        {
            Debug.Log("Falha na Consulta: Erro#" + www.text);
        }

    }

    public void btnAlt1()
    {
        Alternativaescolhida.text = Alternativa1.text;

        if (Alternativa1.text == AlternativaCorreta.text)
        {
            Acertos++;

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            Debug.Log("Errou!");
        }
        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt2()
    {
        Alternativaescolhida.text = Alternativa2.text;

        if (Alternativa2.text == AlternativaCorreta.text)
        {
            Acertos++;

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            Debug.Log("Errou!");
        }
        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt3()
    {
        Alternativaescolhida.text = Alternativa3.text;

        if (Alternativa3.text == AlternativaCorreta.text)
        {
            Acertos++;

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            Debug.Log("Errou!");
        }
        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt4()
    {
        Alternativaescolhida.text = Alternativa4.text;

        if (Alternativa4.text == AlternativaCorreta.text)
        {
            Acertos++;

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            Debug.Log("Errou!");
        }
        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnProxima()
    {
        NumQuestao++;
        x++;
        NumeroQuestao.text = NumQuestao.ToString();

        if (NumQuestao < 6)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 1 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();
            Subnivel = 2;
        }

        if (NumQuestao == 6)
        {
            x = 0;
            ids = new List<string>();
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ConsultarIds();

            Mapa01.SetActive(false);
            Mapa02.SetActive(true);

            PainelMeio.SetActive(true);
        }

        if (NumQuestao > 6 && NumQuestao < 11)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();
            Subnivel = 3;
        }

        if (NumQuestao == 11)
        {
            x = 0;
            ids = new List<string>();
            Debug.Log("Nivel 3 - Pos:" + x + " = " + idQuestão);

            ConsultarIds();

            Mapa02.SetActive(false);
            Mapa03.SetActive(true);

            PainelMeio.SetActive(true);
        }

        if (NumQuestao > 11 && NumQuestao <= 15)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 3 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();
        }

        if (NumQuestao > 15)
        {
            AcertosFinal.text = Acertos.ToString();

            PainelFinal.SetActive(true);

            if (Acertos > 10)
            {
                Aprovado.SetActive(true);

                Pontos = Acertos * 6;
                Pontos = DBManager.score + Pontos;
                DBManager.score = Pontos;

                if (DBManager.nivel <= Nivel)
                {
                    gravarComNivel();
                }
                else
                {
                    gravarSemNivel();
                }
            }
            else{
                Reprovado.SetActive(true);
            }


        }

        PainelResposta.SetActive(false);
        Correto.SetActive(false);
        Incorreto.SetActive(false);
    }

    public void BtnFinalizar()
    {

        if (Acertos > 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(9);
        }

        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        Nivel = 3;
        Subnivel = 1;
        NumQuestao = 1;
        Acertos = 0;
        Pontos = 0;
    }
}
