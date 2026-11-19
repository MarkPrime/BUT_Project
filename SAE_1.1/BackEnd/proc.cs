using System;
using System.Text;
using BackEnd.main;
using BackEnd.structures;

/*---------------------------------

Fichier dedier que a des procedures

----------------------------------*/

namespace BackEnd.procedures;
public class DistriConf
{
    public static void ajouterDistributeur(List<Dist> list, Dist dist)
    {
        list.Add(dist);
    }
    public static void ajouterConfig(ref Dist distributeur)
    {
        distributeur.configuration.Add("nombreLignes" , 4);
        distributeur.configuration.Add("nombreColonnes" , 4);
        distributeur.configuration.Add("temperatureBoissons" , 6);
        distributeur.configuration.Add("temperaturePlats" , 4);
    }
    public static void ajouterProduit(ref Dist distributeur, Produit produit)
    {
        distributeur.produits.Add(produit);
    }
    public static void ajouterEmplacement(ref Dist distributeur, Emplacement emplacement)
    {
        distributeur.emplacements.Add(emplacement);
    }
    public static void ajouterPiece(ref Dist distributeur, Piece piece)
    {
        distributeur.stockPieces.Add(piece);
    }
    public static void modifDistributeur(ref Dist distributeur, string i, string? org, string? loc, bool? ac, int? md)
    {
        distributeur.id = i;
        distributeur.nomOrganisation = org;
        distributeur.localisation = loc;
        distributeur.actif = ac;
        distributeur.modePaiment = md;
    }
}

public class Menu
{
    public static void afficherMenuInit()
    {
        Console.Clear();
        Console.WriteLine("X-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-Y-X\nX");
        Console.WriteLine("X\tGestion des distributeurs\nX");
        Console.WriteLine("X==============================================================\nX");
    }
    public static void afficherMenuPrincipale()
    {
        Console.WriteLine("X\t[1] - Voir configuration de distributeur.");
        Console.WriteLine("X\t[2] - Modifier la configuration de distributeur.");
        Console.WriteLine("X\t[3] - Supprimer la configuration.");
        Console.WriteLine("X\t[x] - Sortir de Application.\nX");
    }
    public static void afficherMenuDistGeneral(Dist distributeur)
    {
        Console.WriteLine($"X\tId de Distributeur : {distributeur.id}");
        Console.WriteLine($"X\tNom d'organisation : {distributeur.nomOrganisation}");
        Console.WriteLine($"X\tLocalisation : {distributeur.localisation}");
        Console.WriteLine($"X\tEn service : {distributeur.actif}");
        Console.WriteLine($"X\tModes de paiment : {distributeur.modePaiment}\nX");
    }
    public static void afficherMenuModePaiment()
    {
        Console.WriteLine("X\t[1] - En liquide uniquement.");
        Console.WriteLine("X\t[2] - Par CB uniquement.");
        Console.WriteLine("X\t[3] - En liquide + CB.");
        Console.WriteLine("X\t[4] - Sans contact uniquement.");
        Console.WriteLine("X\t[5] - En liquide + sans contact.");
        Console.WriteLine("X\t[6] - Par CB + sans contact.");
        Console.WriteLine("X\t[7] - Tout les modes de paiment.\nX");
    }
}
