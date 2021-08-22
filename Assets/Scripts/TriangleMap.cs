using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMap : MonoBehaviour
{
    Vector2 topVertex = new Vector2();
    Vector2 leftBottomVertex = new Vector2();
    Vector2 rightBottomVertex = new Vector2();
    int minNumSide = 50;
    static System.Random rnd = new System.Random();  

    // Start is called before the first frame update
    void Start()
    {
        
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


