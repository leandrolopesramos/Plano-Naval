using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int id;
    public static int score;
    public static int nivel;
    public static string acesso = "http://localhost/sqlconnect/";

    /* http: //gse.conquista.ifba.edu.br/software/planonaval/arquivosPHP*/

    public static bool login { get { return username != null; } }

    public static void logOut () { username = null; }

}
