using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaClausHeadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Rompible" && collision.gameObject.layer == 6)
        {
            //Debug.Log("Destruir caja");
            Destroy(collision.gameObject);
        }


    }
}
