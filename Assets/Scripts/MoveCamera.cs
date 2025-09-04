using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public Movement movement;
    public void StartCameraTransitionRight()
    {
        StartCoroutine(CameraTransitionRight());
    }
    public void StartCameraTransitionLeft()
    {
        StartCoroutine(CameraTransitionLeft());
    }
    public void StartCameraTransitionUp()
    {
        StartCoroutine(CameraTransitionUp());
    }
    public void StartCameraTransitionDown()
    {
        StartCoroutine(CameraTransitionDown());
    }
    

    IEnumerator CameraTransitionRight()
    {
        movement.canMove = false;
        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x+16, transform.position.y, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(5,0,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
        movement.canMove = true;

    }
    
    IEnumerator CameraTransitionLeft()
    {
        movement.canMove = false;

        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x-16, transform.position.y, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(-5,0,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
        movement.canMove = true;
    }
    
    IEnumerator CameraTransitionUp()
    {
        movement.canMove = false;
        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x, transform.position.y+11, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(0,5,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
        movement.canMove = true;
    }
    
    IEnumerator CameraTransitionDown()
    {
        movement.canMove = false;
        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x, transform.position.y-11, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(0,-5,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
        movement.canMove = true;
    }
}
