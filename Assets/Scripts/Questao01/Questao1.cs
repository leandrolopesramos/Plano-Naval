using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questao1 : MonoBehaviour
{
    public Text Questao, AlternativaCorreta, Alternativa1, Alternativa2, Alternativa3, Alternativa4, Alternativaescolhida, NumeroQuestao, TextoAcertos1, TextoAcertos2, TextoPtos, AcertosFinal;
    public GameObject Mapa01, Mapa02, PainelResposta, PainelComecar, PainelMeio, PainelFinal, correto, incorreto, aprovado, reprovado;
    private int nivel = 1, subnivel = 1, n = 1, acertos = 0, pontos = 0, x = 0;
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

        ChamarQuestao();

        Debug.Log(ids[0]);
        Debug.Log(ids[1]);
        Debug.Log(ids[2]);
        Debug.Log(ids[3]);
        Debug.Log(ids[4]);
    }

    public void ChamarQuestao() { StartCoroutine(ConsultaQuestao()); }
    public void ConsultarIds() { StartCoroutine(ConsultaIds()); }
    public void gravarSemNivel() { StartCoroutine(SemNivel()); }
    public void gravarComNivel() { StartCoroutine(ComNivel()); }

    IEnumerator ConsultaIds()
    {
        WWWForm quest = new WWWForm();

        quest.AddField("nivel", nivel);
        quest.AddField("subnivel", subnivel);

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

        NumeroQuestao.text = n.ToString();
    }

    IEnumerator ComNivel()
    {
        nivel++;
        DBManager.nivel = nivel;

        WWWForm form = new WWWForm();

        form.AddField("id", DBManager.id.ToString());
        form.AddField("pontos", pontos.ToString());
        form.AddField("nivel", nivel.ToString());

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
        nivel = DBManager.nivel;

        WWWForm form = new WWWForm();

        form.AddField("id", DBManager.id.ToString());
        form.AddField("pontos", pontos.ToString());
        form.AddField("nivel", DBManager.nivel.ToString());

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
            acertos++;
            int i = acertos * 2;
            TextoPtos.text = i.ToString();
            Debug.Log("Acertou!");
            correto.SetActive(true); 
        }

        else
        {
            incorreto.SetActive(true);
            Debug.Log("Errou!");
        }
        TextoAcertos1.text = acertos.ToString();
        TextoAcertos2.text = n.ToString();
        PainelResposta.SetActive(true);
    }

    public void btnAlt2()
    {
        Alternativaescolhida.text = Alternativa2.text;

        if (Alternativa2.text == AlternativaCorreta.text)
        {
            acertos++;
            int i = acertos * 2;
            TextoPtos.text = i.ToString();
            correto.SetActive(true);
            Debug.Log("Acertou!");
        }

        else
        {
            incorreto.SetActive(true);
            Debug.Log("Errou!");
        }

        TextoAcertos1.text = acertos.ToString();
        TextoAcertos2.text = n.ToString();
        PainelResposta.SetActive(true);
    }

    public void btnAlt3()
    {
        Alternativaescolhida.text = Alternativa3.text;

        if (Alternativa3.text == AlternativaCorreta.text)
        {
            acertos++;
            int i = acertos * 2;
            TextoPtos.text = i.ToString();
            correto.SetActive(true);
            Debug.Log("Acertou!");
        }

        else
        {
            incorreto.SetActive(true);
            Debug.Log("Errou!");
        }

        TextoAcertos1.text = acertos.ToString();
        TextoAcertos2.text = n.ToString();
        PainelResposta.SetActive(true);
    }

    public void btnAlt4()
    {
        Alternativaescolhida.text = Alternativa4.text;

        if (Alternativa4.text == AlternativaCorreta.text)
        {
            acertos++;
            int i = acertos * 2;
            TextoPtos.text = i.ToString();
            Debug.Log("Acertou!");
            correto.SetActive(true);
        }

        else
        {
            incorreto.SetActive(true);
            Debug.Log("Errou!");
        }

        TextoAcertos1.text = acertos.ToString();
        TextoAcertos2.text = n.ToString();
        PainelResposta.SetActive(true);
    }

    public void btnProxima()
    {
        n++;
        x++;

        if (n < 6) {
            idQuestão = ids[x];
            Debug.Log("Nivel 1 - Pos:"+ x + " = " + idQuestão);

            ChamarQuestao();
            subnivel = 2;
        }

        if (n == 6)
        {
            x = 0;
            ids = new List<string>();
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ConsultarIds();

            Mapa01.SetActive(false);
            Mapa02.SetActive(true);

            PainelMeio.SetActive(true);
        }

        if (n > 6 && n <= 10)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();
        }
        
        if (n > 10)
        {
            AcertosFinal.text = acertos.ToString();

            PainelFinal.SetActive(true);

            if (acertos > 6)
            {
                aprovado.SetActive(true);

                pontos = acertos * 2;
                pontos = DBManager.score + pontos;
                DBManager.score = pontos;

                if (DBManager.nivel <= nivel)
                {
                    gravarComNivel();
                }
                else
                {
                    gravarSemNivel();
                }
            }
            else{
                reprovado.SetActive(true);
            }

            
        }

        PainelResposta.SetActive(false);
        correto.SetActive(false);
        incorreto.SetActive(false);
    }

    public void BtnFinalizar()
    {
        nivel = 1;
        subnivel = 1;
        n = 1;
        acertos = 0;
        pontos = 0;
        x = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }   
}