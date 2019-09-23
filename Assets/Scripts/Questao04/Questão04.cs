using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questão04 : MonoBehaviour
{
    public Text Questao, AlternativaCorreta, Alternativa1, Alternativa2, Alternativa3, Alternativa4, Alternativaescolhida, 
                NumeroQuestao, TxtAcerto, TxtQtdQuestoesRespond, TextoPtos, AcertosFinal;
    public GameObject Mapa01, Mapa02, Mapa03, Mapa04, Mapa05, PainelComecar, Painel01, Painel02, Painel03, Painel04, HP01, HP02, HP03, HP04, HP05,
                      Estou01, Estou02, Estou03, Estou04, Estou05, Estou06, Estou07, Estou08, Estou09, Estou10, Estou11, Estou12, Estou13, Estou14, Estou15,
                      PainelResposta, PainelFinal, Correto, Incorreto, Aprovado, Reprovado;
    private int Nivel = 4, Subnivel = 1, NumQuestao = 1, Acertos = 0, Pontos = 0, x = 0, err = 0;
    private string idQuestão;
    private List<string> ids = new List<string>();

    private void Start()
    {
        ConsultarIds();
    }

    public void Comecar()
    {
        PainelComecar.SetActive(false);

        Painel01.SetActive(false);
        Painel02.SetActive(false);
        Painel03.SetActive(false);
        Painel04.SetActive(false);
        Estou01.SetActive(false);

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

        for (int n = 1; n < 16 ; n++) {

            quest.AddField("nivel", Nivel);
            quest.AddField("subnivel", n);

            WWW www = new WWW(DBManager.acesso + "ids.php", quest);
            yield return www;

            if (www.text[0] == '0')
            {
                ids.Add(www.text.Split('\t')[1]);
                Debug.Log("Ids Carregados com Sucesso!");
            }
            else
            {
                Debug.Log("Falha na Consulta: Erro#" + www.text);
            }
        }

        Debug.Log(ids[0]);
        Debug.Log(ids[1]);
        Debug.Log(ids[2]);
        Debug.Log(ids[3]);
        Debug.Log(ids[4]);
        Debug.Log(ids[5]);
        Debug.Log(ids[6]);
        Debug.Log(ids[7]);
        Debug.Log(ids[8]);
        Debug.Log(ids[9]);
        Debug.Log(ids[10]);
        Debug.Log(ids[11]);
        Debug.Log(ids[13]);
        Debug.Log(ids[14]);
    }

    IEnumerator ConsultaQuestao()
    {
        WWWForm quest = new WWWForm();

        quest.AddField("id", ids[x].ToString());

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
        x++;

        if (Alternativa1.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 8;
            TextoPtos.text = i.ToString();

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);

            err++;
            Debug.Log("Errou!");
        }

        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt2()
    {
        Alternativaescolhida.text = Alternativa2.text;
        x++;

        if (Alternativa2.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 8;
            TextoPtos.text = i.ToString();

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);

            err++;
            Debug.Log("Errou!");
        }

        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt3()
    {
        Alternativaescolhida.text = Alternativa3.text;
        x++;

        if (Alternativa3.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 8;
            TextoPtos.text = i.ToString();

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            err++;
            Debug.Log("Errou!");
        }

        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt4()
    {
        Alternativaescolhida.text = Alternativa4.text;
        x++;

        if (Alternativa4.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 8;
            TextoPtos.text = i.ToString();

            Debug.Log("Acertou!");

            Correto.SetActive(true);
        }

        else
        {
            Incorreto.SetActive(true);
            err++;
            Debug.Log("Errou!");
        }

        TxtAcerto.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnProxima()
    {
        NumQuestao++;
        Subnivel++;

        if (err == 1)
        {
            HP05.SetActive(false);
        }

        if (err == 2)
        {
            HP04.SetActive(false);
        }

        if (err == 3)
        {
            HP03.SetActive(false);
        }

        if (err == 4)
        {
            HP02.SetActive(false);
        }

        if (err >= 5)
        {
            HP01.SetActive(false);
            NumQuestao = 16;
        }

        if (NumQuestao == 2)
        {
            ChamarQuestao();

            Estou01.SetActive(false);
            Estou02.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 3)
        {
            ChamarQuestao();

            Estou02.SetActive(false);
            Estou03.SetActive(true);
           
            Debug.Log(Questao);
        }

        if (NumQuestao == 4)
        {
            ChamarQuestao();

            Estou03.SetActive(false);
            Estou04.SetActive(true);

            Mapa01.SetActive(false);
            Mapa02.SetActive(true);

            Painel01.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 5)
        {
            ChamarQuestao();

            Estou04.SetActive(false);
            Estou05.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 6)
        {
            ChamarQuestao();

            Estou05.SetActive(false);
            Estou06.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 7)
        {
            ChamarQuestao();

            Estou06.SetActive(false);
            Estou07.SetActive(true);

            Mapa02.SetActive(false);
            Mapa03.SetActive(true);

            Painel02.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 8)
        {
            ChamarQuestao();

            Estou07.SetActive(false);
            Estou08.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 9)
        {
            ChamarQuestao();

            Estou08.SetActive(false);
            Estou09.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 10)
        {
            ChamarQuestao();

            Estou09.SetActive(false);
            Estou10.SetActive(true);

            Mapa03.SetActive(false);
            Mapa04.SetActive(true);

            Painel03.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 11)
        {
            ChamarQuestao();

            Estou10.SetActive(false);
            Estou11.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 12)
        {
            ChamarQuestao();

            Estou11.SetActive(false);
            Estou12.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 13)
        {
            ChamarQuestao();

            Estou12.SetActive(false);
            Estou13.SetActive(true);

            Mapa04.SetActive(false);
            Mapa05.SetActive(true);

            Painel04.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 14)
        {
            ChamarQuestao();

            Estou13.SetActive(false);
            Estou14.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao == 15)
        {
            ChamarQuestao();

            Estou14.SetActive(false);
            Estou15.SetActive(true);

            Debug.Log(Questao);
        }

        if (NumQuestao > 15)
        {
            AcertosFinal.text = Acertos.ToString();

            PainelFinal.SetActive(true);

            if (Acertos > 10)
            {
                Aprovado.SetActive(true);
                Pontos = Acertos * 8;

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
            else
            {
                Reprovado.SetActive(true);
            }


        }

        NumeroQuestao.text = NumQuestao.ToString();
        PainelResposta.SetActive(false);
        Correto.SetActive(false);
        Incorreto.SetActive(false);
    }

    public void BtnFinalizar()
    {

        if (Acertos > 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(12);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        Nivel = 4;
        Subnivel = 1;
        NumQuestao = 1;
        Acertos = 0;
        Pontos = 0;
        x = 0;
        err = 0;
        Acertos = 0;
        Pontos = 0;


    }
}
