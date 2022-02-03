using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GestionSonsKratos : MonoBehaviour
{
    // Liste des sons de Kratos
    public AudioClip Kratos_Slice_1;
    public AudioClip Kratos_Slice_2;
    public AudioClip Kratos_Slice_3;
    public AudioClip Kratos_SonicThrust;
    public AudioClip Kratos_LightSpearCanon;
    public AudioClip Kratos_UpSlice;
    public AudioClip Kratos_FierceDemonFang;
    public AudioClip Kratos_LightSpear;


    // Variable permettant de faire jouer les sons ou non
    public static bool audioKratosActif;


    // Fonction à toutes les frames
    void Start()
    {
        // Activer la valeur audio
        audioKratosActif = true;

        // Créer une coroutine pour détecter le son des actions
        StartCoroutine(DetecterAction());
    }

    // Fonction permettant de détecter lorsque Kratos fait une action
    IEnumerator DetecterAction()
    {
        // Si la valeur est vraie,
        while (audioKratosActif)
        {
            // Faire attendre une fraction de seconde
            yield return new WaitForSeconds(0.001f);

            // Si Kratos attaque ET qu'il ne se défend pas,
            if (ControlerKratos.attaque && !ControlerKratos.defend)
            {
                // Appeler la fonction audio des attaques
                SonAttaque();

                // Faire attendre pendant un intervalle de temps
                yield return new WaitForSeconds(0.75f);

                // Mettre la valeur d'attaque à faux
                ControlerKratos.attaque = false;
            }

            // Si Kratos saute,
            if (ControlerKratos.saute)
            {
                // Appeler la fonction du saut
                SonSaut();

                // Faire attendre pendant un intervalle de temps
                yield return new WaitForSeconds(0.75f);

                // Mettre la valeur de saut à faux
                ControlerKratos.saute = false;
            }
        }
    }

    // Fonction permettant de faire jouer les sons d'attaque de Kratos
    void SonAttaque()
    {
        // Jouer un son dépendamment de l'index d'attaque de Kratos
        switch (ControlerKratos.attaqueIndex)
        {
            case 1:
                GetComponent<AudioSource>().PlayOneShot(Kratos_Slice_1, 1f);
                break;

            case 2:
                GetComponent<AudioSource>().PlayOneShot(Kratos_SonicThrust, 1f);
                break;

            case 3:
                GetComponent<AudioSource>().PlayOneShot(Kratos_LightSpearCanon, 1f);
                break;

            case 4:
                GetComponent<AudioSource>().PlayOneShot(Kratos_UpSlice, 1f);
                break;

            case 5:
                GetComponent<AudioSource>().PlayOneShot(Kratos_FierceDemonFang, 1f);
                break;
        }
    }

    // Fonction permettant de faire jouer le son de saut de Kratos
    void SonSaut()
    {
        // Jouer le son de saut de Kratos
        GetComponent<AudioSource>().PlayOneShot(Kratos_LightSpear, 1f);
    }
}