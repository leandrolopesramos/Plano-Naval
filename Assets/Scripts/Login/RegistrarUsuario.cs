using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System;

public class RegistrarUsuario : MonoBehaviour
{
    public InputField CampoNome;
    public InputField CampoSerie;
    public InputField CampoDia;
    public InputField CampoMes;
    public InputField CampoAno;
    public InputField CampoSexo;
    public InputField CampoEmail;
    public InputField CampoUsuario;
    public InputField CampoSenha;

    public Button BtnCadastrar;
    public Button BtnVoltar;
    public Button BtnPainelContinuar;
    public Button BtnPainelVoltar;
    
    public GameObject x, y, emailIncorreto, usuarioOuSenha, campoVazio;

    public Text erro;

    public void VoltaInicio()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ChamarRegistro()
    {
        bool email = ValidarEmail(CampoEmail.text);

        ;
        if (CampoNome.text == string.Empty || CampoSerie.text == string.Empty || CampoSexo.text == string.Empty || CampoDia.text == string.Empty ||
                    CampoEmail.text == string.Empty || CampoUsuario.text == string.Empty || CampoSenha.text == string.Empty)
        {
            campoVazio.SetActive(true);
        }

        else
        {
            if (CampoUsuario.text.Length > 5 && CampoSenha.text.Length > 5)
            {
                if (email == true)
                {
                    int dia = int.Parse(CampoDia.text);
                    int mes = int.Parse(CampoMes.text);
                    int ano = int.Parse(CampoAno.text);

                    if (dia < 32 && dia > 0)
                    {
                        if (mes < 13 && mes > 0)
                        {
                            if (ano > 1930 && ano < 2019)
                            {
                                StartCoroutine(Registrar());
                            }
                            else
                            {
                                erro.text = "Data de Nascimento Inválida (Ano Inválido)";
                                y.SetActive(true);
                            }
                        }
                        else
                        {
                            erro.text = "Data de Nascimento Inválida (Mês Inválido)";
                            y.SetActive(true);
                        }
                    }
                    else
                    {
                        erro.text = "Data de Nascimento Inválida (Dia Inválido)";
                        y.SetActive(true);
                    }
                }
                else
                {
                    emailIncorreto.SetActive(true);
                }
            }

            else
            {
                usuarioOuSenha.SetActive(true);
            }
        }

    }

    IEnumerator Registrar()
    {
        WWWForm form = new WWWForm();

        string nasc = CampoDia.text + "/" + CampoMes.text + "/" + CampoAno.text;

        form.AddField("nome", CampoNome.text);
        form.AddField("idade", nasc );
        form.AddField("usuario", CampoUsuario.text);
        form.AddField("senha", CampoSenha.text);
        form.AddField("sexo", CampoSexo.text);
        form.AddField("serie", CampoSerie.text);
        form.AddField("email", CampoEmail.text); 

        WWW www = new WWW(DBManager.acesso + "register.php", form);

        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Usuário criado com sucesso!");

            x.SetActive(true);

            BtnCadastrar.interactable = false;
            BtnCadastrar.interactable = false;
        }
        else
        {
            y.SetActive(true);

            erro.text = "Usuário não foi criado. Erro#" + www.text;
        }
    }

    public bool ValidarEmail(string email){

        bool emailValido = false;

        string emailRegex = string.Format("{0}{1}", @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

        try
        {
            emailValido = Regex.IsMatch(
                email,
                emailRegex);
        }
        catch (RegexMatchTimeoutException)
        {
            emailValido = false;
        }

        return emailValido;
    }

    public void VoltaCadastro()
    {
        y.SetActive(false);
    }

    public void VoltaEmailIncorreto()
    {
        emailIncorreto.SetActive(false);
    }

    public void VoltaUsuarioOuSenha()
    {
        usuarioOuSenha.SetActive(false);
    }

    public void VoltaCampoVazio()
    {
        campoVazio.SetActive(false);
    }
}
