using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float speed = 3f;
    public Transform point;
    public float distance = 1f;
    public LayerMask layerMask;
    public bool facingleft = true;
    public bool inrange = false;
    public Transform player;
    public float  atrange = 4f;
    public float retriev = 2f;
    public float chasespeed = 5f;
    public Animator enan;
    public Transform attackpoint;

    public float atradius = 1f;
    public LayerMask Attack_layer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,player.position) <= atrange){
            inrange = true;

        }
        else{
            inrange = false;
        }
        if(inrange){
            if(player.position.x > transform.position.x && facingleft == true){
                transform.eulerAngles = new Vector3(0,-180,0);
                facingleft = false;
            }
            else if(player.position.x < transform.position.x && facingleft == false){
                transform.eulerAngles = new Vector3(0,0,0);
                facingleft = true;
            }
            
            if(Vector2.Distance(transform.position,player.position) > retriev){
                transform.position = Vector2.MoveTowards(transform.position,player.position,chasespeed * Time.deltaTime);

            }
            else{
                enan.SetBool("Attack 1",true);
            }
        }
        else{
            transform.Translate(Vector2.left*Time.deltaTime * speed);
            RaycastHit2D hit = Physics2D.Raycast(point.position,Vector2.down,distance,layerMask);

            if(hit == false && facingleft){
                transform.eulerAngles = new Vector3(0,-180,0);
                facingleft = false;
            }
            else if(hit == false && facingleft == false){
                transform.eulerAngles = new Vector3(0,0,0);
                facingleft = true;
            }
        }

    }
    public void Attack(){
        Collider2D collinfo = Physics2D.OverlapCircle(attackpoint.position,atradius,Attack_layer);
        if(collinfo){
            if(collinfo.gameObject.GetComponent<Player_movement>() != null){
                collinfo.gameObject.GetComponent<Player_movement>().TakeDamage(1);
            }
        }

    }
    
    private void OnDrawGizmosSelected()
    {
        if (point == null){
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(point.position,Vector2.down * distance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,atrange);
        if(attackpoint == null) return;
        Gizmos.DrawWireSphere(attackpoint.position,atradius);
    }
}
