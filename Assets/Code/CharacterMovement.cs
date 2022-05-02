using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Swipe))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed = 15;
    public static bool IsDead { get; set; } = false;

    Swipe swipe;
    int laneIndex = 1; //0-left, 1-middle, 2-right
    float laneDistance = 3f;
    bool isSwipe = false;
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        swipe = GetComponent<Swipe>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator.SetBool("isHit", false);
        animator.SetBool("isDead", false);
        IsDead = false;
    }

    private void Update()
    {
        
        if (IsDead)
            return;

        if (swipe.SwipeRight)
        {
            if (laneIndex != 2)
            {
                isSwipe = true;
                StartCoroutine(Move("right"));                
            }

            laneIndex++;
            if (laneIndex == 3)
                laneIndex = 2;
        }
        if (swipe.SwipeLeft)
        {
            if (laneIndex != 0)
            {
                isSwipe = true;
                StartCoroutine(Move("left"));                
            }

            laneIndex--;
            if (laneIndex == -1)
                laneIndex = 0;
        }

        if (!isSwipe)
            StartCoroutine(MoveForward());

    }

    IEnumerator Move(string direction = "left")
    {
        float time = 0f;
        float duration = 0.3f;
        transform.position += Vector3.forward * speed * Time.deltaTime;
        Vector3 startPosition = transform.position;
        Vector3 endPosition;
        animator.SetBool("isRunning", false);

        if (direction == "left")
        {
            animator.SetBool("isLeft", true);
            endPosition = transform.position + Vector3.left * laneDistance;
        }
        else
        {
            animator.SetBool("isRight", true);
            endPosition = transform.position + Vector3.right * laneDistance;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            yield return null;
        }

        if (direction == "left")
            animator.SetBool("isLeft", false);
        else
            animator.SetBool("isRight", false);
        isSwipe = false;
    }

    IEnumerator MoveForward()
    {
        animator.SetBool("isRunning", true);
        transform.position += Vector3.forward * speed * Time.deltaTime;
        yield return null;
    }

    IEnumerator Dead()
    {
        
        audioSource.Play();
        animator.SetBool("isRunning", false);
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2.20f);
        GameController.IsGameOver = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            IsDead = true;
            StopAllCoroutines();
            StartCoroutine(Dead());                      
        }
    }
}
