using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinPartie : MonoBehaviour
{
    // Variable détectant si la partie est finie
    public static bool partieFinie = false;

    // Menu HUD du jeu
    public GameObject MenuJeu;
    public GameObject MenuFinPartie;
    public GameObject TexteLloyd;
    public GameObject TexteKratos;


    // Valeur des animations 'victoire'
    bool lloydVictoire;
    bool kratosVictoire;


    // Audio de la victoire de Lloyd
    public AudioClip sonVictoireLloyd;
    // Audio de la victoire de Kratos
    public AudioClip sonVictoireKratos;

    // Objet de Lloyd
    GameObject persoLloyd;
    // Objet de Kratos
    GameObject persoKratos;



    // Fonction à la première frame
    private void Start()
    {
        // Rechercher l'objet du personnage Lloyd
        persoLloyd = GameObject.Find("Lloyd_Vide/Lloyd");
        // Rechercher l'objet du personnage Kratos
        persoKratos = GameObject.Find("Kratos_Vide/Kratos");
    }


    // Fonction à toutes les frames
    void Update()
    {
        // Si la partie est finie,
        if (partieFinie)
        {
            // Annuler le dégat des attaques
            ControlerLloyd.degatAttaque = 0;
            ControlerKratos.degatAttaque = 0;

            // Arrêter l'animation de défense, s'il y a lieu
            persoLloyd.GetComponent<Animator>().SetBool("paraDefense", false);
            persoKratos.GetComponent<Animator>().SetBool("paraDefense", false);

            // Boucle de programmation des attaques
            for (int a = 1; a < 6; a++)
            {
                // Arrêter l'animation d'attaque, s'il y a lieu
                persoLloyd.GetComponent<Animator>().SetBool("paraAttaque" + a, false);
                persoKratos.GetComponent<Animator>().SetBool("paraAttaque" + a, false);
            }

            // Désactiver l'invulnérabilité, s'il y a lieu
            ControlerLloyd.estInvincible = false;
            ControlerKratos.estInvincible = false;

            // Désactiver le HUD du jeu
            MenuJeu.SetActive(false);

            // Activer le HUD de fin de partie
            MenuFinPartie.SetActive(true);



            // Si Kratos a perdu et que l'animation de la victoire de Lloyd n'est pas encore jouée,
            if (!ControlerLloyd.perdu && ControlerKratos.perdu && !lloydVictoire)
            {
                // Montrer le texte "Lloyd"
                TexteLloyd.SetActive(true);
                
                // Jouer l'animation de la victoire de Lloyd
                persoLloyd.GetComponent<Animator>().SetBool("paraVictoire", true);

                // Jouer l'animation de la défaite de Kratos
                persoKratos.GetComponent<Animator>().SetBool("paraDefaite", true);

                // Si le script audio de Lloyd est actif,
                if (GestionSonsLloyd.audioLloydActif)
                {
                    // Faire jouer le son que fait Lloyd lorsqu'il gagne
                    GetComponent<AudioSource>().PlayOneShot(sonVictoireLloyd, 1f);
                }

                // Activer la variable booléenne (Pour ne plus la déclencher)
                lloydVictoire = true;
            }

            // Sinon, si Lloyd a perdu et que l'animation de la victoire de Kratos n'est pas encore jouée,
            if (ControlerLloyd.perdu && !ControlerKratos.perdu && !kratosVictoire)
            {
                // Montrer le texte "Kratos"
                TexteKratos.SetActive(true);

                // Faire jouer le son que fait Kratos lorsqu'il gagne
                GetComponent<AudioSource>().PlayOneShot(sonVictoireKratos, 1f);

                // Jouer l'animation de la défaite de Lloyd
                persoLloyd.GetComponent<Animator>().SetBool("paraDefaite", true);

                // Jouer l'animation de la victoire de Kratos
                persoKratos.GetComponent<Animator>().SetBool("paraVictoire", true);

                // Si le script audio de Kratos est actif,
                if (GestionSonsKratos.audioKratosActif)
                {
                    // Faire jouer le son que fait Kratos lorsqu'il gagne
                    GetComponent<AudioSource>().PlayOneShot(sonVictoireKratos, 1f);
                }

                // Activer la variable booléenne (Pour ne plus la déclencher)
                kratosVictoire = true;
            }



            // Si l'on appuie sur la touche ENTER,
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Retourner la valeur de point de vie de Lloyd à 100
                ControlerLloyd.pointDeVie = 100;

                // Retourner la valeur de point de vie de Kratos à 100
                ControlerKratos.pointDeVie = 100;

                // Arrêter l'animation de la victoire de Lloyd
                persoLloyd.GetComponent<Animator>().SetBool("paraVictoire", false);

                // Arrêter l'animation de la défaite de Lloyd
                persoLloyd.GetComponent<Animator>().SetBool("paraDefaite", false);

                // Arrêter l'animation de la victoire de Kratos
                persoKratos.GetComponent<Animator>().SetBool("paraVictoire", false);

                // Arrêter l'animation de la défaite de Kratos
                persoKratos.GetComponent<Animator>().SetBool("paraDefaite", false);

                // Désactiver les variables booléennes (Pour ne plus les déclencher)
                lloydVictoire = false;
                kratosVictoire = false;

                // Appeler la fonction qui permet de relancer la partie
                RelancerJeu();
            }
        }

        // Sinon,
        else
        {
            // Activer le HUD principal
            MenuJeu.SetActive(true);

            // Enlever le texte de Lloyd
            TexteLloyd.SetActive(false);

            // Enlever le texte de Kratos
            TexteKratos.SetActive(false);
        }
    }

    // Fonction permettant de relancer le jeu
    void RelancerJeu()
    {
        // Rétablir la variable de défaite à faux
        ControlerLloyd.perdu = false;
        ControlerKratos.perdu = false;

        // Remettre la valeur de la fin de partie à faux
        FinPartie.partieFinie = false;

        // Relancer le jeu
        SceneManager.LoadScene(1);
    }
}