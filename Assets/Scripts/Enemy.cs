using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float damage = 20;

    private GameObject preFab;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Dead", false);
        currentHealth = maxHealth;    
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Weapon>())
        {
            Debug.Log(currentHealth);
            TakeDamage();
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            // Animator의 Parameter Dead의 값을 true로 바꿔준다. 그런데 Die애니메이션이 반복해서 재생된다. 한 번만 재생되어야 하는데...
            // 반복을 해제하는 loop옵션이 있었던것 같은데 못찾겠다.
            animator.SetBool("Dead", true);
            
            // Get rid of Enemy Object. Bug appears. Enemy object disappears before the die animation is not finished.
            Destroy(gameObject);
        }
    }

    private void TakeDamage()
    {
        currentHealth -= damage;
    }
}
