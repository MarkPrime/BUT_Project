using System;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using BackEnd.Structures;



class Distributeur
{    
    //Procedures pour ajouter des info
    static void ajouterConfig(ref Dist distributeur)
    {
        distributeur.configuration.Add("nombreLignes" , 4);
        distributeur.configuration.Add("nombreColonnes" , 4);
        distributeur.configuration.Add("temperatureBoissons" , 6);
        distributeur.configuration.Add("temperaturePlats" , 4);
    }
    static void ajouterProduit(ref Dist distributeur, Produit produit)
    {
        distributeur.produits.Add(produit);
    }
    static void ajouterEmplacement(ref Dist distributeur, Emplacement emplacement)
    {
        distributeur.emplacements.Add(emplacement);
    }
    static void ajouterPiece(ref Dist distributeur, Piece piece)
    {
        distributeur.stockPieces.Add(piece);
    }

    static void Main()
    {
        /*Declarations*/ 
        Dist dist1;
        Produit produit1;
        Emplacement emp1;
        Piece piece1;
        string objectDistributeur;

        // Verification si le fichier existe
        if (!File.Exists("data.json"))
        {
            File.WriteAllText("data.json", "{}");
        }

        // Les configurations pour Json 
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true, // Permet de ecrire les objets
            IncludeFields = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Permet de encoder "â"
        };
        
        // Creation des Structures
        dist1 = new Dist("DIST-001", "IUT Robert Shuman", "Bâtiment A Neuvic", true, 7);
        produit1 = new Produit("BOI001", "Eau minérale", "Boutelle d'eau minérale 50cl", "Boisson", 1.00m);
        emp1 = new Emplacement("A1", "BOI001", 8, 10);
        piece1 = new Piece(0.20m, 20);


        // Ajout le Dictionaire Configuration
        ajouterConfig(ref dist1);
        ajouterProduit(ref dist1, produit1);
        ajouterEmplacement(ref dist1, emp1);
        ajouterPiece(ref dist1, piece1);
        

        // Affichage de Distributeur en Struct
        Console.WriteLine(dist1.nomOrganisation + " " + dist1.actif + " " + dist1.configuration["nombreColonnes"]);

        // Serialisation dans le Objet Json les Structures
        objectDistributeur = JsonSerializer.Serialize(dist1, options);

        // Ecrire Objet choisi dans le Fichier Json
        File.WriteAllText("data.json", objectDistributeur);
    }
}