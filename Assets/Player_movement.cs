using UnityEngine;

public class Player_movement : MonoBehaviour
{
    private float movement;
    public float speed = 6f;
    private bool facingright = true;
    public Rigidbody2D rb;
    public float jumpheight = 15f;
    public bool isground = true;
    public Animator av;
    
    public int maxhealth = 5;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        av = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(maxhealth <= 0){
            die();
        }
        movement = Input.GetAxis("Horizontal"); //+1,0,-1 front or backward movement
        if (movement < 0f && facingright)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingright = false;

        }  //condition which is true will only cause the code inside the if statement to run
        else if (movement > 0f && facingright == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingright = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isground)
        {
            jump();
            isground = false;
            av.SetBool("Jump",true);
        }
        if (Mathf.Abs(movement) > .1f)
        {
            av.SetFloat("Run", 1f);
        }
        else if(movement < .1f)
        {
            av.SetFloat("Run",0f);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            av.SetTrigger("Attack");

        }



    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * speed;
    }

    void jump()
    {
        rb.AddForce(new Vector2(0f, jumpheight), ForceMode2D.Impulse);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform"){
            isground = true;
            av.SetBool("Jump",false);
        }
    }
    public void TakeDamage(int damage){
        if (maxhealth <= 0) {
            return;
        }
        maxhealth -= damage;

    }
    void die(){
        Debug.Log("Player has died");
    }
}
