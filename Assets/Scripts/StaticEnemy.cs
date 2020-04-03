using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditingProjectAI;
public class StaticEnemy : MonoBehaviour
{

    [SerializeField] SightSettings sightSetting;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject player;
    [SerializeField] float rotationAngleValue;
    [SerializeField] float rotationConstrain;
    private bool detected;

    // Update is called once per frame
    void Update()
    {
        if (!detected)
        {       
            Rotating(rotationAngleValue, rotationConstrain);
            if (IsTargetInRange())
            {
                Debug.Log("detected");
                detected = true;
            }
        }
        else
        {
            UIManager.Instance.ActivateGameOverPanel();
            Debug.Log("GameOver");
        }

    }


    void Rotating(float rotationAngle = 180, float rotationConstrain = 0)
    {
        if(rotationAngle != 0) { 
        float angle = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle) - (rotationAngle/2f); 
        
            if(angle < rotationConstrain)
            {
                angle *= -1;
                
            }
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    bool IsTargetInRange()
    {
        bool detected;
        float sightAngle;

        Vector3 start = transform.position + (transform.up / 2); //sight eyeheight
        Vector3 dir = (player.transform.position) - start;

      //  Debug.DrawRay(start, dir.normalized * sightSetting.SightRange, Color.red);

        sightAngle = Vector3.Angle(dir, transform.forward);
        detected = Physics.Raycast(start, dir, out RaycastHit hit, sightSetting.SightRange, sightSetting.SightLayers);

        if (detected && sightAngle < sightSetting.FieldOfView / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //void OnDrawGizmosSelected()
    //{
    //    Color c = Color.red;
    //    c.a = 0.05f;
    //    UnityEditor.Handles.color = c;
    //    UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, sightSetting.SightRange);

    //    Vector3 fovLine1 = Quaternion.AngleAxis(sightSetting.FieldOfView / 2, transform.up) * transform.forward * sightSetting.SightRange;
    //    Vector3 fovLine2 = Quaternion.AngleAxis(-sightSetting.FieldOfView / 2, transform.up) * transform.forward * sightSetting.SightRange;

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawRay(transform.position, fovLine1);
    //    Gizmos.DrawRay(transform.position, fovLine2);
    //}
}
