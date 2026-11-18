using System;
using System.Text;

namespace BackEnd.Structures;

//Structure generale de Distributeur
public struct Dist
{
    public string id;
    public string nomOrganisation;
    public string localisation;
    public bool actif;
    public int modePaiment;
    public Dictionary<string, int> configuration;
    public List<Produit> produits;
    public List<Emplacement> emplacements;
    public List<Piece> stockPieces;
    public Dist(string idi, string nom, string loc, bool ac, int mode)
    {
        id = idi;
        nomOrganisation = nom;
        localisation = loc;
        actif = ac;
        modePaiment = mode;
        configuration = new  Dictionary<string, int>();
        produits = new List<Produit>();
        emplacements = new List<Emplacement>();
        stockPieces = new List<Piece>();
    }
}

//Structure des emplacement
public struct Emplacement
{
    public string code;
    public string referenceProduit;
    public int stock;
    public int capacite;
    public Emplacement(string cd, string refer, int stk, int cap)
    {
        code = cd;
        referenceProduit = refer;
        stock = stk;
        capacite = cap;
    }
}

//Structure pour les products
public struct Produit
{
    public string reference;
    public string nom;
    public string description;
    public string categorie;
    public decimal prix;
    public List<string> allergenes;
    public Produit(string refer, string nm, string dsc, string cat, decimal px)
    {
        reference = refer;
        nom = nm;
        description = dsc;
        categorie = cat;
        prix = px;
        allergenes = new List<string>();
    }
}

//Structure pour les pieces
public struct Piece
{
    public decimal valeur;
    public int quantite;
    public Piece(decimal val, int quant)
    {
        valeur = val;
        quantite = quant;
    }
}