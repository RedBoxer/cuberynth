using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGen : MonoBehaviour
{
    const int labSize = 20;
    private LabyrinthTile[,] labyrinth = new LabyrinthTile[labSize,labSize];
    public LabyrinthTile tile;
    public GameObject player;
    public GameObject glass;
    public bool spawningPlane = false;

    public class Pair<T, U>
    {
        public bool Empty = true;
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
            this.Empty = false;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

    // Start is called before the first frame update
    void Start()
    {
        Transform currentPos = this.transform;

        for(int i = 0; i < labSize; i++)
        {
            for(int j = 0; j < labSize; j++)
            {
                labyrinth[i,j] = Instantiate(tile, currentPos.position, this.transform.rotation);
                currentPos.position += currentPos.forward * tile.GetComponent<Transform>().localScale.x;
                if (i == 0)
                {
                    labyrinth[i, j].ToPath();
                }
            }
            currentPos.position -= currentPos.forward * tile.GetComponent<Transform>().localScale.x * labSize;
            currentPos.position += currentPos.right * tile.GetComponent<Transform>().localScale.z; 
        }

        for (int i = 0; i < labSize; i++)
        {
            for (int j = 0; j < labSize; j++)
            {
                labyrinth[i, j].transform.parent = this.transform;
            }
        }
        //new Vector3(this.transform.position.x - 32 * tile.transform.localScale.x, this.transform.position.y + 32 * tile.transform.localScale.x, this.transform.position.z - tile.transform.localScale.x)
        GameObject cover = Instantiate(glass, this.transform.position , this.transform.rotation);
        cover.transform.position += cover.transform.up * tile.transform.localScale.x;
        cover.transform.position -= cover.transform.right * (labSize/2) * tile.transform.localScale.x;
        cover.transform.position += cover.transform.forward * (labSize/2) * tile.transform.localScale.x;
        cover.transform.localScale = new Vector3((labSize + 3) * tile.transform.localScale.x, tile.transform.localScale.x, (labSize + 3) * tile.transform.localScale.x);
        cover.transform.parent = this.transform;

        GeneratePath(new Pair<int, int>(1,1));
        if (spawningPlane)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {     
        player.transform.position = labyrinth[1, 1].transform.position + labyrinth[1, 1].transform.up * labyrinth[1, 1].transform.localScale.x;
    }
    void GeneratePath(Pair<int, int> start)
    {
        Stack< Pair<int, int>> wallStack = new Stack<Pair<int,int>>();
        List<Pair<int, int>> neighbours = new List<Pair<int, int>>();
        Pair<int, int> currentCell;
        labyrinth[start.First, start.Second].ToPath();
        wallStack.Push(start);
        while (wallStack.Count > 0)
        {
            currentCell = wallStack.Peek();
            wallStack.Pop();
            neighbours = GetNeighbours(currentCell);
            if(neighbours.Count != 0)
            {
                int pos = Random.Range(0, neighbours.Count);
                wallStack.Push(currentCell);
                currentCell = neighbours[pos];
                labyrinth[currentCell.First, currentCell.Second].ToPath();

                if(wallStack.Peek().First - currentCell.First == 1)
                {
                    wallStack.Push(new Pair<int, int>(currentCell.First - 1, currentCell.Second));
                    labyrinth[currentCell.First - 1, currentCell.Second].ToPath();
                }
                else if (wallStack.Peek().First - currentCell.First == -1)
                {
                    wallStack.Push(new Pair<int, int>(currentCell.First + 1, currentCell.Second));
                    labyrinth[currentCell.First + 1, currentCell.Second].ToPath();
                }

                if (wallStack.Peek().Second - currentCell.Second == 1)
                {
                    wallStack.Push(new Pair<int, int>(currentCell.First, currentCell.Second - 1));
                    labyrinth[currentCell.First, currentCell.Second - 1].ToPath();
                }
                else if (wallStack.Peek().Second - currentCell.Second == -1)
                {
                    wallStack.Push(new Pair<int, int>(currentCell.First, currentCell.Second + 1));
                    labyrinth[currentCell.First, currentCell.Second + 1].ToPath();
                }
            }
        }
    }

    List<Pair<int, int>> GetNeighbours(Pair<int, int> cell)
    {       
        List<Pair<int, int>> notpath = new List<Pair<int, int>>();

        if (cell.First - 2 >= 0)
        {
            if (labyrinth[cell.First - 2, cell.Second].isWall())
            {
                notpath.Add(new Pair<int, int>(cell.First - 1, cell.Second));
            }
        }

        if (cell.First + 2 < labSize)
        {
            if (labyrinth[cell.First + 2, cell.Second].isWall())
            {
                notpath.Add(new Pair<int, int>(cell.First + 1, cell.Second));
            }
        }

        if (cell.Second - 2 >= 0)
        {
            if (labyrinth[cell.First, cell.Second - 2].isWall())
            {
                notpath.Add(new Pair<int, int>(cell.First, cell.Second - 1));
            }
        }

        if (cell.Second + 2 < labSize)
        {
            if (labyrinth[cell.First, cell.Second + 2].isWall())
            {
                notpath.Add(new Pair<int, int>(cell.First, cell.Second + 1));
            }
        }

        return notpath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
