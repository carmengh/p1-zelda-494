using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public void StartCameraTransitionRight()
    {
        StartCoroutine(CameraTransitionRight());
    }
    public void StartCameraTransitionLeft()
    {
        StartCoroutine(CameraTransitionLeft());
    }

    IEnumerator CameraTransitionRight()
    {
        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x+16, transform.position.y, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(6,0,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
    }
    
    IEnumerator CameraTransitionLeft()
    {
        Vector3 initial_pos = transform.position;
        Vector3 target_pos = new Vector3(transform.position.x-16, transform.position.y, -10);
        
        Vector3 player_pos = player.position;
        Vector3 player_final = player_pos+new Vector3(-5,0,0);
        
        yield return StartCoroutine(CoroutineUtilities.MoveTwoObjectsOverTime(transform, 
            initial_pos, target_pos,player,player_pos, player_final,2.5f));
    }
}
