using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace MazeAssignment
{
    public class MazeGenerator : MonoBehaviour
    {

        [SerializeField]
        public int length;
        public int scale = 1;

        [SerializeField]
        GameObject testPrefab;

        [SerializeField]
        GameObject StartTrigger;

        [SerializeField]
        GameObject EndTrigger;

        [SerializeField]
        GameObject FloorPrefab;
        [SerializeField]
        GameObject AIPrefab;

        // Serialized Two Dimensional Map all the Points
        List<List<Point>> map = new List<List<Point>>();

        // Serialized Two Dimensional Map of the Floors
        List<List<Point>> floor = new List<List<Point>>();

        enum CardinalDirection { North = 0, South = 1, East = 2, West = 3 };

        List<Point> MST = new List<Point>();

        int floorCount;

        // Generates Maze per MazeGameManager
        public void GenerateMaze()
        {
            generateMap();

            // Map is Successfully Serialized
            //changeColorTest();

            floorCount = allocateFloors();
            changeColorFloorTest();

            // Test Reset Edge weights
            resetWeightsDirectedAtEdge();

            // Initial Test
            PrimAlgo(floor[0][0]);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            //debugFloor();
        }

        void generateMap()
        {
            //int temp = 0; 
            //UnityEngine.Debug.Log("Generating Map.");
            for (int x = 0; x < (2 * length) + 1; x++)
            {
                List<Point> tempList = new List<Point>();

                for (int z = 0; z < (2 * length) + 1; z++)
                {
                    //temp++;
                    // Instantiate a Two-Dimensional Map of the Prefab.
                    //GameObject thing = Instantiate(testPrefab, new Vector3(this.transform.position.x + x, this.transform.position.y, this.transform.position.z + z), Quaternion.identity);
                    //tempList.Add(thing);

                    // "Instatiate" Point Class
                    Point point = new Point(this.transform.position.x + x * scale, this.transform.position.y, this.transform.position.z + z * scale);

                    // Associate a prefab to the Point class, by Instantiating it at the current Position of this Point, modified by the map index.
                    point.testPrefab = Instantiate(testPrefab, new Vector3(point.pos.x, point.pos.y, point.pos.z), Quaternion.identity);
                    point.testPrefab.transform.localScale = new Vector3(scale, scale, scale);

                    // Add to tempList
                    tempList.Add(point);
                }

                // Every Column List, Add to Map
                map.Add(tempList);
            }
            //Debug.Log("GenerateMap.Count: " + temp)
        }

        // Testing Function
        void changeColorTest()
        {
            for (int x = 0; x < (2 * length) + 1; x++)
            {
                for (int z = 0; z < (2 * length) + 1; z++)
                {
                    map[x][z].testPrefab.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

                    // This check only gathers the points that I want. (Tested)
                    if (x % 2 != 0 && z % 2 != 0)
                    {
                        map[x][z].testPrefab.GetComponent<Renderer>().material.color = Color.black;
                    }
                }
            }
        }

        // Testing Function.. Works
        void changeColorFloorTest()
        {
            foreach (List<Point> a in floor)
            {
                foreach (Point b in a)
                {
                    b.testPrefab.GetComponent<Renderer>().material.color = Color.black;
                }
            }

            //for (int x = 0; x < floor.Count; x++)
            //{
            //    Debug.Log("Sizeof List in List: " + x + " = " + floor[x].Count);
            //}

            //floor[0][0].testPrefab.GetComponent<Renderer>().material.color = Color.blue;
            //floor[length-1][length-1].testPrefab.GetComponent<Renderer>().material.color = Color.red;
        }

        // Testing Function - detect all the Points that're "Floors"
        public int allocateFloors()
        {
            int count = 0;
            for (int x = 0; x < (2 * length) + 1; x++)
            {
                List<Point> tempList = new List<Point>();

                for (int z = 0; z < (2 * length) + 1; z++)
                {
                    if (x % 2 != 0 && z % 2 != 0)
                    {
                        map[x][z].initMapPointer(x, z);
                        tempList.Add((Point)map[x][z]);
                        count++;
                    }
                }

                if (tempList.Count > 0)
                {
                    floor.Add(tempList);
                    for (int i = 0; i < floor.Count; i++)
                    {
                        for (int j = 0; j < floor[i].Count; j++)
                        {
                            //Debug.Log("X: " + x + " Z: " + i);
                            floor[i][j].initFloorPointer(i, j);
                        }
                    }
                }

            }
            //UnityEngine.Debug.Log("Number of Floors:" + count);
            return count;
        }

        /* Prim Algo
         * Setup: Initialize a MST List<Point>.
         * 1. Get Point.MapPointer coordinates
         * 2. Find the smallest VALID weight.
         *      - Check if weight is greater than 0
         * 3. Destroy the intermediary Wall between the two points
         *      - Add THAT point to MST.
         * 4. Add Adjacent Point (in the direction of the Cardinal Direction) to MST
        */
        public void PrimAlgo(Point inputPoint)
        {
            //foreach (List<Point> a in floor)
            //{
            //    foreach (Point b in a)
            //    {
            //        Destroy(getIntermediatePoint(b, getLowestWeightDirection(b)).testPrefab);
            //    }
            //}

            //MST.Add(inputPoint);

            //string direction = getLowestWeightDirection(inputPoint);
            //Destroy(getIntermediatePoint(inputPoint, direction).testPrefab);

            //Point a = getAdjacentPoint(inputPoint, direction);

            //// Sometimes it'll infinitely loop between two points
            //while (MST.Contains(a))
            //{
            //    a = getAdjacentPoint(inputPoint, direction);
            //}

            //if (map[map.Count - 1][map[0].Count - 1] != a)
            //{
            //    PrimAlgo(a);
            //}

            /*
             * 1. Arbitrarily Start at any Point (I will choose map[0][0]).
             * 2. While map[length][length] is not in the MST...
             * 3. Check the Smallest Weight among the Points in the MST.
             *      3.1 If the smallest weight leads to an AdjacentPoint that is in MST, set the weight to 999.
             *          - Loop until I no longer have a smallest weight direction that directs inwards.
             *      3.2 If NOW the smallest weight direction leads to an AdjacentPoint is NOT in MST
             *          - Add to MST.
             *          - Repeat Step 3
             */

            // Arbitrarily start at a Point
            MST.Add(inputPoint);

            // Loop until Exit Point is in MST
            while (!MST.Contains(floor[length - 1][length - 1]) || (MST.Count < floorCount))
            //for (int x = 0; x < 5; x++)
            {
                // Check and fix all smallest weight directions that lead inwards.
                foreach (Point point in MST)
                {
                    // Acquire each cardinal direction to check
                    List<string> adjacentDirection = getAllAdjacentPoints(point);
                    //foreach(string a in adjacentDirection)
                    //{
                    //    Debug.Log(a);
                    //}

                    foreach (string a in adjacentDirection)
                    {

                        // Check all cardinal directions of each Point if AdjacentPoint are in MST
                        //if (MST.Contains(getAdjacentPoint(point, i)) && point.cardinalDirection[i] >= 1)
                        //{
                        //    point.setCardinalWeight(i, 420);
                        //}

                        // Check if AdjacentPoint is in MST.
                        if (MST.Contains(getAdjacentPoint(point, a)))
                        {
                            // This current smallest weight direction is point inwards towards the MST. Set the weight to 0.
                            point.setCardinalWeight(a, 999);
                        }
                    }
                }

                // At this point, there should now be no edges that point inwards. Now find the current smallest weight in MST (which points outwards)
                Point temp = MST[0];
                int lowest = 420;
                string tempdirection = "";
                foreach (Point point in MST)
                {
                    for (int i = 0; i < point.cardinalDirection.Count; i++)
                    {
                        //Debug.Log(i);
                        // Check every direction of every point for smaller weight
                        if (point.cardinalDirection[i] < lowest && point.cardinalDirection[i] >= 1)
                        {
                            //Debug.Log("lowest: " + lowest + " > " + "point.cardinalDirection[" + i + "]: " + point.cardinalDirection[i]);
                            // Smaller weight detected, record this Point and its direction.
                            temp = point;
                            lowest = point.cardinalDirection[i];
                            tempdirection = getLowestWeightDirection(point);
                        }
                    }
                }


                // At this point I should have my next smallest weight that is NOT pointing inwards. Add it to MST. Set the internal direction to 999.
                MST.Add(getAdjacentPoint(temp, tempdirection));
                Destroy(getIntermediatePoint(temp, tempdirection).testPrefab);

                // Repeat While loop until Exit Point is in MST..
            }

            // Debugging..
            //for(int i = 0; i<MST.Count; i++)
            //{
            //    for (int x = 0; x < MST[i].cardinalDirection.Count; x++) 
            //    {
            //        Debug.Log(i+" " + MST[i].cardinalDirection[x]);
            //    }
            //}

            destroyFloors();
            buildNavMesh();

            //floor[0][0].testPrefab = Instantiate(StartTrigger, floor[0][0].pos, Quaternion.identity);
            //PlayerRef.transform.pos = floor[0][0].pos;

            floor[length - 1][length - 1].testPrefab = Instantiate(EndTrigger, floor[length - 1][length - 1].pos + Vector3.up, Quaternion.identity);

            MazeGameManager.Instance.SetFloor(floor);

        }

        /*Helper Functions for access the Points in the Cardinal Directions
         * 1. Get Point.MapPointer coordinates
         * 2. Return Intermediary Point
        */
        public Point getIntermediatePoint(Point inputPoint, string Direction)
        {
            switch (Direction)
            {
                case "North":
                    return map[inputPoint.getMapPointerX()][inputPoint.getMapPointerZ() + 1];
                case "South":
                    return map[inputPoint.getMapPointerX()][inputPoint.getMapPointerZ() - 1];
                case "East":
                    return map[inputPoint.getMapPointerX() + 1][inputPoint.getMapPointerZ()];
                case "West":
                    return map[inputPoint.getMapPointerX() - 1][inputPoint.getMapPointerZ()];
            }

            // Something went wrong
            return null;
        }

        /*Helper Functions for access the Points in the Cardinal Directions
         * 1. Get Point.MapPointer coordinates
         * 2. Return Adjacent Point
        */
        public Point getAdjacentPoint(Point inputPoint, string Direction)
        {
            switch (Direction)
            {
                case "North":
                    return floor[inputPoint.getFloorPointerX()][inputPoint.getFloorPointerZ() + 1];
                case "South":
                    return floor[inputPoint.getFloorPointerX()][inputPoint.getFloorPointerZ() - 1];
                case "East":
                    return floor[inputPoint.getFloorPointerX() + 1][inputPoint.getFloorPointerZ()];
                case "West":
                    return floor[inputPoint.getFloorPointerX() - 1][inputPoint.getFloorPointerZ()];
            }

            // Something went wrong
            return null;
        }

        /*Helper Functions for access the Points in the Cardinal Directions
         * 1. Get Point.MapPointer coordinates
         * 2. Return Adjacent Point
        */
        public Point getAdjacentPoint(Point inputPoint, int Direction)
        {
            switch (Direction)
            {
                case 0:
                    return floor[inputPoint.getFloorPointerX()][inputPoint.getFloorPointerZ() + 1];
                case 1:
                    return floor[inputPoint.getFloorPointerX()][inputPoint.getFloorPointerZ() - 1];
                case 2:
                    return floor[inputPoint.getFloorPointerX() + 1][inputPoint.getFloorPointerZ()];
                case 3:
                    return floor[inputPoint.getFloorPointerX() - 1][inputPoint.getFloorPointerZ()];
            }

            // Something went wrong
            return null;
        }

        public string getLowestWeightDirection(Point inputPoint)
        {
            int lowest = 999;
            int indexLowest = 0;
            for (int x = 0; x < inputPoint.cardinalDirection.Count; x++)
            {
                if (inputPoint.cardinalDirection[x] < lowest && inputPoint.cardinalDirection[x] >= 1)
                {
                    indexLowest = x;
                    lowest = inputPoint.cardinalDirection[x];
                }
            }
            //UnityEngine.Debug.Log("LOWEST: " + lowest);
            switch (indexLowest)
            {
                case 0:
                    return "North";
                case 1:
                    return "South";
                case 2:
                    return "East";
                case 3:
                    return "West";
            }

            // Something went Wrong
            return null;
        }

        // Helper Function - reset all weights directed towards the edges
        public void resetWeightsDirectedAtEdge()
        {
            for (int x = 0; x < floor.Count; x++)
            {
                for (int z = 0; z < floor[x].Count; z++)
                {
                    if (x == 0)
                    {
                        floor[x][z].setWest(-1);
                    }
                    if (z == 0)
                    {
                        floor[x][z].setSouth(-1);
                    }
                    if (x == floor[x].Count - 1)
                    {
                        floor[x][z].setEast(-1);
                    }
                    if (z == floor[z].Count - 1)
                    {
                        //floor[x][z].testPrefab.GetComponent<Renderer>().material.color = Color.yellow;
                        floor[x][z].setNorth(-1);
                    }
                }
            }
        }

        public void debugFloor()
        {
            foreach (List<Point> a in floor)
            {
                foreach (Point b in a)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(b.pos, 1);
                }
            }
        }

        // Helper function - Get all adjacent Points
        public List<string> getAllAdjacentPoints(Point inputPoint)
        {
            List<string> result = new List<string>();

            //Debug.Log("X: " + inputPoint.getFloorPointerX());
            //Debug.Log("Z: " + inputPoint.getFloorPointerX());

            // Check North
            if (inputPoint.getFloorPointerZ() + 1 >= 0 && inputPoint.getFloorPointerZ() + 1 < length)
            {
                result.Add("North");
            }
            if (inputPoint.getFloorPointerZ() - 1 >= 0 && inputPoint.getFloorPointerZ() - 1 < length)
            {
                result.Add("South");
            }
            if (inputPoint.getFloorPointerX() + 1 >= 0 && inputPoint.getFloorPointerX() + 1 < length)
            {
                result.Add("East");
            }
            if (inputPoint.getFloorPointerX() - 1 >= 0 && inputPoint.getFloorPointerX() - 1 < length)
            {
                result.Add("West");
            }

            return result;
        }

        // Helper Function - Delete all Floors
        public void destroyFloors()
        {
            foreach (List<Point> list in floor)
            {
                foreach (Point point in list)
                {
                    Destroy(point.testPrefab);
                    point.testPrefab = Instantiate(FloorPrefab, point.pos, Quaternion.identity);
                    //Destroy(map[point.getMapPointerX()][point.getMapPointerZ()].testPrefab);
                }
            }
        }

        [SerializeField]
        private NavMeshSurface Surface;
        private NavMeshData NavMeshData;
        private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();
        private void buildNavMesh()
        {
            NavMeshData = new NavMeshData();
            NavMesh.AddNavMeshData(NavMeshData);
            Bounds navMeshBounds = new Bounds(floor[length / 2][length / 2].pos, length * scale * floor[0][0].testPrefab.GetComponent<Renderer>().bounds.size);
            List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

            List<NavMeshModifier> modifiers;
            if (Surface.collectObjects == CollectObjects.Children)
            {
                modifiers = new List<NavMeshModifier>(GetComponentsInChildren<NavMeshModifier>());
            }
            else
            {
                modifiers = NavMeshModifier.activeModifiers;
            }

            for (int i = 0; i < modifiers.Count; i++)
            {
                if (((Surface.layerMask & (1 << modifiers[i].gameObject.layer)) == 1)
                    && modifiers[i].AffectsAgentType(Surface.agentTypeID))
                {
                    markups.Add(new NavMeshBuildMarkup()
                    {
                        root = modifiers[i].transform,
                        overrideArea = modifiers[i].overrideArea,
                        area = modifiers[i].area,
                        ignoreFromBuild = modifiers[i].ignoreFromBuild
                    });
                }
            }

            if (Surface.collectObjects == CollectObjects.Children)
            {
                NavMeshBuilder.CollectSources(transform, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
            }
            else
            {
                NavMeshBuilder.CollectSources(navMeshBounds, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
            }

            Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);


            NavMeshBuilder.UpdateNavMeshData(NavMeshData, Surface.GetBuildSettings(), Sources, navMeshBounds);

        }

    }
}
