using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVideo : MonoBehaviour
{

    public Text DisplayUsuario;
    public Text DisplayPontos;

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        DisplayUsuario.text = DBManager.username;
        DisplayPontos.text = "" + DBManager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();

        form.AddField("usuario", DBManager.username);
        form.AddField("pontos", DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);

        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Jogo Salvo!");
            //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log( "Usuário não foi criado. Erro#" + www.text);
        }
    }

    public void IncreaseScore()
    {
        DBManager.score++;
        DisplayPontos.text = "" + DBManager.score;
    }
}
