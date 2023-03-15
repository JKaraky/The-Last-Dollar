using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloating : MonoBehaviour
{
    public float bobHeight;
    public float bobSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        GetComponent<Rigidbody2D>().MovePosition(new Vector3(transform.position.x, newY, transform.position.z));

    }
}
