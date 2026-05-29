using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarIA : MonoBehaviour
{
    public float wheelSpeed;
    public float wheelAddTorque;
    public float wheelBrake;
    public float wheelBrakeSpeed;
    public float wheelSteelAngle;
    public float wheelSteelSpeed;

    public Transform[] waypoints;
    public int actualWaypoint;

    public WheelCollider wheel1, wheel2, wheel3, wheel4;

    public float minWaypointVariation, maxWaypointVariation, waypointDistanceVariation;

    public Transform trajectoryPoint;
    public float directionToPoint;
    public float steelPrecision;

    public float wheel1S;
    public float wheel2S;
    public void NextPoint()
    {
        actualWaypoint++;

        if (actualWaypoint >= waypoints.Length)
            actualWaypoint = 0;

        waypointDistanceVariation = Random.Range(minWaypointVariation, maxWaypointVariation);
    }

    private void Update()
    {
        DirectionControl();
        Drive();

        wheel1S = wheel1.steerAngle;
        wheel2S = wheel2.steerAngle;

        Vector3 dirToTarget =
        (waypoints[actualWaypoint].position - transform.position).normalized;

        //Debug.Log(dirToTarget);
    }

    void DirectionControl()
    {
        directionToPoint = waypoints[actualWaypoint].InverseTransformPoint(trajectoryPoint.position).x;

        if (directionToPoint < steelPrecision && directionToPoint > -steelPrecision)
        {

        }
        else
        {
            wheel1.steerAngle += wheelSteelSpeed * -directionToPoint * Time.deltaTime;
            wheel1.steerAngle = Mathf.Clamp(wheel1.steerAngle, -wheelSteelAngle, wheelSteelAngle);

            wheel2.steerAngle += wheelSteelSpeed * -directionToPoint * Time.deltaTime;
            wheel2.steerAngle = Mathf.Clamp(wheel2.steerAngle, -wheelSteelAngle, wheelSteelAngle);
        }

    }

    void Drive()
    {
        if(Vector3.Distance(transform.position, waypoints[actualWaypoint].position) < waypointDistanceVariation)
        {
            NextPoint();
        }
        else
        {
            /*if (Vector3.Distance(transform.position, waypoints[actualWaypoint].position) < waypointDistanceVariation + 10)
            {
                wheel3.brakeTorque += wheelBrake * wheelBrakeSpeed * Time.deltaTime;
                wheel4.brakeTorque += wheelBrake * wheelBrakeSpeed * Time.deltaTime;
            }*/

            wheel3.motorTorque += wheelAddTorque * wheelSpeed * Time.deltaTime; 
            wheel4.motorTorque += wheelAddTorque * wheelSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, waypoints[actualWaypoint].position);

            if (distance < waypointDistanceVariation + 3)
            {
                wheel3.brakeTorque += wheelBrakeSpeed * Time.deltaTime;
                wheel3.brakeTorque = Mathf.Clamp(wheel3.brakeTorque, 0, wheelBrake);

                wheel4.brakeTorque += wheelBrakeSpeed * Time.deltaTime;
                wheel4.brakeTorque = Mathf.Clamp(wheel4.brakeTorque, 0, wheelBrake);
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        //(Gizmos.DrawCube(transform.position + new Vector3(0f, 0f, 10f) * transform.forward, new Vector3(0.5f, 0.5f, 0.5f));
    }
}