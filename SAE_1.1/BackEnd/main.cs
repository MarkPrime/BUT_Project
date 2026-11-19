using System;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using BackEnd.structures;
using BackEnd.procedures;
namespace BackEnd.main;

/*--------------------------------------------

Application de configuratoin des distributeurs

---------------------------------------------*/

public class Distributeur
{
    public static void Main()
    {
        /*Declarations structures*/ 
        Dist dist;
        Produit produit1;
        Emplacement emp1;
        Piece piece1;
        List<Dist> lesDist;

        /*Declarations variables*/ 
        string objectsDistributeurs;
        bool exit = false;
        bool confirmer = false;
        string? saisie;
        string? id_dist = null;
        string? nomOrgan;
        string? loc;
        bool? act;
        int? md;
        int nbDist = 0;
        lesDist = new List<Dist>();
        dist = new Dist(id_dist);

        // Les configurations pour Json Serializer
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
        };

        while (!exit)
        {
            /*Premiere affichage*/
            Menu.afficherMenuInit();
            if (!File.Exists("config.json"))
            {
                Console.WriteLine("X\t[WARNING]\nX\tLe fichier \"config.json\" n'a pas été détecté.");
                Console.WriteLine("X\tPour créer un, taper [1].\nX");
                Console.WriteLine("X\tTaper [x] pour quiter.\nX");
                Console.Write("X-----> ");
                saisie = Console.ReadLine();
                switch (saisie)
                {
                    case "1":
                        /*Creation de fichier*/
                        File.WriteAllText("config.json", "{}");
                        while (!confirmer)
                        {
                            /*Definition des variables*/
                            id_dist = null;
                            nomOrgan = null;
                            loc = null;
                            act = null;
                            md = null;


                            /*Entrer le id de distribureur*/
                            Menu.afficherMenuInit();
                            Console.WriteLine("X\tEntrer id de votre distributeur.\nX");
                            Console.Write("X-----> ");
                            id_dist = Console.ReadLine();
                            DistriConf.modifDistributeur(ref dist,id_dist,nomOrgan,loc,act,md);

                            /*Entrer l'organisation*/
                            Menu.afficherMenuInit();
                            Menu.afficherMenuDistGeneral(dist);
                            Console.WriteLine("X\tEntrer nom de organisation.\nX");
                            Console.Write("X-----> ");
                            nomOrgan = Console.ReadLine();
                            DistriConf.modifDistributeur(ref dist,id_dist,nomOrgan,loc,act,md);
                            
                            /*Entrer localisation*/
                            Menu.afficherMenuInit();
                            Menu.afficherMenuDistGeneral(dist);
                            Console.WriteLine("X\tEntrer localisation de distributeur.\nX");
                            Console.Write("X-----> ");
                            loc = Console.ReadLine();
                            DistriConf.modifDistributeur(ref dist,id_dist,nomOrgan,loc,act,md);

                            /*Entrer is_actif*/
                            while (!exit)
                            {
                                Menu.afficherMenuInit();
                                Menu.afficherMenuDistGeneral(dist);
                                Console.WriteLine("X\tEntrer état de votre distributeur.\nX\t[Y] - en servise,\nX\t[X] - hors servise\nX");
                                Console.Write("X-----> ");
                                saisie = Console.ReadLine();
                                switch (saisie)
                                {
                                    case "Y":
                                        act = true;
                                        exit = true;
                                        break;
                                    case "X":
                                        act = false;
                                        exit = true;
                                        break;
                                    default:
                                        Console.WriteLine("X\tLe saisie n'est pas correct...");
                                        Thread.Sleep(2000);
                                        break;
                                }
                                DistriConf.modifDistributeur(ref dist,id_dist,nomOrgan,loc,act,md);
                            }
                            exit = false;

                            /*Entrer modePaiment*/
                            while (!exit)
                            {
                                Menu.afficherMenuInit();
                                Menu.afficherMenuDistGeneral(dist);
                                Menu.afficherMenuModePaiment();
                                Console.Write("X-----> ");
                                saisie = Console.ReadLine();
                                switch (saisie)
                                {
                                    case "1":
                                        md = 1;
                                        exit = true;
                                        break;
                                    case "2":
                                        md = 2;
                                        exit = true;
                                        break;
                                    case "3":
                                        md = 3;
                                        exit = true;
                                        break;
                                    case "4":
                                        md = 4;
                                        exit = true;
                                        break;
                                    case "5":
                                        md = 5;
                                        exit = true;
                                        break;
                                    case "6":
                                        md = 6;
                                        exit = true;
                                        break;
                                    case "7":
                                        md = 7;
                                        exit = true;
                                        break;
                                    default:
                                        Console.WriteLine("X\tLe saisie n'est pas correct...");
                                        Thread.Sleep(2000);
                                        break;
                                }
                                DistriConf.modifDistributeur(ref dist,id_dist,nomOrgan,loc,act,md);
                            }
                            exit = false;

                            /*Confirmation*/
                            while (!exit)
                            {
                                Menu.afficherMenuInit();
                                Menu.afficherMenuDistGeneral(dist);
                                Console.WriteLine("X\tVoulez-vous confirmer?\nX\t[Y] - OUI,\nX\t [X] - NON\nX");
                                Console.Write("X-----> ");
                                saisie = Console.ReadLine();
                                switch (saisie)
                                {
                                    case "Y":
                                        Console.WriteLine("X\tEcriture information dans fichier...");
                                        // Ajout de distributeur dans la liste
                                        DistriConf.ajouterDistributeur(lesDist, dist);
                                        // Serialisation dans le Objet Json les Structures
                                        objectsDistributeurs = JsonSerializer.Serialize(lesDist, options);
                                        // Ecrire Objet choisi dans le Fichier Json
                                        File.WriteAllText("config.json", objectsDistributeurs);
                                        confirmer = true;
                                        exit = true;
                                        Thread.Sleep(3000);
                                        break;
                                    case "X":
                                        confirmer = false;
                                        exit = true;
                                        break;
                                    default:
                                        Console.WriteLine("X\tLe saisie n'est pas correct...");
                                        Thread.Sleep(2000);
                                        break;
                                }
                            }
                            exit = false;
                        }
                        break;
                    case "x":
                        Console.WriteLine("X\tSortie...");
                        Thread.Sleep(1000);
                        exit = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("X\tLe saisie n'est pas correct...");
                        Thread.Sleep(2000);
                        break;
                }
            }
            else
            {
                Menu.afficherMenuInit();
                Thread.Sleep(3000);
                exit = true;
            }
        }

        // Creation des Structures
        produit1 = new Produit("BOI001", "Eau minérale", "Boutelle d'eau minérale 50cl", "Boisson", 1.00m);
        emp1 = new Emplacement("A1", "BOI001", 8, 10);
        piece1 = new Piece(0.20m, 20);


        // Ajout le Dictionaire Configuration
        //DistriConf.ajouterConfig(ref dist);
        //DistriConf.ajouterProduit(ref dist, produit1);
        //DistriConf.ajouterEmplacement(ref dist, emp1);
        //DistriConf.ajouterPiece(ref dist, piece1);
    }
}