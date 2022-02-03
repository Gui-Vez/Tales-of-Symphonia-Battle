using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GestionHitbox : MonoBehaviour
{
    // Point d'attaque du personnage
    public Transform pointAttaque;
    // Point du sol
    public Transform pointSol;

    bool lloydPeutToucher;
    bool kratosPeutToucher;

    // Layer permettant de détecter qui est l'adversaire
    public LayerMask layerAdversaire;


    // Portée de l'attaque (rayon du hitbox)
    public float porteeAttaque = 0.5f;



    // Fonction répétant à chaque frame
    void Update()
    {
        // Appeler la fonction permettant d'attaquer
        Attaquer();

        // Si Lloyd peut toucher Kratos
        if (lloydPeutToucher)
        {
            // Activer la valeur qui permet de toucher Kratos
            ControlerKratos.estTouche = true;

            // Remettre la valeur à faux
            lloydPeutToucher = false;
        }

        // Sinon, si Kratos peut toucher Lloyd,
        else if (kratosPeutToucher)
        {
            // Activer la valeur qui permet de toucher Lloyd
            ControlerLloyd.estTouche = true;

            // Remettre la valeur à faux
            kratosPeutToucher = false;
        }
            
        // Sinon,
        else
        {
            // Activer la valeur qui fait toucher Kratos
            ControlerKratos.estTouche = false;

            // Activer la valeur qui fait toucher Lloyd
            ControlerLloyd.estTouche = false;
        }
    }

    // Fonction permettant d'attaquer
    void Attaquer()
    {
        // Détecter l'adversaire frappé dans un Array, lorsqu'il entre en contact avec la portée de l'attaque
        Collider2D[] adversaireFrappe = Physics2D.OverlapCircleAll(pointAttaque.position, porteeAttaque, layerAdversaire);

        // À chaque fois que l'adversaire est frappé,
        foreach (Collider2D adversaire in adversaireFrappe)
        {
            // Si l'adversaire est Kratos,
            if (adversaire.name == "Kratos")
            {
                // Lloyd peut toucher sa cible
                lloydPeutToucher = true;
            }

            // Si l'adversaire est Lloyd,
            if (adversaire.name == "Lloyd")
            {
                // Kratos peut toucher sa cible
                kratosPeutToucher = true;
            }
        }
    }

    // Fonction permettant de montrer visuellement le point d'attaque
    private void OnDrawGizmosSelected()
    {
        // Si le point d'attaque n'est pas choisi,
        if (pointAttaque == null)
        {
            // Retourner la commande (pour ne pas avoir d'erreurs dans la console)
            return;
        }

        // Afficher la hitbox du personnage dans l'inspecteur à l'aide du menu Gizmo
        Gizmos.DrawWireSphere(pointAttaque.position, porteeAttaque);
    }
}
