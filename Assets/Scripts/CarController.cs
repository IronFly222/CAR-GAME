using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody rigid;

    public WheelCollider wheel1, wheel2, wheel3, wheel4;

    public float drivespeed = 1500f;
    public float brakeForce = 4000f;
    public float steerspeed = 50f;

    public float noInputBrakeFroce = 2000f;

    float horizontalInput, verticalInput;

    float motor;

    private void Start()
    {
        // Hace el coche MUCHO más estable
        //rigid.centerOfMass = new Vector3(0, 0, 0);

        // Ajustes recomendados
        rigid.linearDamping = 0.15f;
        rigid.angularDamping = 0.5f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        motor = verticalInput;

        // ACELERAR
        if (motor > 0)
        {
            wheel3.motorTorque = motor * drivespeed;
            wheel4.motorTorque = motor * drivespeed;

            wheel1.motorTorque = motor * drivespeed;
            wheel2.motorTorque = motor * drivespeed;

            wheel1.brakeTorque = 0;
            wheel2.brakeTorque = 0;
            wheel3.brakeTorque = 0;
            wheel4.brakeTorque = 0;
        }

        // FRENAR
        if (motor < 0)
        {
            wheel1.motorTorque = 0;
            wheel2.motorTorque = 0;
            wheel3.motorTorque = 0;
            wheel4.motorTorque = 0;

            wheel1.brakeTorque = -motor * brakeForce;
            wheel2.brakeTorque = -motor * brakeForce;
            wheel3.brakeTorque = -motor * brakeForce;
            wheel4.brakeTorque = -motor * brakeForce;
        }

        // SIN INPUT
        if (motor == 0 && wheel1.motorTorque > 0)
        {
            wheel1.motorTorque -= Time.deltaTime * noInputBrakeFroce;
            wheel2.motorTorque -= Time.deltaTime * noInputBrakeFroce;
            wheel3.motorTorque -= Time.deltaTime * noInputBrakeFroce;
            wheel4.motorTorque -= Time.deltaTime * noInputBrakeFroce;
        }

        // GIRAR
        wheel1.steerAngle = steerspeed * horizontalInput;
        wheel2.steerAngle = steerspeed * horizontalInput;
    }

    private void OnDrawGizmos()
    {
        if (rigid == null) return;

        Gizmos.color = Color.red;

        // Convierte el centro de masa local a posición global
        Vector3 worldCenterOfMass = transform.TransformPoint(rigid.centerOfMass);

        Gizmos.DrawSphere(worldCenterOfMass, 0.2f);
    }
}