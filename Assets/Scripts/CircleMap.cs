using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMap : MonoBehaviour
{
    private static System.Random rnd = new System.Random(); 
 
    public int minNumSide = 10;
    public int mapSideLength = 100;
    public GameObject destroyedWall; 
    public GameObject notDestroyedWall;

    public List<List<CellType>> map2D;

    public enum CellType {
        Floor,
        DestroyedWall,
        NotDestroyedWall
    }
    // Start is called before the first frame update
    void Start()
    {
        BoxesTree boxesTree = GenerateBinaryBoxesTree(new Box(new Vector2(0, 0), mapSideLength, mapSideLength));
        List<Box> allBoxLeafs = boxesTree.GetAllBoxLeafs();
        foreach (Box box in allBoxLeafs) {
            if (IsBoxInInscribedCircle(box)) {
                box.DrawBoxFrame(destroyedWall, notDestroyedWall);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class BoxesTree{
        public Box currentBox;
        public List<BoxesTree> subBoxes;

        public BoxesTree(Box currentBox, List<BoxesTree> subBoxes) {
            this.currentBox = currentBox;
            this.subBoxes = subBoxes;
        }

        public List<Box> GetAllBoxLeafs(){
            if (subBoxes == null)
                return new List<Box>(){currentBox};
            List<Box> tempCont = new List<Box>();
            foreach (BoxesTree subBoxesTree in subBoxes) {
                tempCont.AddRange(subBoxesTree.GetAllBoxLeafs());
            }
            return tempCont;
        }
    }

    private class Box{
        public Vector2 leftTopPos, center;
        public int width, height;

        public Box(Vector2 leftTopPos, int width, int height){
            this.leftTopPos = leftTopPos;
            this.width = width;
            this.height = height;
            center = new Vector2(leftTopPos.x + width/2, leftTopPos.y + height/2);
        }
        public void DrawBoxFrame(GameObject destroyedWall, GameObject notDestroyedWall){
            for (int i = 0; i < width; i++){
                if (Mathf.Abs(center.x - (leftTopPos.x + i)) <= width/5)
                    Instantiate(destroyedWall, new Vector2(leftTopPos.x + i, leftTopPos.y), Quaternion.identity);
                else
                    Instantiate(notDestroyedWall, new Vector2(leftTopPos.x + i, leftTopPos.y), Quaternion.identity);
            }
            for (int i = 0; i < width; i++){
                if (Mathf.Abs(center.x - (leftTopPos.x + i)) <= width/5)
                    Instantiate(destroyedWall, new Vector2(leftTopPos.x + i, leftTopPos.y + height), Quaternion.identity);
                else
                    Instantiate(notDestroyedWall, new Vector2(leftTopPos.x + i, leftTopPos.y + height), Quaternion.identity);
            }
            for (int j = 1; j < height; j++){
                if (Mathf.Abs(center.y - (leftTopPos.y + j)) <= height/5)
                    Instantiate(destroyedWall, new Vector2(leftTopPos.x, leftTopPos.y + j), Quaternion.identity);
                else
                    Instantiate(notDestroyedWall, new Vector2(leftTopPos.x, leftTopPos.y + j), Quaternion.identity);
            }
            for (int j = 1; j < height; j++){
                if (Mathf.Abs(center.y - (leftTopPos.y + j)) <= height/5)
                    Instantiate(destroyedWall, new Vector2(leftTopPos.x + width, leftTopPos.y + j), Quaternion.identity);
                else
                    Instantiate(notDestroyedWall, new Vector2(leftTopPos.x + width, leftTopPos.y + j), Quaternion.identity);
            }
        }
    }

    private BoxesTree GenerateBinaryBoxesTree(Box box){
        if (2*minNumSide > box.height || 2*minNumSide > box.height)
            return new BoxesTree(box, null);
        Box newBox1, newBox2;
        if (box.height > box.width){
            int line = rnd.Next(box.height / 4, box.height *3 / 4);
            newBox1 = new Box(box.leftTopPos, box.width, line);
            newBox2 = new Box(new Vector2(box.leftTopPos.x, box.leftTopPos.y + line), box.width, box.height - line);
        } else {
            int line = rnd.Next(box.width / 4, box.width * 3 / 4);
            newBox1 = new Box(box.leftTopPos, line, box.height);
            newBox2 = new Box(new Vector2(box.leftTopPos.x + line, box.leftTopPos.y), box.width - line, box.height);
        }
        return new BoxesTree(box, new List<BoxesTree>(){
                GenerateBinaryBoxesTree(newBox1),
                GenerateBinaryBoxesTree(newBox2)
            });
    }

    private bool IsBoxInInscribedCircle(Box box){
        return Mathf.Pow((box.leftTopPos.x + box.width/2 - mapSideLength /2), 2) + Mathf.Pow((box.leftTopPos.y + box.height/2 - mapSideLength /2), 2) <= Mathf.Pow(mapSideLength/2, 2);
    }
}


