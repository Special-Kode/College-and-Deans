using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [SerializeField] int velx;
    [SerializeField] int vely;
    [SerializeField] int limx;
    [SerializeField] int limy;

    // Start is called before the first frame update
    void Start()
    {
        velx = -2;
        vely = -2;
        limx = -12;
        limy = -6;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * vely * Time.deltaTime);
        transform.Translate(Vector2.right * velx * Time.deltaTime);

        if (this.transform.position.y < limy)
            this.transform.position = new Vector2(transform.position.x, -limy);

        if (this.transform.position.x < limx)
            this.transform.position = new Vector2(-limx, transform.position.y);
    }
}
