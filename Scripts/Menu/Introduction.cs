using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    // Gestion des Caméras
    public GameObject CameraIntroduction;
    public GameObject CameraInstructions;

    // Gestion des Textes
    public GameObject TexteJouer;
    public GameObject TexteInstructions;
    public GameObject TexteRetour;

    // Gestion du menu d'instructions
    public GameObject MenuInstructions;

    // Gestion de la musique d'introduction
    public AudioSource MusiqueIntro;

    // Variables
    bool PartieLancee;
    float VolumeMusique;


    // Fonction à la première frame
    private void Start()
    {
        // Réduire le volume de la musique à sa moitié
        VolumeMusique = 0.5f;
    }



    // Losque la souris entre dans la zone de texte,
    public void SourisEntreJouer()
    {
        // Changer le texte pour blanc
        TexteJouer.GetComponent<Text>().color = Color.white;
    }

    // Losque la souris sort dans la zone de texte,
    public void SourisSortJouer()
    {
        // Changer le texte pour noir
        TexteJouer.GetComponent<Text>().color = Color.black;
    }

    // Lorsque la souris clique sur la zone de texte,
    public void SourisClicJouer()
    {
        // Lancer la partie
        PartieLancee = true;
    }



    // Losque la souris entre dans la zone de texte,
    public void SourisEntreInstructions()
    {
        // Changer le texte pour blanc
        TexteInstructions.GetComponent<Text>().color = Color.white;
    }

    // Losque la souris sort dans la zone de texte,
    public void SourisSortInstructions()
    {
        // Changer le texte pour noir
        TexteInstructions.GetComponent<Text>().color = Color.black;
    }

    // Lorsque la souris clique sur la zone de texte,
    public void SourisClicInstructions()
    {
        // Afficher les instructions
        AfficherInstructions();
    }



    // Lorsque la souris clique sur la zone de texte,
    public void SourisClicRetour()
    {
        // Masquer les instructions
        MasquerInstructions();
    }



    // Fonction permettant d'afficher les instructions
    void AfficherInstructions()
    {
        // Activer la caméra
        CameraInstructions.SetActive(true);

        // Désactiver le texte qui permet de lancer le jeu
        TexteJouer.SetActive(false);

        // Désactiver le texte qui permet de montrer les instructions
        TexteInstructions.SetActive(false);

        // Activer le menu d'instructions
        MenuInstructions.SetActive(true);

        // Activer le texte qui permet de faire un retour au menu
        TexteRetour.SetActive(true);

        // Rendre la couleur du texte d'instructions à noir
        TexteInstructions.GetComponent<Text>().color = Color.black;

        // Diminuer le volume de la musique
        MusiqueIntro.GetComponent<AudioSource>().volume = 0.25f;
    }


    // Fonction permettant de masquer les instructions
    void MasquerInstructions()
    {
        // Désactiver la caméra
        CameraInstructions.SetActive(false);

        // Activer le texte qui permet de lancer le jeu
        TexteJouer.SetActive(true);

        // Activer le texte qui permet de montrer les instructions
        TexteInstructions.SetActive(true);

        // Désactiver le menu d'instructions
        MenuInstructions.SetActive(false);

        // Désactiver le texte qui permet de faire un retour au menu
        TexteRetour.SetActive(false);

        // Augmenter le volume de la musique
        MusiqueIntro.GetComponent<AudioSource>().volume = 0.5f;
    }



    // Fonction à toutes les frames
    void Update()
    {
        // Si la partie est lancée,
        if (PartieLancee)
        {
            // Rendre la couleur du texte de jeu à blanc
            TexteJouer.GetComponent<Text>().color = Color.white;

            // Diminuer la valeur du volume de la musique progressivement
            VolumeMusique = VolumeMusique - 0.0025f;

            // Diminuer le volume de la musique
            MusiqueIntro.GetComponent<AudioSource>().volume = VolumeMusique;

            // Faire lancer le jeu après 2 secondes
            Invoke("LancerJeu", 2f);
        }
    }


    // Fonction permettant de lancer la partie
    void LancerJeu()
    {
        // Lancer le jeu
        SceneManager.LoadScene(1);
    }
}