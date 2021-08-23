using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMap : MonoBehaviour
{
    private static System.Random rnd = new System.Random(); 
 
    public int minNumSide = 50;
    public int mapSideLength = 600;
    public GameObject roomType;

    // Start is called before the first frame update
    void Start()
    {
        BoxesTree boxesTree = GenerateBinaryBoxesTree(new Box(new Vector2(0, 0), mapSideLength, mapSideLength));
        List<Box> allBoxLeafs = boxesTree.GetAllBoxLeafs();
        foreach (Box box in allBoxLeafs) {
            Instantiate(roomType, new Vector2(box.leftTopPos.x + box.width/2, box.leftTopPos.y + box.height/2), Quaternion.identity);
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
        public Vector2 leftTopPos;
        public int width, height;

        public Box(Vector2 leftTopPos, int width, int height){
            this.leftTopPos = leftTopPos;
            this.width = width;
            this.height = height;
        }
    }

    private BoxesTree GenerateBinaryBoxesTree(Box box){
        if (2*minNumSide > box.height || 2*minNumSide > box.height)
            return new BoxesTree(box, null);
        Box newBox1, newBox2;
        if (box.height > box.width){
            int line = rnd.Next(box.height / 4, box.height *3 / 4);
            newBox1 = new Box(box.leftTopPos, box.width, line);
            newBox2 = new Box(new Vector2(box.leftTopPos.x, box.leftTopPos.y + line + 1), box.width, box.height - line);
        } else {
            int line = rnd.Next(box.width / 4, box.width * 3 / 4);
            newBox1 = new Box(box.leftTopPos, line, box.height);
            newBox2 = new Box(new Vector2(box.leftTopPos.x + line + 1, box.leftTopPos.y), box.width - line, box.height);
        }
        return new BoxesTree(box, new List<BoxesTree>(){
                GenerateBinaryBoxesTree(newBox1),
                GenerateBinaryBoxesTree(newBox2)
            });
    }
}


