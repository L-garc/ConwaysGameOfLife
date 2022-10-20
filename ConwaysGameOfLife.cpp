// ConwaysGameOfLife.cpp
// 
// If a cell has less than two neighbors, it dies
// A live cell with 2 or 3 living neighbors, continues to next generation
// A live cell with more than 3 living neighbors dies
// A dead cell with 3 live neighbor cells comes to life
//

#include <iostream>
using namespace std;

const int FIELDSIZE = 10;

//Each index in the field is a cell, 1 is a live cell, 0 is a dead cell
bool field[FIELDSIZE][FIELDSIZE] = {
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,1,1,0,1,0,0,0,0},
                       {0,0,0,1,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0},
                       {0,0,0,0,0,0,0,0,0,0} };

//Field to hold all the changes made
//Immediately applying a change to the original field causes bugs
bool tempField[FIELDSIZE][FIELDSIZE] = {0};

//Function prototypes
void setNextGeneration();
bool checkLife(bool cellState, int x, int y);
bool outOfBounds(int x, int y);
void printField();
void setTemp(bool state, int x, int y);
void applyTemp();

int main()
{
    printField(); //Print the initial state of the field (the 0th generation)

    while (true) {
        setNextGeneration(); //Determine what the next generation looks like
        printField(); //Print that generation
    }
}

void setNextGeneration() {
    //Creating a variable to hold the state, which is determined by a function makes the code easier to read
    //when that same value is passed to another method
    bool state = 0; 

    for (int x = 0; x < FIELDSIZE; x++) {
        for (int y = 0; y < FIELDSIZE; y++) {
            //the state of the cell to place in tempField depends on current state and its location & neighbors
            state = checkLife(field[x][y], x, y);

            setTemp(state, x, y); //Save the changes that should occur in tempField
        }
    }

    applyTemp(); //Apply the changes that should occur (stored in tempField) to the original field
}


bool checkLife(bool cellState, int x, int y) {
    //For tracking living neighbors of a cell
    int livingNeighbors = 0;

    //relative locations of neighbors of a cell
    int neighboringCells[8][2] = {
        { 1,-1}, //Up right
        { 0,-1}, //Up
        {-1,-1}, //Up left
        {-1, 0}, //Left
        {-1, 1}, //Left down
        { 0, 1}, //Down
        { 1, 1}, //Down right
        { 1, 0}, //Right
    };

    //Iterate through the list of locations to check all those locations
    for (auto ls : neighboringCells) {
        //If location is not out of bounds
        if (outOfBounds(x + ls[0], y + ls[1]) != true) {
            //If a neighbor is alive, increment counter
            if (field[x + ls[0]][y + ls[1]] == 1) { livingNeighbors++; }
        }
    }

    //If less than 2 living neighbors, or more than 3, the cell dies
    if (cellState == 1 && (livingNeighbors < 2 || livingNeighbors > 3)) {
        return 0;
    }
    //If a dead cell has 3 live neighbors, then it comes to life
    if (cellState == 0 && livingNeighbors == 3) {
        return 1;
    }


}

bool outOfBounds(int x, int y) {
    //If a either x or y is less than zero or greater than the size of the array, out of bounds is true
    //Otherwise false
    if (x < 0 || y < 0 || x > FIELDSIZE || y > FIELDSIZE){
        return true;
    }
    else {
        return false;
    }
}

void printField() {
    for (int x = 0; x < FIELDSIZE; x++) {
        for (int y = 0; y < FIELDSIZE; y++) {
            cout << field[x][y] << " ";
        }
        cout << "\n";
    }
    cout << "\n";
}

void setTemp(bool state, int x, int y) {
    tempField[x][y] = state;
}

void applyTemp() {
    for (int x = 0; x < FIELDSIZE; x++) {
        for (int y = 0; y < FIELDSIZE; y++) {
            field[x][y] = tempField[x][y];
        }
    }
}