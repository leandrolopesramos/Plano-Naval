    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField CampoUsuario, CampoSenha;
    public Button BtnNovaConta, BtnLogin;
    public GameObject y, PainelSobre;
    public Text erro;

    public void ChamarLogin()
    {
        StartCoroutine(Logar());
    }

    public void IrNovaConta() //Botão NovaConta
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Logar()
    {
        WWWForm form = new WWWForm();

        form.AddField("usuario", CampoUsuario.text);
        form.AddField("senha", CampoSenha.text);

        WWW www = new WWW(DBManager.acesso + "login.php", form);

        yield return www;

        if (www.text[0] == '0')
        {
            DBManager.username = CampoUsuario.text;
            DBManager.id = int.Parse(www.text.Split('\t')[1]);
            DBManager.score = int.Parse(www.text.Split('\t')[2]);
            DBManager.nivel = int.Parse(www.text.Split('\t')[3]);

            Debug.Log("Login realizado com sucesso!");

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            y.SetActive(true);

            erro.text = "Falha no login: " + www.text;
            Debug.Log("Falha no login: Erro#" + www.text);
        }
    }

    public void VerificarInputs()
    {
        BtnLogin.interactable = (CampoUsuario.text.Length >= 4 && CampoSenha.text.Length >= 4);
    }

    public void Voltar()
    {
        y.SetActive(false);
    }

    public void SobrePainel()
    {
        PainelSobre.SetActive(true);
    }

    public void VoltarPainel()
    {
        PainelSobre.SetActive(false);
    }
}
