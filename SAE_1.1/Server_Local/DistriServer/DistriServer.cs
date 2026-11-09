
using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        string dossier = AppDomain.CurrentDomain.BaseDirectory;

        HttpListener serveur = new HttpListener();
        serveur.Prefixes.Add("http://localhost:8080/");

        try
        {
            serveur.Start();

            Console.WriteLine("Serveur est démarré !");
            Console.WriteLine("Adresse : http://localhost:8080/");
            Console.WriteLine("Appuyez Ctrl+c pour arrêter le serveur.");

            while (true)
            {
                HttpListenerContext contexte = serveur.GetContext();

                try
                {
                    TraiterRequete(contexte, dossier);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);

                    EnvoyerReponse(
                        contexte,
                        500,
                        "{\"erreur\":\"Erreur interne du serveur\"}"
                    );
                }
            }
        }
        catch (HttpListenerException ex)
        {
            Console.WriteLine("Erreur du serveur : " + ex.Message);
        }
        finally
        {
            serveur.Close();
        }
    }

    static void TraiterRequete(
        HttpListenerContext contexte,
        string dossier)
    {
        string methode = contexte.Request.HttpMethod;
        string chemin = contexte.Request.Url.AbsolutePath;

        Console.WriteLine(methode + " " + chemin);

        if (methode != "GET")
        {
            EnvoyerReponse(
                contexte,
                405,
                "{\"erreur\":\"Methode non autorisée\"}"
            );
            return;
        }

		char[] delimiteurs = {'/'};
        string[] morceaux = chemin.Split(delimiteurs);

        // Exemple : /distri/29
        if (morceaux.Length != 3 ||
            morceaux[1] != "distri" ||
            morceaux[2].Length == 0)
        {
            EnvoyerReponse(
                contexte,
                404,
                "{\"erreur\":\"Route inconnue\"}"
            );
            return;
        }

        int numero;

        if (!int.TryParse(morceaux[2], out numero) ||
            numero <= 0)
        {
            EnvoyerReponse(
                contexte,
                400,
                "{\"erreur\":\"Numero de distributeur invalide\"}"
            );
            return;
        }

        string nomFichier = "distri" + numero + ".json";
        string cheminFichier = Path.Combine(dossier, nomFichier);

        if (!File.Exists(cheminFichier))
        {
            EnvoyerReponse(
                contexte,
                404,
                "{\"erreur\":\"Fichier " + nomFichier + " introuvable\"}"
            );
            return;
        }

        string json = File.ReadAllText(cheminFichier);
        byte[] donnees = Encoding.UTF8.GetBytes(json);
		
		AjouterEntetes(contexte.Response);

        contexte.Response.StatusCode = 200;
        contexte.Response.ContentType =
            "application/json; charset=utf-8";
        contexte.Response.ContentLength64 = donnees.Length;

        contexte.Response.OutputStream.Write(
            donnees, 0, donnees.Length
        );
    }

    static void EnvoyerReponse(
        HttpListenerContext contexte,
        int code,
        string json)
    {
        byte[] donnees = Encoding.UTF8.GetBytes(json);
		
		AjouterEntetes(contexte.Response);

        contexte.Response.StatusCode = code;
        contexte.Response.ContentType =
            "application/json; charset=utf-8";
        contexte.Response.ContentLength64 = donnees.Length;

        contexte.Response.OutputStream.Write(
            donnees, 0, donnees.Length
        );
    }
	
	static void AjouterEntetes(HttpListenerResponse reponse)
	{
		reponse.ContentType = "application/json; charset=utf-8";

		reponse.AddHeader(
			"Access-Control-Allow-Origin", "*"
		);
	}	
}
