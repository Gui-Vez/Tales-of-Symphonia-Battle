using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ControlerKratos : MonoBehaviour
{
    /* ********************** */
    /* Création des variables */
    /* ********************** */


    // Vitesse horizontale
    float vitesseX;
    // Vitesse verticale 
    float vitesseY;
    // Vitesse horizontale maximale
    float vitesseXMax;
    // Vitesse de saut
    float vitesseYMax;


    // Direction gauche
    int gauche;
    // Direction droite
    int droite;
    // Direction par défaut
    int direction;


    // Effet de la potion rouge
    int effetPotionRouge;
    // Effet de la potion bleue
    Color effetPotionBleue;
    // Effet de la potion verte
    float effetPotionVerte;
    // Effet de la potion jaune
    float effetPotionJaune;


    // Temps nécessaire avant la prochaine attaque
    float attaqueDelai;
    // Temps écoulé depuis la dernière attaque
    float attaqueTemps;


    // Valeur de dégât causé par une attaque
    public static int degatAttaque;
    // Valeur du compteur de la vie
    public static int pointDeVie;
    // Index de l'attaque de Lloyd
    public static int attaqueIndex;


    // Variable détectant si Kratos est en train de marcher
    public static bool marche;
    // Variable détectant si Kratos est en train de courir
    public static bool cours;
    // Variable détectant si Kratos est en train de sauter
    public static bool saute;
    // Variable détectant si Kratos est en train de se défendre
    public static bool defend;
    // Variable détectant si Kratos est au sol ou non
    public static bool estAuSol;
    // Variable détectant si Kratos est invincible ou non
    public static bool estInvincible;
    // Variable détectant si Kratos est touché ou non
    public static bool estTouche;
    // Variable détectant si Kratos est en train d'attaquer
    public static bool attaque;
    // Variable détectant si Kratos peut attaquer ou non
    public static bool peutAttaquer;


    // Variable qui détermine si Kratos a gagné
    public static bool gagne;
    // Variable qui détermine si Kratos a perdu
    public static bool perdu;


    // Texte affiché dans le HUD
    public Text texteHP;
    // Layer permettant de déterminer ce qu'est le sol
    public LayerMask layerSol;
    // L'objet de Lloyd
    public GameObject persoLloyd;
    // L'objet de Kratos
    public GameObject persoKratos;



    /* *************************************************************** */
    /* La liste des commandes qui débutent à la première image (Frame) */
    /* *************************************************************** */
    void Start()
    {
        // Appliquer la vitesse horizontale maximale à 3
        vitesseXMax = 3;
        // Appliquer la vitesse verticale maximale à 12
        vitesseYMax = 12;

        // Valeur de la direction gauche
        gauche = 1;
        // Valeur de la direction droite
        droite = -1;
        // Valeur de la direction par défaut
        direction = -1;
        // Valeur de l'attaque par défaut
        attaqueIndex = 1;
        // Valeur du délai d'attaque par défaut
        attaqueDelai = 2f;
        // Valeur du temps écoulé depuis l'attaque
        attaqueTemps = 0f;

        // Donner 100 HP à Kratos
        pointDeVie = 100;

        // La potion rouge donne 20 HP
        effetPotionRouge = 20;
        // La potion bleue rend le sprite de Kratos bleu   [#9BA8EC]
        effetPotionBleue = new Color(155f, 168f, 236f);
        // La potion verte permet d'augmenter la vitesse de course de Kratos
        effetPotionVerte = 1f;
        // La potion jaune permet d'augmenter la hauteur du saut de Kratos
        effetPotionJaune = 1f;
    }



    /* *************************************************************** */
    /* Le Update permet d'activer les commandes à chaque image (Frame) */
    /* *************************************************************** */
    void Update()
    {
        // Si la partie n'est pas finie,
        if (!FinPartie.partieFinie)
        {
            /************* GESTION DES TOUCHES DE CLAVIER *************/


            // Si le clavier détecte que l'on appuie sur "gauche",
            if (Input.GetKey("left"))
            {
                // Rendre la valeur "marche" à vrai
                marche = true;

                // Modifier l'orientation du joueur vers la gauche
                direction = gauche;

                // Orienter le sprite de Kratos vers la gauche
                transform.localScale = new Vector3(direction, 1, 1);

                // Appliquer la valeur de déplacement X par rapport à la vitesse maximale négative
                vitesseX = (-vitesseXMax * effetPotionVerte);
            }

            // Sinon si le clavier détecte que l'on appuie sur "droite",
            else if (Input.GetKey("right"))
            {
                // Rendre la valeur "marche" à vrai
                marche = true;

                // Modifier l'orientation du joueur vers la droite
                direction = droite;

                // Orienter le sprite de Kratos vers la droite
                transform.localScale = new Vector3(direction, 1, 1);

                // Appliquer la valeur de déplacement X par rapport à la vitesse maximale positive
                vitesseX = (vitesseXMax * effetPotionVerte);
            }

            // Sinon,
            else
            {
                // Rendre la valeur "marche" à faux
                marche = false;

                // Maintenir la vélocité de la vitesse horizontale
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
            }

            // Si le clavier détecte que l'on appuie sur "haut" ET qu'il est au sol,
            if (Input.GetKeyDown("up") && estAuSol)
            {
                // Rendre la valeur "saute" à true
                saute = true;

                // Appliquer la valeur de déplacement Y par rapport à la vitesse de saut
                vitesseY = (vitesseYMax * effetPotionJaune);
            }

            // Sinon,
            else
            {
                // Rendre la valeur "saute" à faux
                saute = false;

                // Maintenir la vélocité de la vitesse verticale
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }


            // Si le clavier détecte que l'on appuie sur "bas",
            // MAIS PAS "haut", "gauche" ou "droite" ET qu'il est au sol,
            if (Input.GetKey("down") &&
               (!Input.GetKey("up") && !Input.GetKey("left") && !Input.GetKey("right")) &&
                estAuSol)
            {
                // Rendre la valeur "defend" à vrai
                defend = true;

                // Kratos ne peut plus toucher Lloyd
                ControlerLloyd.estTouche = false;

                // Réduire l'attaque de Lloyd
                ControlerLloyd.degatAttaque = UnityEngine.Random.Range(1, 3);
            }

            // Sinon,
            else
            {
                // Rendre la valeur "defend" à faux
                defend = false;

                // Retourner l'attaque de Lloyd par défaut
                ControlerLloyd.degatAttaque = UnityEngine.Random.Range(5, 11);
            }


            // Appliquer les vitesses de Kratos en X et Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
            

            /*************  GESTION DES COLLISIONS (SOL)  *************/


            // Si le pivot du personnage touche le sol,
            if (Physics2D.OverlapCircle(transform.position, 0.5f, layerSol) == true)
            {
                // Rendre la valeur "estAuSol" à vrai
                estAuSol = true;
            }

            // Sinon,
            else
            {
                // Rendre la valeur "estAuSol" à faux
                estAuSol = false;
            }


            /*****************  GESTION DES ATTAQUES  *****************/


            // Si la durée d'attente est supérieure au temps nécessaire,
            if (Time.time >= attaqueTemps)
            {
                // Permettre à Kratos d'attaquer
                peutAttaquer = true;
            }

            // Sinon,
            else
            {
                // Interdire Kratos d'attaquer
                peutAttaquer = false;
            }

            // Si l'on appuie sur le "shift" droite ET que Kratos peut attaquer ET que la partie n'est pas terminée,
            if (Input.GetKeyDown(KeyCode.RightShift) && peutAttaquer && !FinPartie.partieFinie)
            {
                // Rendre la valeur "attaque" à vrai
                attaque = true;

                // Réintialiser le temps écoulé depuis la dernière attaque
                attaqueTemps = Time.time + 1.5f / attaqueDelai;

                // Retenir l'index de l'attaque aléatoirement
                attaqueIndex = UnityEngine.Random.Range(1, 6);

                // Jouer l'animation d'attaque de celle-ci
                GetComponent<Animator>().SetBool("paraAttaque" + attaqueIndex, true);
            }

            // Sinon,
            else
            {
                // Rendre la valeur "attaque" à faux
                attaque = false;

                // Arrêter l'animation d'attaque en question
                GetComponent<Animator>().SetBool("paraAttaque" + attaqueIndex, false);
            }
        }



        /***************** GESTION DES ANIMATIONS *****************/


        // Si Kratos est en train de marcher,
        if (marche)
        {
            // Jouer l'animation de marche
            GetComponent<Animator>().SetBool("paraMarche", true);

            // Appeler la fonction permettant de faire sprinter Kratos après une seconde
            Invoke("ActiverCourse", 1f);
        }

        // Sinon,
        else
        {
            // Arrêter l'animation de marche
            GetComponent<Animator>().SetBool("paraMarche", false);

            // Arrêter l'animation de course
            GetComponent<Animator>().SetBool("paraCourse", false);

            // Arrêter immédiatement la fonction permettant de faire sprinter Kratos
            CancelInvoke("ActiverCourse");

            // Rétablir la vitesse horizontale maximale
            vitesseXMax = 3;
        }


        // Si Kratos est en train de sauter,
        if (saute)
        {
            // Jouer l'animation de saut
            GetComponent<Animator>().SetBool("paraSaut", true);
        }

        // Sinon,
        else
        {
            // Arrêter l'animation de saut
            GetComponent<Animator>().SetBool("paraSaut", false);
        }


        // Si Kratos se défend,
        if (defend)
        {
            // Jouer l'animation de défense
            GetComponent<Animator>().SetBool("paraDefense", true);
        }

        // Sinon,
        else
        {
            // Arrêter l'animation de défense
            GetComponent<Animator>().SetBool("paraDefense", false);
        }
        


        /*************  GESTION DES POINTS DE VIE  *************/


        // Ajouter la valeur du compteur au texte
        texteHP.text = pointDeVie.ToString() + " HP";

        // Si le compteur de point de vie de Kratos est supérieur ou égal à 100,
        if (pointDeVie >= 100)
        {
            // Retourner la valeur à 100
            pointDeVie = 100;
        }

        // Si La valeur du point de vie de Kratos est positif,
        if (pointDeVie > -1)
        {
            // Si Kratos attaque sans se défendre,
            if (attaque && !defend)
            {
                // Appeler la fonction permettant de donner des dégats à l'adversaire
                Invoke("donnerDegats", 0.25f);
            }
        }

        // Lorsque le compteur de Kratos devient négatif,
        if (pointDeVie <= 0)
        {
            // Kratos a perdu
            perdu = true;

            // La partie est terminée
            FinPartie.partieFinie = true;
        }
    }



    /* ********************* */
    /* Gestion des fonctions */
    /* ********************* */


    // Fonction permettant de détecter la collision entre objets
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignorer la collision avec le personnage Kratos
        Physics2D.IgnoreCollision(persoKratos.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Fonction permettant de détecter le chevauchement entre objets
    void OnTriggerEnter2D(Collider2D trigger)
    {
        // Si Kratos entre en contact avec la potion rouge,
        if (trigger.gameObject.name == "Rouge")
        {
            // Si Kratos a moins que 100 HP,
            if (pointDeVie < 100)
            {
                // Augmenter la jaune de vie de Kratos
                pointDeVie = pointDeVie + effetPotionRouge;
            }
        }

        // Si Kratos entre en contact avec la potion bleue,
        if (trigger.gameObject.name == "Bleue")
        {
            // Accorder un temps d'invincibilité à Kratos
            DonnerInvincibilite();
        }

        // Si Kratos entre en contact avec la potion verte,
        if (trigger.gameObject.name == "Verte")
        {
            // Augmenter la vitesse de course pour Kratos temporairement
            effetPotionVerte = 1.5f;
        }

        // Si Kratos entre en contact avec la potion jaune,
        if (trigger.gameObject.name == "Jaune")
        {
            // Augmenter la hauteur du saut pour Kratos temporairement
            effetPotionJaune = 1.25f;
        }

        // Appeler la fonction permettant d'arrêter de stopper les effets après 10 secondes
        Invoke("ArreterEffets", 10f);
    }

    // Fonction permettant à Kratos de sprinter
    void ActiverCourse()
    {
        // Rendre la valeur "cours" à vrai
        cours = true;

        // Jouer l'animation de course
        GetComponent<Animator>().SetBool("paraCourse", true);

        // Augmenter la vitesse horizontale maximale de Kratos
        vitesseXMax = 6;
    }

    // Fonction permettant de donner l'invincibilité
    void DonnerInvincibilite()
    {
        // Rendre Kratos invincible
        estInvincible = true;

        // Appliquer la couleur de la potion bleue au sprite de Kratos
        GetComponent<SpriteRenderer>().color = effetPotionBleue;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    // Fonction permettant d'arrêter les effets des potions
    void ArreterEffets()
    {
        // Rétablir la vulnérabilité de Kratos
        estInvincible = false;

        // Rendre le sprite de Kratos à sa couleur par défaut
        GetComponent<SpriteRenderer>().color = Color.white;

        // Rétablir la vitesse de Kratos
        effetPotionJaune = 1f;

        // Rétablir la vitesse de Kratos
        effetPotionVerte = 1f;
    }

    // Fonction permettant de donner des dégats
    void donnerDegats()
    {
        // Si Lloyd est touché ET qu'il n'est pas invincible ni mort,
        if (ControlerLloyd.estTouche && !ControlerLloyd.estInvincible && !ControlerLloyd.perdu)
        {
            // Baisser sa valeur de vie d'après le dégat de l'attaque
            ControlerLloyd.pointDeVie = ControlerLloyd.pointDeVie - degatAttaque;
        }
    }
}