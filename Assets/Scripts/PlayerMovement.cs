using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Je te promets que j'ai fait mes commentaires moi-m�me UwU */
public class PlayerMovement : MonoBehaviour
{
    //Init var li�es aux components
    Rigidbody2D rb;
    SpriteRenderer sr;

    //Init var li�es aux d�placements horizontales
    float horizontalValue;
    [SerializeField] float movementSpeed;

    //Init var li�ees au saut
    bool canJump;
    bool isJumping;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le component Rigidbody2D du Player
        sr = GetComponent<SpriteRenderer>(); // R�cup�re le component SpriteRenderer du Player
    }

    
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal"); // R�cup�re la valeur d'input horizontal
        if (horizontalValue > 0) sr.flipX = false; // Si la val horizontale est positif ( va � gauche ), le sprite est orient� vers la gauche.
        else if (horizontalValue < 0 ) sr.flipX = true; // Si la val horizontale est n�gatif ( va � droite ), le sprite est orient� vers la droite.
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isJumping)  // Appelle la fonction Jump si le joueur peut sauter, n'est pas d�j� en train de sauter et appuie sur la touche d'espace
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalValue * Time.deltaTime * movementSpeed, rb.velocity.y); // Modifie la velocit� du joueur en fonction de l'input horizontale et de la vitesse
    }

    private void OnTriggerEnter2D(Collider2D collision) // Si le joueur entre en collision avec une plateforme, il peut sauter et la gravit� est r�initialis�e
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

    void Jump()   // Ajoute une force de saut au joueur et active la coroutine pour g�rer le temps de chute
    {
        isJumping = true;
        Debug.Log("Jump");
        rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        StartCoroutine(FallTime());
    }

    IEnumerator FallTime() // Attend 0.6 secondes avant d'augmenter la gravit� du joueur pour le faire retomber plus vite
    {
        yield return new WaitForSeconds(0.6f);
        rb.gravityScale = 1.75f;
       
    }
}
