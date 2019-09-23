using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questao05 : MonoBehaviour
{
    public Text Questao, AlternativaCorreta, Alternativa1, Alternativa2, Alternativa3, Alternativa4, Alternativaescolhida,
                NumeroQuestao, TxtAcerto, TxtQtdQuestoesRespond, TextoPtos, AcertosFinal;
    public GameObject S01, S02, S03, S04, S05, S06, S07, S08, D01, D02, D03, D04, D05, D06, D07, D08, M01, M02, M03, M04, M05, M06, M07, M08, SubMeu01, SubMeu02, SubMeu03,
        PainelComecar, PainelResposta, PainelMeio, PainelUltimo, PainelFinal, Correto, Incorreto, Aprovado, Reprovado, HP01, HP02, HP03, HP04, HP05;
    private int Nivel = 5, Subnivel = 1, NumQuestao = 1, Acertos = 0, Pontos = 0, x = 0, err = 0;
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
        PainelUltimo.SetActive(false);

        NumeroQuestao.text = NumQuestao.ToString();

        ChamarQuestao();
        testarImagem();
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

    public void testarImagem()
    {
        if (idQuestão == "172")
        {
            S01.SetActive(true);

            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "170")
        {
            S02.SetActive(true);

            S01.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "171")
        {
            S03.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "169")
        {
            S04.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "168")
        {
            S05.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "167")
        {
            S06.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "166")
        {
            S07.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S08.SetActive(false);
        }
        if (idQuestão == "165")
        {
            S08.SetActive(true);

            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
        }

        if (idQuestão == "173")
        {
            D01.SetActive(true);

            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "174")
        {
            D02.SetActive(true);

            D01.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "175")
        {
            D03.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "176")
        {
            D04.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "177")
        {
            D05.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "178")
        {
            D06.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "179")
        {
            D07.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D08.SetActive(false);
        }
        if (idQuestão == "180")
        {
            D08.SetActive(true);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
        }

        if (idQuestão == "181")
        {
            M01.SetActive(true);

            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "182")
        {
            M02.SetActive(true);

            M01.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "183")
        {
            M03.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "184")
        {
            M04.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "185")
        {
            M05.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "186")
        {
            M06.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "187")
        {
            M07.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M08.SetActive(false);
        }
        if (idQuestão == "188")
        {
            M08.SetActive(true);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
        }
    }

    public void ZerarImagem()
    {
            S01.SetActive(false);
            S02.SetActive(false);
            S03.SetActive(false);
            S04.SetActive(false);
            S05.SetActive(false);
            S06.SetActive(false);
            S07.SetActive(false);
            S08.SetActive(false);

            D01.SetActive(false);
            D02.SetActive(false);
            D03.SetActive(false);
            D04.SetActive(false);
            D05.SetActive(false);
            D06.SetActive(false);
            D07.SetActive(false);
            D08.SetActive(false);

            M01.SetActive(false);
            M02.SetActive(false);
            M03.SetActive(false);
            M04.SetActive(false);
            M05.SetActive(false);
            M06.SetActive(false);
            M07.SetActive(false);
            M08.SetActive(false);        
    }

    public void btnAlt1()
    {
        Alternativaescolhida.text = Alternativa1.text;

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
        x++;
        

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

        if (NumQuestao < 6)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 1 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            Subnivel = 2;

            testarImagem();
        }

        if (NumQuestao == 6)
        {
            x = 0;
            ids = new List<string>();
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ConsultarIds();

            PainelMeio.SetActive(true);
            SubMeu01.SetActive(false);
            SubMeu02.SetActive(true);

            ZerarImagem();
        }

        if (NumQuestao > 6 && NumQuestao < 11)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 2 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            Subnivel = 3;

            testarImagem();
        }

        if (NumQuestao == 11)
        {
            x = 0;
            ids = new List<string>();
            Debug.Log("Nivel 3 - Pos:" + x + " = " + idQuestão);

            ConsultarIds();

            PainelUltimo.SetActive(true);

            ZerarImagem();

            SubMeu02.SetActive(false);
            SubMeu03.SetActive(true);
        }

        if (NumQuestao > 11 && NumQuestao <= 15)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 3 - Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            testarImagem();
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(15);
        }

        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        Nivel = 5;
        Subnivel = 1;
        NumQuestao = 1;
        Acertos = 0;
        Pontos = 0;

    }
}

