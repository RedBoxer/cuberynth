using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCube : MonoBehaviour
{
    const int labSize = 20;
    public List<LabyrinthGen> sides;
    public LabyrinthTile tile; 
    // Start is called before the first frame update
    void Start()
    {
        //top
        sides[0].transform.position = new Vector3(sides[0].transform.position.x, tile.transform.localScale.x * labSize, sides[0].transform.position.z + tile.transform.localScale.x);
        //bottom
        sides[1].transform.Rotate(Vector3.forward, 180);
        sides[1].transform.position = new Vector3(tile.transform.localScale.x * (labSize - 1), sides[1].transform.position.y - tile.transform.localScale.x, sides[1].transform.position.z + tile.transform.localScale.x);
        //right
        sides[2].transform.Rotate(Vector3.forward, 90);
        sides[2].transform.position = new Vector3(sides[2].transform.position.x - tile.transform.localScale.x, sides[2].transform.position.y, sides[2].transform.position.z + tile.transform.localScale.x);
        //left
        sides[3].transform.Rotate(Vector3.forward, 90);
        sides[3].transform.Rotate(Vector3.left, 180);
        sides[3].transform.position = new Vector3(tile.transform.localScale.x * labSize, sides[3].transform.position.y, tile.transform.localScale.x * labSize);
        //back
        sides[4].transform.Rotate(Vector3.right, 90);
        sides[4].transform.position = new Vector3(sides[4].transform.position.x, tile.transform.localScale.x * (labSize - 1), tile.transform.localScale.x * (labSize + 1));
        //front
        sides[5].transform.Rotate(Vector3.left, 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
