using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class BreadthFirstSearch 
{
    private static List<List<List<Vector2>>> graph;// = new List<List<List<Vector2>>>();


    public BreadthFirstSearch(CircleMap map) {
        if (graph == null)
            graph = CreateGraph(map);
    }
    Vector2 start,stop;
    public void OrderAWay(Vector2 start,Vector2 stop){
        this.start=start;
        this.stop=stop;
    // Create a thread and call a background method   
        Thread backgroundThread = new Thread(new ThreadStart(GetShortestWay));  
    // Start thread  
        backgroundThread.Start();  

    } 
    List<Vector2> readyWay = null;
    public List<Vector2> takeAWay(){
        if (readyWay == null)
            return null;
        List<Vector2> way = readyWay;
        readyWay=null;
        return way;
    }

    private void GetShortestWay() {
        Queue<Vector2> q = new Queue<Vector2>();
        q.Enqueue(start);
        List<List<bool>> used = Create2DList(graph.Count, graph[0].Count, false);
        used[(int)start.x][(int)start.y] = true;
        List<List<int>> d = Create2DList(graph.Count, graph[0].Count, 0);
        List<List<Vector2>> p = Create2DList(graph.Count, graph[0].Count, new Vector2(0, 0));
        p[(int)start.x][(int)start.y] = new Vector2(-1, -1);
        

        bool endFlag = false;
        while (q.Count != 0 && !endFlag) {
            Vector2 v = q.Dequeue();
            for (int i = 0; i < graph[(int)v.x][(int)v.y].Count; i++) {
                Vector2 to = graph[(int)v.x][(int)v.y][i];
                if (!used[(int)to.x][(int)to.y]) {
                    used[(int)to.x][(int)to.y] = true;
                    q.Enqueue(to);
                    d[(int)to.x][(int)to.y] = d[(int)v.x][(int)v.y] + 1;
                    p[(int)to.x][(int)to.y] = v;
                    if (to.x == stop.x && to.y == stop.y) {
                        endFlag = true;
                        break;
                    }   

                }
            }
        }
        Debug.Log(graph[(int)stop.x][(int)stop.y].Count);
        List<Vector2> path = new List<Vector2>();
        if (!used[(int)stop.x][(int)stop.y]){
          /*  int cnt = 0;
            foreach(List<List<Vector2>> lst in graph)
                foreach (List<Vector2> el in lst)
                    foreach (Vector2 to in el)
                        if (to == stop)
                            cnt++;
         throw new Exception("There are no any path form start ot stop " + start + " " + stop + "To array of start point: " + graph[(int)start.x][(int)stop.y].Count + "Count pathes to stop " + cnt);
        */
            readyWay=null;
            return;
        }
        for (Vector2 v = stop; v.x != -1 || v.y != -1; v = p[(int)v.x][(int)v.y]){
            path.Add(v);
        }
        path.Reverse();
        string str = "";
        foreach (Vector2 v in path)
            str = str + v;
        Debug.Log(str); 
        /*Debug.Log(path[0].x + " " + path[0].y + " " +  path[1].x + " " + path[1].y);
        */readyWay=path;

    }

    private List<List<List<Vector2>>> CreateGraph(CircleMap circleMap) {
        List<List<CircleMap.CellType>> map = circleMap.map2D;
        List<List<List<Vector2>>> graph =  Create2DList(map.Count, map[0].Count, new List<Vector2>());
        for (int i = 1; i < map.Count - 1; i++) {
            for (int j = 1; j < map[0].Count - 1; j++) {
                if (map[i][j] == CircleMap.CellType.NotDestroyedWall)
                    continue;
                for (int ii = i - 1; ii <= i + 1; ii++)
                    for(int jj = j - 1; jj <= j + 1; jj++) {
                        if ((map[ii][jj] == CircleMap.CellType.DestroyedWall || map[ii][jj] == CircleMap.CellType.Floor)&& !(ii == i && jj == j)  && (ii == i || jj == j))
                            graph[i][j].Add(new Vector2(ii, jj));
                    }
            }
        }
        return graph;
    } 

    private List<List<bool>> Create2DList(int width, int height, bool toFill){
        List<List<bool>> list = new List<List<bool>>();
        for (int i = 0; i < width; i++) {
            List<bool> temp = new List<bool>();
            for (int j = 0; j < height; j++) {
                temp.Add(toFill);
            }
            list.Add(temp);
        }
        return list;
    }

    private List<List<int>> Create2DList(int width, int height, int toFill){
        List<List<int>> list = new List<List<int>>();
        for (int i = 0; i < width; i++) {
            List<int> temp = new List<int>();
            for (int j = 0; j < height; j++) {
                temp.Add(toFill);
            }
            list.Add(temp);
        }
        return list;
    }

    private List<List<Vector2>> Create2DList(int width, int height, Vector2 toFill){
        List<List<Vector2>> list = new List<List<Vector2>>();
        for (int i = 0; i < width; i++) {
            List<Vector2> temp = new List<Vector2>();
            for (int j = 0; j < height; j++) {
                temp.Add(toFill);
            }
            list.Add(temp);
        }
        return list;
    }


    private List<List<List<Vector2>>> Create2DList(int width, int height, List<Vector2> toFill){
        List<List<List<Vector2>>> list = new List<List<List<Vector2>>>();
        for (int i = 0; i < width; i++) {
            List<List<Vector2>> temp = new List<List<Vector2>>();
            for (int j = 0; j < height; j++) {
                temp.Add(new List<Vector2>());
            }
            list.Add(temp);
        }
        return list;
    }
}
