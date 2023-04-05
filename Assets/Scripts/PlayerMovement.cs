using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Je te promets que j'ai fait mes commentaires moi-même UwU */
public class PlayerMovement : MonoBehaviour
{
    //Init var liées aux components
    Rigidbody2D rb;
    SpriteRenderer sr;

    //Init var liées aux déplacements horizontales
    float horizontalValue;
    [SerializeField] float movementSpeed;

    //Init var liéees au saut
    bool canJump;
    bool isJumping;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le component Rigidbody2D du Player
        sr = GetComponent<SpriteRenderer>(); // Récupère le component SpriteRenderer du Player
    }

    
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal"); // Récupère la valeur d'input horizontal
        if (horizontalValue > 0) sr.flipX = false; // Si la val horizontale est positif ( va à gauche ), le sprite est orienté vers la gauche.
        else if (horizontalValue < 0 ) sr.flipX = true; // Si la val horizontale est négatif ( va à droite ), le sprite est orienté vers la droite.
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isJumping)  // Appelle la fonction Jump si le joueur peut sauter, n'est pas déjà en train de sauter et appuie sur la touche d'espace
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalValue * Time.deltaTime * movementSpeed, rb.velocity.y); // Modifie la velocité du joueur en fonction de l'input horizontale et de la vitesse
    }

    private void OnTriggerEnter2D(Collider2D collision) // Si le joueur entre en collision avec une plateforme, il peut sauter et la gravité est réinitialisée
    {
        if (collision.tag == "Plateform")
        {
            isJumping = false;
            canJump = true;
            rb.gravityScale = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  // Si le joueur quitte la plateforme et n'est pas en train de sauter, il ne peut plus sauter
    {
        if (!isJumping) canJump = false;
    }

    void Jump()   // Ajoute une force de saut au joueur et active la coroutine pour gérer le temps de chute
    {
        isJumping = true;
        Debug.Log("Jump");
        rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        StartCoroutine(FallTime());
    }

    IEnumerator FallTime() // Attend 0.6 secondes avant d'augmenter la gravité du joueur pour le faire retomber plus vite
    {
        yield return new WaitForSeconds(0.6f);
        rb.gravityScale = 1.75f;
       
    }
}
