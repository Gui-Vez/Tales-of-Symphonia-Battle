using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GestionSonsLloyd : MonoBehaviour
{
    // Liste des sons de Lloyd
    public AudioClip Lloyd_Slice_1;
    public AudioClip Lloyd_Slice_2;
    public AudioClip Lloyd_Slice_3;
    public AudioClip Lloyd_SonicThrust;
    public AudioClip Lloyd_SwordRain;
    public AudioClip Lloyd_DemonFang;
    public AudioClip Lloyd_FierceDemonFang;
    public AudioClip Lloyd_Tempest;


    // Variable permettant de faire jouer les sons ou non 
    public static bool audioLloydActif;


    // Fonction à toutes les frames
    void Start()
    {
        // Activer la valeur audio
        audioLloydActif = true;

        // Créer une coroutine pour détecter le son des attaques
        StartCoroutine(DetecterActions());
    }

    // Fonction permettant de détecter lorsque Lloyd fait une action
    IEnumerator DetecterActions()
    {
        // Si la valeur est vraie,
        while (audioLloydActif)
        {
            // Faire attendre une fraction de seconde
            yield return new WaitForSeconds(0.001f);

            // Si Lloyd attaque ET qu'il ne se défend pas,
            if (ControlerLloyd.attaque && !ControlerLloyd.defend)
            {
                // Appeler la fonction audio des attaques
                SonAttaque();

                // Faire attendre pendant un intervalle de temps
                yield return new WaitForSeconds(0.75f);

                // Mettre la valeur d'attaque à faux
                ControlerLloyd.attaque = false;
            }

            // Si Lloyd saute,
            if (ControlerLloyd.saute)
            {
                // Appeler la fonction du saut
                SonSaut();

                // Faire attendre pendant un intervalle de temps
                yield return new WaitForSeconds(0.75f);

                // Mettre la valeur de saut à faux
                ControlerLloyd.saute = false;
            }
        }
    }


    // Fonction permettant de faire jouer les sons d'attaque Lloyd
    void SonAttaque()
    {
        // Jouer un son dépendamment de l'index d'attaque de Lloyd
        switch (ControlerLloyd.attaqueIndex)
        {
            case 1:
                GetComponent<AudioSource>().PlayOneShot(Lloyd_Slice_1, 1f);
                break;

            case 2:
                GetComponent<AudioSource>().PlayOneShot(Lloyd_SonicThrust, 1f);
                break;

            case 3:
                GetComponent<AudioSource>().PlayOneShot(Lloyd_SwordRain, 1f);
                break;

            case 4:
                GetComponent<AudioSource>().PlayOneShot(Lloyd_DemonFang, 1f);
                break;

            case 5:
                GetComponent<AudioSource>().PlayOneShot(Lloyd_FierceDemonFang, 1f);
                break;
        }
    }

    // Fonction permettant de faire jouer le son de saut de Lloyd
    void SonSaut()
    {
        // Jouer le son de saut de Lloyd
        GetComponent<AudioSource>().PlayOneShot(Lloyd_Tempest, 1f);
    }
}