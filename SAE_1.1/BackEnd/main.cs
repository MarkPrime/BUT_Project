#:property PublishAot=false
using System;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;

class Distributeur
{
    // Structure pour les products
    struct Produit
    {
        public string reference;
        public string nom;
        public string description;
        public string categorie;
        public double prix;
        public List<string> allergenes;
        public Produit(string refer, string nm, string dsc, string cat, double px)
        {
            reference = refer;
            nom = nm;
            description = dsc;
            categorie = cat;
            prix = px;
            allergenes = new List<string>();
        }
    }
    // Structure generale de Distributeur
    struct Dist
    {
        public string id;
        public string nomOrganisation;
        public string localisation;
        public bool actif;
        public int modePaiment;
        public Dictionary<string, int> configuration;
        public Dictionary<int, Produit> produits;
        public Dist(string idi, string nom, string loc, bool ac, int mode)
        {
            id = idi;
            nomOrganisation = nom;
            localisation = loc;
            actif = ac;
            modePaiment = mode;
            configuration = new  Dictionary<string, int>();
            produits = new Dictionairy<int, Produit>();
        }
    }
    
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
        distributeur.produits.Add(1, produit);
    }

    static void Main()
    {
        /*Declarations*/ 
        Dist dist1;
        Produit produit1;
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
        produit1 = new Produit("BOI001", "Eau minérale", "Boutelle d'eau minérale 50cl", "Boisson", 1);


        // Ajout le Dictionaire Configuration
        ajouterConfig(ref dist1);
        ajouterProduit(ref dist1, produit1);
        

        // Affichage de Distributeur en Struct
        Console.WriteLine(dist1.nomOrganisation + " " + dist1.actif + " " + dist1.configuration["nombreColonnes"]);

        // Serialisation dans le Objet Json les Structures
        objectDistributeur = JsonSerializer.Serialize(dist1, options);

        // Ecrire Objet choisi dans le Fichier Json
        File.WriteAllText("data.json", objectDistributeur);
    }
}