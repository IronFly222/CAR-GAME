using UnityEngine;

public class WheelController : MonoBehaviour
{
    WheelCollider wheelCol;

    public float steerangle;
    public Transform meshWheel;
    public bool wheelTurn;

    void Start()
    {
        wheelCol = GetComponent<WheelCollider>();    
    }

    // Update is called once per frame
    void Update()
    {
        steerangle = wheelCol.steerAngle;

        if (steerangle != 0 && wheelTurn)
        {
            Quaternion eulerWheelRot = Quaternion.Euler(meshWheel.rotation.x, meshWheel.rotation.y + steerangle + 90, meshWheel.rotation.z);

            meshWheel.localRotation = eulerWheelRot;
        }

        if (wheelCol.rpm != 0)
        {
            meshWheel.Rotate(0, 0, wheelCol.rpm * Time.deltaTime);
        }
    }
}
