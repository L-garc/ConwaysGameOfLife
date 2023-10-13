/*
    A C# Object Oriented Implementation of Conway's Game of Life

    A living cell with less than two neighbors, or more than 3, dies
    A dead cell with exactly three living neighbors comes to life
*/
using System;
class Life {
    //Field size is 10 by 10
    const int FIELDSIZE = 10;

    //Define a field (array) that is 10 by 10
    //This is the seed (initial state) of the game
    int[,] field = {
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,1,1,0,1,0,0,0,0},
        {0,0,0,1,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0}
    };

    //We use a second field to track what changes need to be made on a cell by cell basis
    //without prior modifactions affecting future modifications to the field
    int[,] tempField = new int[FIELDSIZE, FIELDSIZE];

    //Constructor, initialize things
    public Life(){
        //Set the entire field "tempField" to zeros
        zeroField(tempField);
    }

    //The program goes here, this is the entry point
    static void Main(string[] Args){
        //C# feels like a cross between C++ and Java, instantiate this class to avoid "static reference" problems
        Life life = new Life();

        //Print the seed of the field
        life.printField();

        for(int i = 0; i < 10; i++){
            //update the field
            life.nextGen();
            //print the updated field
            life.printField();
        }
    }

    private void zeroField(int[,] arr){
        //Set all cells in the field to zero
        for(int i = 0; i < arr.GetLength(0); i++){
            for(int j = 0; j < arr.GetLength(1); j++){
                arr[i, j] = 0;
            }
        }
    }

    public void printField(){
        //This method prints the current state of the field
        for(int i = 0; i < field.GetLength(0); i++){
            for(int j = 0; j < field.GetLength(1); j++){
                Console.Write( field[i,j].ToString() );
            }
            Console.WriteLine(""); //Go to a new line
        }
        Console.WriteLine(""); //Doublespace between prints of field
    }

    public void nextGen(){
        //This method goes thorugh each cell of field, and updates tempField
        for(int i = 0; i < field.GetLength(0); i++){
            for(int j = 0; j < field.GetLength(1); j++){
                tempField[i,j] = applyRules(i, j);
            }
        }

        //After the next generation is set in tempField, copy the tempField to the field
        copyTemp();
    }

    private int applyRules(int i, int j){
        /*
            This method counts the number of living neighbors a cell has then applies the two rules given at
            the top of this program to decide whether the cell lives or dies
        */

        //We assume a cell has no living neighbors, we increment as we encounter living neighbors
        int livingNeighbors = 0;

        //Relative positions of a cell's neighbors
        //Row first, column second (As opposed to x-axis translation, y-axis translation)
        var neighbors = new[]{
            (-1, 1), //up right
            (-1, 0), //up
            (-1,-1), //up left
            ( 0,-1), //left
            ( 0, 1), //right
            ( 1, 1), //down right
            ( 1,-1), //down left
            ( 1, 0)  //down
        };

        //For every neighboring cell
        foreach (var ls in neighbors){
            //If the neighboring cell is not out of bounds
            if (outOfBounds(i + ls.Item1, j + ls.Item2) == false){
                //If the neighboring cell is alive, increment counter
                if (field[(i + ls.Item1), (j + ls.Item2)] == 1){ livingNeighbors++; }
            }
        }

        //Test for Rule 1: If a living cell has less than 2 living neighbors, or more than 3 living neighbors, it dies
        if((field[i,j] == 1) && (livingNeighbors < 2 || livingNeighbors > 3)){ return 0;}
        //Test for Rule 2: If a dead cell has exactly 3 living neighbors, bring it to life
        else if((field[i,j] == 0) && (livingNeighbors == 3)){ return 1;}
        //If no change should occur, return whatever the cell is currently
        else{ return field[i,j];}
    }

    private void copyTemp(){
        //This copies the contents of the tempField to the field
        for(int i = 0; i < tempField.GetLength(0); i++){
            for(int j = 0; j < tempField.GetLength(1); j++){
                field[i,j] = tempField[i,j];
            }
        }
    }

    private bool outOfBounds(int y, int x){
        if(x < 0 || x >= FIELDSIZE || y < 0 || y >= FIELDSIZE){
            return true;
        }
        else{
            return false;
        }
    }
}