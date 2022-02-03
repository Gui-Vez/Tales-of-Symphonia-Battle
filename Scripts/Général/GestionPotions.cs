using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GestionPotions : MonoBehaviour
{
    // Clip audio de la potion
    public AudioClip SonPotion;

    // Valeur de la position X de la potion
    float potionPositionX;
    // Valeur de la position Y de la potion
    float potionPositionY;

    // Temps requis pour ajouter/enlever une potion
    float tempsRequis;


    // Fonction qui débute à la première frame
    private void Start()
    {
        // Désactiver l'objet
        gameObject.SetActive(false);

        // Modifier la valeur de la position X aléatoirement
        potionPositionX = UnityEngine.Random.Range(-7, 8);
        // Modifier la valeur de la position Y aléatoirement
        potionPositionY = UnityEngine.Random.Range(-1, 2);

        // Donner un temps aléatoire pour la gestion de la potion
        tempsRequis = UnityEngine.Random.Range(10, 50);

        // Appeler la fonction permettant d'ajouter des potions après x temps
        Invoke("AjouterPotion", tempsRequis);
    }

    // Fonction qui se joue à chaque frame
    private void Update()
    {
        // Modifier la valeur de la position X aléatoirement
        potionPositionX = UnityEngine.Random.Range(-7, 8);

        // Modifier la valeur de la position Y aléatoirement
        potionPositionY = UnityEngine.Random.Range(-1, 2);

        // Si la partie est finie,
        if (FinPartie.partieFinie)
        {
            // Enlever toutes les potions
            EnleverPotion();
        }
    }

    // Lorsqu'un personnage touche la potion,
    IEnumerator OnTriggerEnter2D(Collider2D trigger)
    {
        // Faire jouer l'effet sonore de la potion
        GetComponent<AudioSource>().PlayOneShot(SonPotion, 0.5f);

        // Attendre avant d'exécuter la prochaine commande
        yield return new WaitForSeconds(0.15f);

        // Désactiver la potion
        gameObject.SetActive(false);

        // Appeler la fonction qui permet d'ajouter la potion après le double du temps
        Invoke("AjouterPotion", tempsRequis * 2);
    }

    // Fonction permettant d'ajouter une potion
    void AjouterPotion()
    {
        // Si la partie n'est pas encore terminée,
        if (!FinPartie.partieFinie)
        {
            // Ajouter la potion à l'écran
            gameObject.SetActive(true);

            // Modifier la position aléatoirement
            transform.position = new Vector2(potionPositionX, potionPositionY);

            // Appeler la fonction qui permet d'enlever la potion après le double du temps
            Invoke("EnleverPotion", (tempsRequis * 2));
        }
    }

    // Fonction permettant d'enlever la potion
    void EnleverPotion()
    {
        // Enlever la potion à l'écran
        gameObject.SetActive(false);

        // Appeler la fonction permettant d'ajouter une potion après x temps
        Invoke("AjouterPotion", tempsRequis);
    }
}