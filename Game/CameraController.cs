using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

	void Update () {
        //Runs if the player's position is different to the camera's position
		if ((player.transform.position != transform.position))
        {
            //This simply sets the camera's position to the player's position using a 3D Vector.
            //I could have simply set the camera's position to the player's position directly but it also set the Z axis to the same,
            //So the camera couldn't see the player. Alternatively I could have just used a 2D vector but I added the Z axis just to fix it in case it somehow gets set to something different
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
	}
}
