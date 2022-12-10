using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PlayerController verhindert, dass mehrere Player in einem Level existieren,
 * es gibt die Steuerung vor und animiert den Palyer
 */

public class PlayerController : MonoBehaviour
{
    public string startPoint;                               // gibt den Start-Punkt des Players an
    public float moveSpeed = 5f;                            // gibt die Bewegungsgeschwindigkeit des Players an
    public Transform movePoint;                             // beinhaltet den MovePoint, dem der Player folgt
    public LayerMask whatStopsMovement;                     // beinhaltet Layer-Maske, welche den Palayer am Bewegen hindert
    public Vector2 movement;                                // beinhaltet das Movement
    public Animator animator;                               // beinhaltet den Animator
    public int amuletBig = 20;                              // Lichtpunkte, die das große Amulett wiederherstellt
    public int amuletSmall = 10;                            // Lichtpunkte, die das kleine Amulett wiederherstellt 
    public Light playerLight;                               // beinhaltet die Lichtpunkte des Players
    public static PlayerController instance = null;         // damit andere Skripte auf den PlayerController zugreifen können
    public int keyCounter = 0;                              // Counter für die gesammelten Schlüssel

    private static bool playerExists;                       // gibt an, ob Player exestiert
    private static bool movePointExists;                    // gibt an, ob MovePoint exestiert
    private SFXManager sfxMan;                              // beinhaltet den SFXManager

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // entfernt den movePoint als Child des Players
    // sichert, dass nur ein Player im Level existiert
    // sichert, das movePoint nur einmal im Level existiert
    private void Start()
    {
        
        movePoint.parent = null;

        playerLight.range = PlayerHealthManager.instance.playerCurrentHealth;

        sfxMan = FindObjectOfType<SFXManager>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!movePointExists)
        {
            movePointExists = true;
            DontDestroyOnLoad(movePoint);
        }
        else
        {
            Destroy(movePoint);
        }
    }

    // implementiert die Steuerung an das Grid gebunden und in nur 4 Richtungen
    // diagonale Bewegungen werden als horizontale Bewegungen interpretiert
    // animiert den Player
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    sfxMan.playerWalk.Play();
                }
            } else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    sfxMan.playerWalk.Play();
                }
            }
        }

        if (keyCounter == 3)
        {
            Destroy(GameObject.FindGameObjectWithTag("Gate"));
            Destroy(GameObject.FindGameObjectWithTag("Boss"));
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    // OnTriggerEnter2D führt verschiedene Operationen aus
    // Operationen werden anhand des Tags des GameObjects bestimmt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "AmuletBig")
        {
            PlayerHealthManager.instance.playerCurrentHealth += amuletBig;
            collision.gameObject.SetActive(false);
            playerLight.range = PlayerHealthManager.instance.playerCurrentHealth;
            sfxMan.collect.Play();
        } 
        else if (collision.tag == "AmuletSmall")
        {
            PlayerHealthManager.instance.playerCurrentHealth += amuletSmall;
            collision.gameObject.SetActive(false);
            playerLight.range = PlayerHealthManager.instance.playerCurrentHealth;
            sfxMan.collect.Play();
        }
        else if(collision.tag == "Despawner")
        {
            collision.gameObject.SetActive(false);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            sfxMan.collect.Play();
        }
        else if (collision.tag == "Shield")
        {
            collision.gameObject.SetActive(false);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            PlayerHealthManager.instance.playerCurrentHealth += amuletBig;
            playerLight.range = PlayerHealthManager.instance.playerCurrentHealth;
            sfxMan.collect.Play();
        }
        else if (collision.tag == "Key")
        {
            collision.gameObject.SetActive(false);
            keyCounter++;
            sfxMan.collect.Play();
        }
    }

}
