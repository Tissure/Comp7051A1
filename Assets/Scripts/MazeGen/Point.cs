using System.Collections.Generic;
using UnityEngine;

namespace MazeAssignment
{
    public class Point 
    {       
        /*
         * Point Class:
         * Holds informaton pertaining to this Point
         * ===========================================================
         * Member Variables:
         * 
         * Represents the Cardinal Directions that this Point has direct connection with.
         * Integer Weights in each index for Prim's Algorithm.
         * NULL for no connection.
         * List<Integer> cardinalDirections [North(int), South(int), East(int), West(int)]
         * 
         * Boolean to determine if there is something at this Point.
         * boolean hasThing
         * ===========================================================
         * Helper Functions:
         * 
         * Setter Function for North
         * SetNorth()
         * 
         * Setter Function for South
         * SetSouth()
         * 
         * Setter Function for East
         * SetEast()
         * 
         * Setter function for West
         * SetWest()
         * 
         * ===========================================================
        */

        /*
         * List of Integer Weights in the Cardinal Directions
         */
        public List<int> cardinalDirection = new List<int>();

        // Boolean to know if this Point has something as this Point right now. Default to False
        public bool hasWall = false;

        // Vector3 for Position
        public Vector3 pos = Vector3.zero;

        // Testing for Serializing the Prefab
        public GameObject testPrefab;

        Vector2 mapPointer = new Vector2();
        Vector2 floorPointer = new Vector2();

        // Constructor
        public Point(float x, float y, float z)
        {
            // Set position of Point
            pos.x = x; 
            pos.y = y;   
            pos.z = z;

            // Nothing at this Point yet.
            hasWall = false;

            initCardinalDirection();

            // Set Cardinal Direction weights
            setNorth(Random.Range(1, 100));
            setSouth(Random.Range(1, 100));
            setEast(Random.Range(1, 100));
            setWest(Random.Range(1, 100));
        }

        public void initCardinalDirection()
        {
            for (int i = 0; i < 4; i++)
            {
                cardinalDirection.Add(-1);
            }
        }

        public void setCardinalWeight(string direction, int weight)
        {
            switch (direction)
            {
                case "North":
                    setNorth(weight);
                    break;
                case "South":
                    setSouth(weight);
                    break;
                case "East":
                    setEast(weight);
                    break;
                case "West":
                    setWest(weight);
                    break;
            }
        }


        public void setCardinalWeight(int direction, int weight)
        {
            switch (direction)
            {
                case 0:
                    setNorth(weight);
                    break;
                case 1:
                    setSouth(weight);
                    break;
                case 2:
                    setEast(weight);
                    break;
                case 3:
                    setWest(weight);
                    break;
            }
        }

        // Generic Setter Functions
        public void setNorth(int input)
        {
            cardinalDirection[0] = input;
        }

        public void setSouth(int input)
        {
            cardinalDirection[1] = input;
        }

        public void setEast(int input)
        {
            cardinalDirection[2] = input;
        }

        public void setWest(int input)
        {
            cardinalDirection[3] = input;
        }

        public void initMapPointer(int x, int z)
        {
            mapPointer.x = (x);
            mapPointer.y = (z);
        }

        public int getMapPointerX()
        {
            return (int)mapPointer.x;
        }

        public int getMapPointerZ()
        {
            return (int)mapPointer.y;
        }

        public void setMapPointerX(int x)
        {
            mapPointer.x = x;
        }

        public void setMapPointerZ(int z)
        {
            mapPointer.y = z;
        }


        public void initFloorPointer(int x, int z)
        {
            floorPointer.x = (x);
            floorPointer.y = (z);
        }

        public int getFloorPointerX()
        {
            return (int)floorPointer.x;
        }

        public int getFloorPointerZ()
        {
            return (int)floorPointer.y;
        }

        public void setFloorPointerX(int x)
        {
            floorPointer.x = x;
        }

        public void setFloorPointerZ(int z)
        {
            floorPointer.y = z;
        }
    }
}
