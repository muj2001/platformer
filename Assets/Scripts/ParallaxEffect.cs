using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{   
    public Camera cam;
    public Transform followTarget;

    // Starting position of the parallax game object
    Vector2 startingPosition;
    // Starting z position of the parallax game object
    float startingZ;    
    // Distance that the camera has moved from the starting posuition of the parallax game object
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    // The further the parallax object is from the target, the more it will move, so we need to invert the distance
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;  
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() 
    {
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
