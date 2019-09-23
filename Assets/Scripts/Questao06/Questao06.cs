using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questao06 : MonoBehaviour
{
    public Text Questao, AlternativaCorreta, Alternativa1, Alternativa2, Alternativa3, Alternativa4, Alternativaescolhida,
                    NumeroQuestao, TxtAcerto01, TxtQtdQuestoesRespond, TextoPtos, AcertosFinal;

    public GameObject
        Nv01Mina01, Nv01Mina02, Nv01Mina03, Nv01Mina04, Nv01Mina05, Nv01Mina06, 
        Nv01Barco01A, Nv01Barco01B, Nv01Barco02A, Nv01Barco02B, Nv01Barco03A, Nv01Barco03B, Nv01Barco04A, Nv01Barco04B,

        Nv02Mina01, Nv02Mina02, Nv02Mina03, Nv02Mina04, Nv02Mina05, Nv02Mina06, 
        Nv02Barco01A, Nv02Barco01B, Nv02Barco02A, Nv02Barco02B, Nv02Barco03A, Nv02Barco03B, Nv02Barco04A, Nv02Barco04B,

        Nv03Miss01, Nv03Miss02, Nv03Miss03, Nv03Miss04, Nv03Miss05, Nv03Miss06,
        Nv03Miss01B, Nv03Miss02B, Nv03Miss03B, Nv03Miss04B, Nv03Miss05B, Nv03Miss06B,
        Nv03Barco01A, Nv03Barco01B, Nv03Barco02A, Nv03Barco02B, Nv03Barco03A, Nv03Barco03B, Nv03Barco04A, Nv03Barco04B,

        Nv04Disc01, Nv04Disc02, Nv04Disc03, Nv04Disc04, Nv04Disc05, Nv04Disc06,
        Nv04Disc01B, Nv04Disc02B, Nv04Disc03B, Nv04Disc04B, Nv04Disc05B, Nv04Disc06B,
        Nv04Barco01A, Nv04Barco01B,

        Nv05Disc01, Nv05Disc02, Nv05Disc03, Nv05Disc04, Nv05Disc05, Nv05Disc06,
        Nv05Disc01B, Nv05Disc02B, Nv05Disc03B, Nv05Disc04B, Nv05Disc05B, Nv05Disc06B,
        Nv05Barco01A, Nv05Barco01B,

        Mapa01, Mapa02, Mapa03, Mapa04, Mapa05,
        Mapa01B, Mapa02B, Mapa03B, Mapa04B, Mapa05B,

        PainelComecar, PainelMeio1, PainelMeio2, PainelMeio3, PainelMeio4, PainelResposta, Correto, Incorreto, PainelFinal, Aprovado, Reprovado, HP01, HP02, HP03, HP04, HP05;

    private int Nivel = 6, Subnivel = 1, NumQuestao = 1, Acertos = 0, Pontos = 0, x = 0, err = 0, posicao = 1;

    private string idQuestão;

    private List<string> ids = new List<string>();

    private void Start()
    {
        ConsultarIds();
    }

    public void Comecar()
    {
        PainelComecar.SetActive(false);
        PainelMeio1.SetActive(false);
        PainelMeio2.SetActive(false);
        PainelMeio3.SetActive(false);
        PainelMeio4.SetActive(false);

        NumeroQuestao.text = NumQuestao.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        ChamarQuestao();

        DesativarRadarInimigo();
        DesativarRadarBarcos();
    }

    public void ChamarQuestao() { StartCoroutine(ConsultaQuestao()); }
    public void ConsultarIds() { StartCoroutine(ConsultaIds()); }
    public void gravarSemNivel() { StartCoroutine(SemNivel()); }
    public void gravarComNivel() { StartCoroutine(ComNivel()); }


    IEnumerator ConsultaIds()
    {
        idQuestão = "";
        ids = new List<string>();

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
                Debug.Log(i + " = " + www.text.Split('\t')[i]);
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

    public void ZerarInimigos()
    {
        Nv01Mina01.SetActive(false);
        Nv01Mina02.SetActive(false);
        Nv01Mina03.SetActive(false);
        Nv01Mina04.SetActive(false);
        Nv01Mina05.SetActive(false);
        Nv01Mina06.SetActive(false);

        Nv02Mina01.SetActive(false);
        Nv02Mina02.SetActive(false);
        Nv02Mina03.SetActive(false);
        Nv02Mina04.SetActive(false);
        Nv02Mina05.SetActive(false);
        Nv02Mina06.SetActive(false);
        
        Nv03Miss01.SetActive(false);
        Nv03Miss02.SetActive(false);
        Nv03Miss03.SetActive(false);
        Nv03Miss04.SetActive(false);
        Nv03Miss05.SetActive(false);
        Nv03Miss06.SetActive(false);

        Nv03Miss01B.SetActive(false);
        Nv03Miss02B.SetActive(false);
        Nv03Miss03B.SetActive(false);
        Nv03Miss04B.SetActive(false);
        Nv03Miss05B.SetActive(false);
        Nv03Miss06B.SetActive(false);

        Nv04Disc01.SetActive(false);
        Nv04Disc02.SetActive(false);
        Nv04Disc03.SetActive(false);
        Nv04Disc04.SetActive(false);
        Nv04Disc05.SetActive(false);
        Nv04Disc06.SetActive(false);

        Nv04Disc01B.SetActive(false);
        Nv04Disc02B.SetActive(false);
        Nv04Disc03B.SetActive(false);
        Nv04Disc04B.SetActive(false);
        Nv04Disc05B.SetActive(false);
        Nv04Disc06B.SetActive(false);

        Nv05Disc01.SetActive(false);
        Nv05Disc02.SetActive(false);
        Nv05Disc03.SetActive(false);
        Nv05Disc04.SetActive(false);
        Nv05Disc05.SetActive(false);
        Nv05Disc06.SetActive(false);

        Nv05Disc01B.SetActive(false);
        Nv05Disc02B.SetActive(false);
        Nv05Disc03B.SetActive(false);
        Nv05Disc04B.SetActive(false);
        Nv05Disc05B.SetActive(false);
        Nv05Disc06B.SetActive(false);
    }

    public void ZerarBarcos()
    {

        Nv01Barco01A.SetActive(false);
        Nv01Barco01B.SetActive(false);
        Nv01Barco02A.SetActive(false);
        Nv01Barco02B.SetActive(false);
        Nv01Barco03A.SetActive(false);
        Nv01Barco03B.SetActive(false);
        Nv01Barco04A.SetActive(false);
        Nv01Barco04B.SetActive(false);

        Nv02Barco01A.SetActive(false);
        Nv02Barco01B.SetActive(false);
        Nv02Barco02A.SetActive(false);
        Nv02Barco02B.SetActive(false);
        Nv02Barco03A.SetActive(false);
        Nv02Barco03B.SetActive(false);
        Nv02Barco04A.SetActive(false);
        Nv02Barco04B.SetActive(false);

        Nv03Barco01A.SetActive(false);
        Nv03Barco01B.SetActive(false);
        Nv03Barco02A.SetActive(false);
        Nv03Barco02B.SetActive(false);
        Nv03Barco03A.SetActive(false);
        Nv03Barco03B.SetActive(false);
        Nv03Barco04A.SetActive(false);
        Nv03Barco04B.SetActive(false);

        Nv04Barco01A.SetActive(false);
        Nv04Barco01B.SetActive(false);

        Nv05Barco01A.SetActive(false);
        Nv05Barco01B.SetActive(false);

    }

    public void AtivarRadarInimigo()
    {
        //Nivel 01
        if (idQuestão == "189")
        {
            Nv01Mina01.SetActive(true);

            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }
        if (idQuestão == "190")
        {
            Nv01Mina02.SetActive(true);

            Nv01Mina01.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }
        if (idQuestão == "191")
        {
            Nv01Mina03.SetActive(true);

            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }
        if (idQuestão == "192")
        {
            Nv01Mina04.SetActive(true);

            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }
        if (idQuestão == "193")
        {
            Nv01Mina05.SetActive(true);

            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina06.SetActive(false);

        }
        if (idQuestão == "194")
        {
            Nv01Mina06.SetActive(true);

            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
        }

        //Nivel 02
        if (idQuestão == "195")
        {
            Nv02Mina01.SetActive(true);

            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }
        if (idQuestão == "196")
        {
            Nv02Mina02.SetActive(true);

            Nv02Mina01.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }
        if (idQuestão == "197")
        {
            Nv02Mina03.SetActive(true);

            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }
        if (idQuestão == "198")
        {
            Nv02Mina04.SetActive(true);

            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }
        if (idQuestão == "199")
        {
            Nv02Mina05.SetActive(true);

            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina06.SetActive(false);
        }
        if (idQuestão == "200")
        {
            Nv02Mina06.SetActive(true);

            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
        }

        //Nivel 03
        if (idQuestão == "201")
        {
            Nv03Miss01.SetActive(true);

            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }
        if (idQuestão == "202")
        {
            Nv03Miss02.SetActive(true);

            Nv03Miss01.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }
        if (idQuestão == "203")
        {
            Nv03Miss03.SetActive(true);

            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }
        if (idQuestão == "204")
        {
            Nv03Miss04.SetActive(true);

            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }
        if (idQuestão == "205")
        {
            Nv03Miss05.SetActive(true);

            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss06.SetActive(false);
        }
        if (idQuestão == "206")
        {
            Nv03Miss06.SetActive(true);

            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
        }

        //Nivel 04
        if (idQuestão == "207")
        {
            Nv04Disc01.SetActive(true);

            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "208")
        {
            Nv04Disc02.SetActive(true);

            Nv04Disc01.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "209")
        {
            Nv04Disc03.SetActive(true);

            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "210")
        {
            Nv04Disc04.SetActive(true);

            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "211")
        {
            Nv04Disc05.SetActive(true);

            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "212")
        {
            Nv04Disc06.SetActive(true);

            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
        }

        //Nivel 05
        if (idQuestão == "213")
        {
            Nv05Disc01.SetActive(true);

            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "214")
        {
            Nv05Disc02.SetActive(true);

            Nv05Disc01.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "215")
        {
            Nv05Disc03.SetActive(true);

            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "216")
        {
            Nv05Disc04.SetActive(true);

            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "217")
        {
            Nv05Disc05.SetActive(true);

            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "218")
        {
            Nv05Disc06.SetActive(true);

            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
        }
    }

    public void DesativarRadarInimigo()
    {
        //Nivel 01
        if (idQuestão == "189")
        {
            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }

        if (idQuestão == "190")
        {
            Nv01Mina02.SetActive(false);
            Nv01Mina01.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }

        if (idQuestão == "191")
        {
            Nv01Mina03.SetActive(false);
            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }

        if (idQuestão == "192")
        {
            Nv01Mina04.SetActive(false);
            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina05.SetActive(false);
            Nv01Mina06.SetActive(false);
        }

        if (idQuestão == "193")
        {
            Nv01Mina05.SetActive(false);
            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina06.SetActive(false);

        }

        if (idQuestão == "194")
        {
            Nv01Mina06.SetActive(false);
            Nv01Mina01.SetActive(false);
            Nv01Mina02.SetActive(false);
            Nv01Mina03.SetActive(false);
            Nv01Mina04.SetActive(false);
            Nv01Mina05.SetActive(false);
        }

        //Nivel 02
        if (idQuestão == "195")
        {
            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }

        if (idQuestão == "196")
        {
            Nv02Mina02.SetActive(false);
            Nv02Mina01.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }

        if (idQuestão == "197")
        {
            Nv02Mina03.SetActive(false);
            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }

        if (idQuestão == "198")
        {
            Nv02Mina04.SetActive(false);
            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina05.SetActive(false);
            Nv02Mina06.SetActive(false);
        }

        if (idQuestão == "199")
        {
            Nv02Mina05.SetActive(false);
            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina06.SetActive(false);
        }

        if (idQuestão == "200")
        {
            Nv02Mina06.SetActive(false);
            Nv02Mina01.SetActive(false);
            Nv02Mina02.SetActive(false);
            Nv02Mina03.SetActive(false);
            Nv02Mina04.SetActive(false);
            Nv02Mina05.SetActive(false);
        }

        //Nivel 03
        if (idQuestão == "201")
        {
            Nv03Miss01B.SetActive(true);

            Nv03Miss02B.SetActive(false);
            Nv03Miss03B.SetActive(false);
            Nv03Miss04B.SetActive(false);
            Nv03Miss05B.SetActive(false);
            Nv03Miss06B.SetActive(false);

            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }

        if (idQuestão == "202")
        {
            Nv03Miss02B.SetActive(true);

            Nv03Miss01B.SetActive(false);
            Nv03Miss03B.SetActive(false);
            Nv03Miss04B.SetActive(false);
            Nv03Miss05B.SetActive(false);
            Nv03Miss06B.SetActive(false);

            Nv03Miss02.SetActive(false);
            Nv03Miss01.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }

        if (idQuestão == "203")
        {
            Nv03Miss03B.SetActive(true);

            Nv03Miss01B.SetActive(false);
            Nv03Miss02B.SetActive(false);
            Nv03Miss04B.SetActive(false);
            Nv03Miss05B.SetActive(false);
            Nv03Miss06B.SetActive(false);

            Nv03Miss03.SetActive(false);
            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }

        if (idQuestão == "204")
        {
            Nv03Miss04B.SetActive(true);

            Nv03Miss01B.SetActive(false);
            Nv03Miss02B.SetActive(false);
            Nv03Miss03B.SetActive(false);
            Nv03Miss05B.SetActive(false);
            Nv03Miss06B.SetActive(false);

            Nv03Miss04.SetActive(false);
            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss05.SetActive(false);
            Nv03Miss06.SetActive(false);
        }

        if (idQuestão == "205")
        {
            Nv03Miss05B.SetActive(true);

            Nv03Miss01B.SetActive(false);
            Nv03Miss02B.SetActive(false);
            Nv03Miss03B.SetActive(false);
            Nv03Miss04B.SetActive(false);
            Nv03Miss06B.SetActive(false);

            Nv03Miss05.SetActive(false);
            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss06.SetActive(false);
        }

        if (idQuestão == "206")
        {
            Nv03Miss06B.SetActive(true);

            Nv03Miss01B.SetActive(false);
            Nv03Miss02B.SetActive(false);
            Nv03Miss03B.SetActive(false);
            Nv03Miss04B.SetActive(false);
            Nv03Miss05B.SetActive(false);

            Nv03Miss06.SetActive(false);
            Nv03Miss01.SetActive(false);
            Nv03Miss02.SetActive(false);
            Nv03Miss03.SetActive(false);
            Nv03Miss04.SetActive(false);
            Nv03Miss05.SetActive(false);
        }

        //Nivel 04
        if (idQuestão == "207")
        {
            Nv04Disc01B.SetActive(true);

            Nv04Disc02B.SetActive(false);
            Nv04Disc03B.SetActive(false);
            Nv04Disc04B.SetActive(false);
            Nv04Disc05B.SetActive(false);
            Nv04Disc06B.SetActive(false);

            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "208")
        {
            Nv04Disc02B.SetActive(true);

            Nv04Disc01B.SetActive(false);
            Nv04Disc03B.SetActive(false);
            Nv04Disc04B.SetActive(false);
            Nv04Disc05B.SetActive(false);
            Nv04Disc06B.SetActive(false);

            Nv04Disc02.SetActive(false);
            Nv04Disc01.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "209")
        {
            Nv04Disc03B.SetActive(true);

            Nv04Disc01B.SetActive(false);
            Nv04Disc02B.SetActive(false);
            Nv04Disc04B.SetActive(false);
            Nv04Disc05B.SetActive(false);
            Nv04Disc06B.SetActive(false);

            Nv04Disc03.SetActive(false);
            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "210")
        {
            Nv04Disc04B.SetActive(true);

            Nv04Disc01B.SetActive(false);
            Nv04Disc02B.SetActive(false);
            Nv04Disc03B.SetActive(false);
            Nv04Disc05B.SetActive(false);
            Nv04Disc06B.SetActive(false);

            Nv04Disc04.SetActive(false);
            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc05.SetActive(false);
            Nv04Disc06.SetActive(false);
        }

        if (idQuestão == "211")
        {
            Nv04Disc05B.SetActive(true);

            Nv04Disc01B.SetActive(false);
            Nv04Disc02B.SetActive(false);
            Nv04Disc03B.SetActive(false);
            Nv04Disc04B.SetActive(false);
            Nv04Disc06B.SetActive(false);
        }

        if (idQuestão == "212")
        {
            Nv04Disc06B.SetActive(true);

            Nv04Disc01B.SetActive(false);
            Nv04Disc02B.SetActive(false);
            Nv04Disc03B.SetActive(false);
            Nv04Disc04B.SetActive(false);
            Nv04Disc05B.SetActive(false);

            Nv04Disc06.SetActive(false);
            Nv04Disc01.SetActive(false);
            Nv04Disc02.SetActive(false);
            Nv04Disc03.SetActive(false);
            Nv04Disc04.SetActive(false);
            Nv04Disc05.SetActive(false);
        }

        //Nivel 05
        if (idQuestão == "213")
        {
            Nv05Disc01B.SetActive(true);

            Nv05Disc02B.SetActive(false);
            Nv05Disc03B.SetActive(false);
            Nv05Disc04B.SetActive(false);
            Nv05Disc05B.SetActive(false);
            Nv05Disc06B.SetActive(false);

            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "214")
        {
            Nv05Disc02B.SetActive(true);

            Nv05Disc01B.SetActive(false);
            Nv05Disc03B.SetActive(false);
            Nv05Disc04B.SetActive(false);
            Nv05Disc05B.SetActive(false);
            Nv05Disc06B.SetActive(false);

            Nv05Disc02.SetActive(false);
            Nv05Disc01.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "215")
        {
            Nv05Disc03B.SetActive(true);

            Nv05Disc01B.SetActive(false);
            Nv05Disc02B.SetActive(false);
            Nv05Disc04B.SetActive(false);
            Nv05Disc05B.SetActive(false);
            Nv05Disc06B.SetActive(false);

            Nv05Disc03.SetActive(false);
            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "216")
        {
            Nv05Disc04B.SetActive(true);

            Nv05Disc01B.SetActive(false);
            Nv05Disc02B.SetActive(false);
            Nv05Disc03B.SetActive(false);
            Nv05Disc05B.SetActive(false);
            Nv05Disc06B.SetActive(false);

            Nv05Disc04.SetActive(false);
            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc05.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "217")
        {
            Nv05Disc05B.SetActive(true);

            Nv05Disc01B.SetActive(false);
            Nv05Disc02B.SetActive(false);
            Nv05Disc03B.SetActive(false);
            Nv05Disc04B.SetActive(false);
            Nv05Disc06B.SetActive(false);

            Nv05Disc05.SetActive(false);
            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc06.SetActive(false);
        }

        if (idQuestão == "218")
        {
            Nv05Disc06B.SetActive(true);

            Nv05Disc01B.SetActive(false);
            Nv05Disc02B.SetActive(false);
            Nv05Disc03B.SetActive(false);
            Nv05Disc04B.SetActive(false);
            Nv05Disc05B.SetActive(false);

            Nv05Disc06.SetActive(false);
            Nv05Disc01.SetActive(false);
            Nv05Disc02.SetActive(false);
            Nv05Disc03.SetActive(false);
            Nv05Disc04.SetActive(false);
            Nv05Disc05.SetActive(false);
        }
    }

    public void AtivarRadarBarcos()
    {
        //Nivel01
        if (posicao == 1)
        {
            Nv01Barco01A.SetActive(false);
            Nv01Barco01B.SetActive(true);
        }
        if (posicao == 2)
        {
            Nv01Barco01A.SetActive(false);
            Nv01Barco01B.SetActive(false);
            Nv01Barco02A.SetActive(false);
            Nv01Barco02B.SetActive(true);
        }
        if (posicao == 3)
        {
            Nv01Barco02A.SetActive(false);
            Nv01Barco02B.SetActive(false);

            Nv01Barco03A.SetActive(false);
            Nv01Barco03B.SetActive(true);
        }
        if (posicao == 4)
        {
            Nv01Barco03A.SetActive(false);
            Nv01Barco03B.SetActive(false);

            Nv01Barco04A.SetActive(false);
            Nv01Barco04B.SetActive(true);
        }

        //Nivel02
        if (posicao == 5)
        {
            Nv01Barco04A.SetActive(false);
            Nv01Barco04B.SetActive(false);

            Nv02Barco01A.SetActive(false);
            Nv02Barco01B.SetActive(true);
        }
        if (posicao == 6)
        {
            Nv02Barco02A.SetActive(false);
            Nv02Barco02B.SetActive(true);
        }
        if (posicao == 7)
        {
            Nv02Barco01A.SetActive(false);
            Nv02Barco01B.SetActive(false);

            Nv02Barco03A.SetActive(false);
            Nv02Barco03B.SetActive(true);
        }
        if (posicao == 8)
        {
            Nv02Barco03A.SetActive(false);
            Nv02Barco03B.SetActive(false);

            Nv02Barco04A.SetActive(false);
            Nv02Barco04B.SetActive(true);
        }

        //Nivel03
        if (posicao == 9)
        {
            Nv02Barco04A.SetActive(false);
            Nv02Barco04B.SetActive(false);

            Nv03Barco01A.SetActive(false);
            Nv03Barco01B.SetActive(true);
        }
        if (posicao == 10)
        {
            Nv03Barco01A.SetActive(false);
            Nv03Barco01B.SetActive(false);

            Nv03Barco02A.SetActive(false);
            Nv03Barco02B.SetActive(true);
        }
        if (posicao == 11)
        {
            Nv03Barco02A.SetActive(false);
            Nv03Barco02B.SetActive(false);

            Nv03Barco03A.SetActive(false);
            Nv03Barco03B.SetActive(true);
        }
        if (posicao == 12)
        {
            Nv03Barco03A.SetActive(false);
            Nv03Barco03B.SetActive(false);

            Nv03Barco04A.SetActive(false);
            Nv03Barco04B.SetActive(true);
        }

        //Nivel04
        if (posicao > 12 && posicao <= 16)
        {
            Nv04Barco01A.SetActive(false);
            Nv04Barco01B.SetActive(true);
        }

        //Nivel05
        if (posicao > 16)
        {
            Nv05Barco01A.SetActive(false);
            Nv05Barco01B.SetActive(true);
        }
    }

    public void DesativarRadarBarcos()
    {
        //Nivel01
        if (posicao == 1)
        {
            Nv01Barco01A.SetActive(true);
            Nv01Barco01B.SetActive(false);
        }
        if (posicao == 2)
        {
            Nv01Barco01A.SetActive(false);
            Nv01Barco01B.SetActive(false);

            Nv01Barco02A.SetActive(true);
            Nv01Barco02B.SetActive(false);
        }
        if (posicao == 3)
        {
            Nv01Barco02A.SetActive(false);
            Nv01Barco02B.SetActive(false);

            Nv01Barco03A.SetActive(true);
            Nv01Barco03B.SetActive(false);
        }
        if (posicao == 4)
        {
            Nv01Barco03A.SetActive(false);
            Nv01Barco03B.SetActive(false);

            Nv01Barco04A.SetActive(true);
            Nv01Barco04B.SetActive(false);
        }

        //Nivel02
        if (posicao == 5)
        {
            Nv01Barco04A.SetActive(false);
            Nv01Barco04B.SetActive(false);

            Nv02Barco01A.SetActive(true);
            Nv02Barco01B.SetActive(false);
        }
        if (posicao == 6)
        {
            Nv02Barco01A.SetActive(false);
            Nv02Barco01B.SetActive(false);

            Nv02Barco02A.SetActive(true);
            Nv02Barco02B.SetActive(false);
        }
        if (posicao == 7)
        {
            Nv02Barco02A.SetActive(false);
            Nv02Barco02B.SetActive(false);

            Nv02Barco03A.SetActive(true);
            Nv02Barco03B.SetActive(false);
        }
        if (posicao == 8)
        {
            Nv02Barco03A.SetActive(false);
            Nv02Barco03B.SetActive(false);

            Nv02Barco04A.SetActive(true);
            Nv02Barco04B.SetActive(false);
        }

        //Nivel03
        if (posicao == 9)
        {
            Nv02Barco04A.SetActive(false);
            Nv02Barco04B.SetActive(false);

            Nv03Barco01A.SetActive(true);
            Nv03Barco01B.SetActive(false);
        }
        if (posicao == 10)
        {
            Nv03Barco01A.SetActive(false);
            Nv03Barco01B.SetActive(false);

            Nv03Barco02A.SetActive(true);
            Nv03Barco02B.SetActive(false);
        }
        if (posicao == 11)
        {
            Nv03Barco02A.SetActive(false);
            Nv03Barco02B.SetActive(false);

            Nv03Barco03A.SetActive(true);
            Nv03Barco03B.SetActive(false);
        }
        if (posicao == 12)
        {
            Nv03Barco03A.SetActive(false);
            Nv03Barco03B.SetActive(false);

            Nv03Barco04A.SetActive(true);
            Nv03Barco04B.SetActive(false);
        }

        //Nivel04
        if (posicao > 12 && posicao <= 16)
        {
            Nv04Barco01A.SetActive(true);
            Nv04Barco01B.SetActive(false);
        }

        //Nivel05
        if (posicao > 16)
        {
            Nv05Barco01A.SetActive(true);
            Nv05Barco01B.SetActive(false);
        }
    }

    public void AtivarRadarMapas()
    {
        //Nivel01
        if (posicao == 1)
        {
            Mapa01.SetActive(false);
            Mapa01B.SetActive(true);
        }
        if (posicao == 2)
        {
            Mapa01.SetActive(false);
            Mapa01B.SetActive(true);
        }
        if (posicao == 3)
        {
            Mapa01.SetActive(false);
            Mapa01B.SetActive(true);
        }
        if (posicao == 4)
        {
            Mapa01.SetActive(false);
            Mapa01B.SetActive(true);
        }

        //Nivel02
        if (posicao == 5)
        {
            Mapa02.SetActive(false);
            Mapa02B.SetActive(true);
        }
        if (posicao == 6)
        {
            Mapa02.SetActive(false);
            Mapa02B.SetActive(true);
        }
        if (posicao == 7)
        {
            Mapa02.SetActive(false);
            Mapa02B.SetActive(true);
        }
        if (posicao == 8)
        {
            Mapa02.SetActive(false);
            Mapa02B.SetActive(true);
        }

        //Nivel03
        if (posicao == 9)
        {
            Mapa03.SetActive(false);
            Mapa03B.SetActive(true);
        }
        if (posicao == 10)
        {
            Mapa03.SetActive(false);
            Mapa03B.SetActive(true);
        }
        if (posicao == 11)
        {
            Mapa03.SetActive(false);
            Mapa03B.SetActive(true);
        }
        if (posicao == 12)
        {
            Mapa03.SetActive(false);
            Mapa03B.SetActive(true);
        }

        //Nivel04
        if (posicao > 12 && posicao <= 16)
        {
            Mapa04.SetActive(false);
            Mapa04B.SetActive(true);
        }

        //Nivel05
        if (posicao > 16)
        {
            Mapa05.SetActive(false);
            Mapa05B.SetActive(true);
        }
    }

    public void DesativarRadarMapas()
    {
        //Nivel01
        if (posicao == 1)
        {
            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (posicao == 2)
        {
            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (posicao == 3)
        {
            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (posicao == 4)
        {
            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }

        //Nivel02
        if (posicao == 5)
        {
            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (posicao == 6)
        {
            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (posicao == 7)
        {
            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (posicao == 8)
        {
            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }

        //Nivel03
        if (posicao == 9)
        {
            Mapa03.SetActive(true);
            Mapa03B.SetActive(false);
        }
        if (posicao == 10)
        {
            Mapa03.SetActive(true);
            Mapa03B.SetActive(false);
        }
        if (posicao == 11)
        {
            Mapa03.SetActive(true);
            Mapa03B.SetActive(false);
        }
        if (posicao == 12)
        {
            Mapa03.SetActive(true);
            Mapa03B.SetActive(false);
        }

        //Nivel04
        if (posicao > 12 && posicao <= 16)
        {
            Mapa04.SetActive(true);
            Mapa04B.SetActive(false);
        }

        //Nivel05
        if (posicao > 16)
        {
            Mapa05.SetActive(true);
            Mapa05B.SetActive(false);
        }
    }

    public void btnAlt1()
    {
        Alternativaescolhida.text = Alternativa1.text;

        if (Alternativa1.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 10;
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
        TxtAcerto01.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt2()
    {
        Alternativaescolhida.text = Alternativa2.text;

        if (Alternativa2.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 10;
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
        TxtAcerto01.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt3()
    {
        Alternativaescolhida.text = Alternativa3.text;

        if (Alternativa3.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 10;
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
        TxtAcerto01.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void btnAlt4()
    {
        Alternativaescolhida.text = Alternativa4.text;

        if (Alternativa4.text == AlternativaCorreta.text)
        {
            Acertos++;

            int i = Acertos * 10;
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
        TxtAcerto01.text = Acertos.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(true);
    }

    public void AtivarRadar()
    {
        AtivarRadarInimigo();
        AtivarRadarBarcos();
        AtivarRadarMapas();
    }

    public void DesativarRadar()
    {
        DesativarRadarInimigo();
        DesativarRadarBarcos();
        DesativarRadarMapas();
    }

    public void btnProxima()
    {
        NumQuestao++;
        x++;
        posicao++;

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
            NumQuestao = 21;
        }

        if (NumQuestao == 2)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Subnivel = 2;

            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (NumQuestao == 3)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (NumQuestao == 4)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Mapa01.SetActive(true);
            Mapa01B.SetActive(false);
        }
        if (NumQuestao == 5)
        {
            ConsultarIds();

            Debug.Log("Nivel 6 - Pos:" + x + " = " + idQuestão);

            PainelMeio1.SetActive(true);

            ZerarInimigos();
            ZerarBarcos();

            Mapa01.SetActive(false);
            Mapa01B.SetActive(false);

            Mapa02.SetActive(true);

            x = 0;

            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);
        }
        if (NumQuestao == 6)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Subnivel = 3;

            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (NumQuestao == 7)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (NumQuestao == 8)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();

            Mapa02.SetActive(true);
            Mapa02B.SetActive(false);
        }
        if (NumQuestao == 9)
        {
            ConsultarIds();

            Debug.Log("Nivel 6 - Pos:" + x + " = " + idQuestão);

            PainelMeio2.SetActive(true);

            ZerarInimigos();
            ZerarBarcos();

            Mapa02.SetActive(false);
            Mapa02B.SetActive(false);

            Mapa03.SetActive(true);

            x = 0;

            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);
        }
        if (NumQuestao > 9 && NumQuestao < 13)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();
            DesativarRadarInimigo();

            Subnivel = 4;

            Mapa03.SetActive(true);
            Mapa03B.SetActive(false);
        }
        if (NumQuestao == 13)
        {
            ConsultarIds();

            Debug.Log("Nivel 6 - Pos:" + x + " = " + idQuestão);

            PainelMeio3.SetActive(true);

            ZerarInimigos();
            ZerarBarcos();

            Mapa03.SetActive(false);
            Mapa03B.SetActive(false);

            Mapa04.SetActive(true);

            x = 0;

            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);
        }
        if (NumQuestao > 13 && NumQuestao < 17)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();
            DesativarRadarInimigo();

            Subnivel = 5;

            Mapa04.SetActive(true);
            Mapa04B.SetActive(false);
        }
        if (NumQuestao == 17)
        {
            ConsultarIds();

            Debug.Log("Nivel 6 - Pos:" + x + " = " + idQuestão);

            PainelMeio4.SetActive(true);

            ZerarInimigos();
            ZerarBarcos();

            Mapa04.SetActive(false);
            Mapa04B.SetActive(false);

            Mapa05.SetActive(true);

            x = 0;

            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);
        }
        if (NumQuestao > 17 && NumQuestao <= 20)
        {
            idQuestão = ids[x];
            Debug.Log("Nivel 6 - subnivel " + Subnivel + "- Pos:" + x + " = " + idQuestão);

            ChamarQuestao();

            ZerarInimigos();
            ZerarBarcos();

            DesativarRadarBarcos();
            DesativarRadarInimigo();

            Mapa05.SetActive(true);
            Mapa05B.SetActive(false);
        }

        if (NumQuestao > 20)
        {
            AcertosFinal.text = Acertos.ToString();

            PainelFinal.SetActive(true);
            if (Acertos > 14)
            {
                Aprovado.SetActive(true);

                Pontos = Acertos * 10;
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

        DesativarRadarInimigo();

        NumeroQuestao.text = NumQuestao.ToString();
        TxtQtdQuestoesRespond.text = NumQuestao.ToString();

        PainelResposta.SetActive(false);
        Correto.SetActive(false);
        Incorreto.SetActive(false);
    }

    public void BtnFinalizar()
    {

        if (Acertos > 14)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(18);
        }

        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        Nivel = 6;
        Subnivel = 1;
        NumQuestao = 1;
        Acertos = 0;
        Pontos = 0;

    }

}
