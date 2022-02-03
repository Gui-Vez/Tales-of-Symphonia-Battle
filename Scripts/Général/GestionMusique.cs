using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GestionMusique : MonoBehaviour
{
    // Valeur de l'index de la musique
    int musiqueIndex;
    // Valeur du volume de la musique
    float VolumeMusique;

    // Musique "The End of a Thought"
    public GameObject musique1;
    // Musique "Beat the Angel"
    public GameObject musique2;
    // Musique "Fighting of the Spirits"
    public GameObject musique3;


    void Start()
    {
        // Prendre une valeur de 1 à 3
        musiqueIndex = UnityEngine.Random.Range(1, 4);
        // Ajouter une valeur au volume
        VolumeMusique = 0.25f;
    }

    
    void Update()
    {
        // Selon l'index de la musique,
        switch (musiqueIndex)
        {
            case 1:

                // Activer la musique #1
                musique1.SetActive(true);
                // Appliquer le volume à la musique
                musique1.GetComponent<AudioSource>().volume = VolumeMusique;

                break;


            case 2:

                // Activer la musique #2
                musique2.SetActive(true);
                // Appliquer le volume à la musique
                musique2.GetComponent<AudioSource>().volume = VolumeMusique;

                break;


            case 3:

                // Activer la musique #3
                musique3.SetActive(true);
                // Appliquer le volume à la musique
                musique3.GetComponent<AudioSource>().volume = VolumeMusique;

                break;
        }


        // Si la partie est terminée,
        if (FinPartie.partieFinie)
        {
            // Si le volume de la musique est plus haut que le nombre demandé,
            if (VolumeMusique > 0.1f)
            {
                // Baisser le volume de la musique progressivement
                VolumeMusique = VolumeMusique - 0.0025f;
            }

            // Sinon,
            else
            {
                // Laisser le volume à ce niveau
                VolumeMusique = 0.1f;
            }
        }
    }
}
