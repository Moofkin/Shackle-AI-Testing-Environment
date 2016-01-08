// Created by: Lauren Cunningham

// This file will be used to set up the undirected graph that will govern the monster's movement, specifically for the 
    // monster testing environment. Another file like this will be created for each of Shackle's levels.

using UnityEngine;
using System.Collections;

public class spt_createGraphForTestingEnvironment : MonoBehaviour {

    //There are 49 waypoints in the test level, arraged in a square grid formation.
        //Their indices are stored in a two dimensional array to mark links between them.
        //For example, the first waypoint (index 0) can be accessed by the second(1), eigth(7), and ninth(8) waypoints.
    private int[][] waypointGraph;

	// Use this for initialization
	void Start () {
        setWaypoints();
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Sets up the graph (two-dimensional array).
    void setWaypoints(){
        waypointGraph = new int[49][];

        waypointGraph[0] = new int[] { 1, 7, 8 };
        waypointGraph[1] = new int[] { 0, 2, 7, 8, 9 };
        waypointGraph[2] = new int[] { 1, 3, 8, 9, 10 };
        waypointGraph[3] = new int[] { 2, 4, 9, 10, 11 };
        waypointGraph[4] = new int[] { 3, 5, 10, 11, 12 };
        waypointGraph[5] = new int[] { 4, 6, 11, 12, 13 };
        waypointGraph[6] = new int[] { 5, 12, 13 };

        waypointGraph[7] = new int[] { 0, 1, 8, 14, 15 };
        waypointGraph[8] = new int[] { 0, 1, 2, 7, 9, 14, 15, 16 };
        waypointGraph[9] = new int[] { 1, 2, 3, 8, 10, 15, 16, 17 };
        waypointGraph[10] = new int[] { 2, 3, 4, 9, 11, 16, 17, 18 };
        waypointGraph[11] = new int[] { 3, 4, 5, 10, 12, 17, 18, 19 };
        waypointGraph[12] = new int[] { 4, 5, 6, 11, 13, 18, 19, 20 };
        waypointGraph[13] = new int[] { 5, 6, 12, 19, 20 };

        waypointGraph[14] = new int[] { 7, 8, 15, 21, 22 };
        waypointGraph[15] = new int[] { 7, 8, 9, 14, 16, 21, 22, 23 };
        waypointGraph[16] = new int[] { 8, 9, 10, 15, 17, 22, 23, 24 };
        waypointGraph[17] = new int[] { 9, 10, 11, 16, 18, 23, 24, 25 };
        waypointGraph[18] = new int[] { 10, 11, 12, 17, 19, 24, 25, 26 };
        waypointGraph[19] = new int[] { 11, 12, 13, 18, 20, 25, 26, 27 };
        waypointGraph[20] = new int[] { 12, 13, 19, 26, 27 };

        waypointGraph[21] = new int[] { 14, 15, 22, 28, 29 };
        waypointGraph[22] = new int[] { 14, 15, 16, 21, 23, 28, 29, 30 };
        waypointGraph[23] = new int[] { 15, 16, 17, 22, 24, 29, 30, 31 };
        waypointGraph[24] = new int[] { 16, 17, 18, 23, 25, 30, 31, 32 };
        waypointGraph[25] = new int[] { 17, 18, 19, 24, 26, 31, 32, 33 };
        waypointGraph[26] = new int[] { 18, 19, 20, 25, 27, 32, 33, 34 };
        waypointGraph[27] = new int[] { 19, 20, 26, 33, 34 };

        waypointGraph[28] = new int[] { 21, 22, 29, 35, 36 };
        waypointGraph[29] = new int[] { 21, 22, 23, 28, 30, 35, 36, 37 };
        waypointGraph[30] = new int[] { 22, 23, 24, 29, 31, 36, 37, 38 };
        waypointGraph[31] = new int[] { 23, 24, 25, 30, 32, 37, 38, 39 };
        waypointGraph[32] = new int[] { 24, 25, 26, 31, 33, 38, 39, 40 };
        waypointGraph[33] = new int[] { 25, 26, 27, 32, 34, 39, 40, 41 };
        waypointGraph[34] = new int[] { 26, 27, 33, 40, 41 };

        waypointGraph[35] = new int[] { 28, 29, 36, 42, 43 };
        waypointGraph[36] = new int[] { 28, 29, 30, 35, 37, 42, 43, 44 };
        waypointGraph[37] = new int[] { 29, 30, 31, 36, 38, 43, 44, 45 };
        waypointGraph[38] = new int[] { 30, 31, 32, 37, 39, 44, 45, 46 };
        waypointGraph[39] = new int[] { 31, 32, 33, 38, 40, 45, 46, 47 };
        waypointGraph[40] = new int[] { 32, 33, 34, 39, 41, 46, 47, 48 };
        waypointGraph[41] = new int[] { 33, 34, 40, 47, 48 };

        waypointGraph[42] = new int[] { 35, 36, 43 };
        waypointGraph[43] = new int[] { 35, 36, 37, 42, 44 };
        waypointGraph[44] = new int[] { 36, 37, 38, 43, 45 };
        waypointGraph[45] = new int[] { 37, 38, 39, 44, 46 };
        waypointGraph[46] = new int[] { 38, 39, 40, 45, 47 };
        waypointGraph[47] = new int[] { 39, 40, 41, 46, 48 };
        waypointGraph[48] = new int[] { 40, 41, 47 };
    }

    // Returns the graph so that it can be used by other scripts.
    public int[][] getWaypointGraph(){
        return waypointGraph;
    }
}
