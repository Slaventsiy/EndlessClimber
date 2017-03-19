using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

    private bool isShot = false;
    private float speed = 0.3f;
    public float rotationSpeed = 50;
    private int maxAngle = 60;
    private Quaternion rotation;
    public GameObject tip;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (isShot)
        {
            Move();

            if (Mathf.Abs(tip.transform.position.x) >= 7)
            {
                isShot = false;
            }
        }
        else
        {
            Rotate();
        }
    }

    public void Shoot (Quaternion rotation)
    {
        isShot = true;
        this.rotation = rotation;
    }

    private void Move()
    {
        Vector3 dir = Quaternion.AngleAxis(rotation.eulerAngles.z, Vector3.forward) * Vector3.right;
        transform.Translate(dir * speed);
    }

    private void Rotate()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.PingPong(Time.time * rotationSpeed, maxAngle)));
    }
}
