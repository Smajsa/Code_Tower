# Code_Tower
Coding language using GameObjects as entities in Unity

**Goals**
This language intends to be easy, reliable, and fun. It encourages simple data manipulation and observation of control flow as a program executes. The language intentionally runs slowly so that integers can be observed as they are processed.

**How To Use**
The language is, at face value, ultimately very simple. It can only handle Integer datatypes, and allows for increment, decrement, Comparisons and Loops. All Entities are stored in the Prefabs folder.
Create Program Structure through the unity editor, you will almost entirely be dealing with the prefab directory, as this is where all entities are stored

_**Interactions:**_
On play (button on the top middle), move the character with _WASD_, jump with _SPACE_, in order to run a program, you will need an integer. Go to the Integer farm to find one.
Grab an integer with the _Left Shift button_ while the player character hovers over the Integer. You can grab as many Integers as you like.
Once near an interactable container (either the Button or Compare entities), place an integer with _Z_.  **_Integers function in Queue Order, the last one you grab is the first one that appears_**

**_On Containers_**
This is where most of the coding goes. Every Entity (besides the Integer) has a container as a child object. There are three public references available in every child.
- Integer Prefab: Drag the integer prefab here to ensure appropriate references can be made, or else an error will be thrown
- Output [Optional]: Link where the integer will be parsed next, if left blank, this is an end state of the program
- InLoop [Optional]: If you decide to create a loop here, drag the In_Loop prefab here to ensure it functions correctly, nothing happens otherwise


**Parsing Entities**
_Button_: Starts the program **(Place an Integer datatype in the lower container in order to start, or nothing will happen)**, then press the button with the player character

_Integer_: An integer datatype entity, the lowest form of data this language handles and what will be manipulated with this language.

_Increment, Decrement_: Increment Adds one to the integer data, Decrement... decreases one

_Compare_: This and the Button entity are the only containers that can be interacted with (specifically, the red one). If no integer is placed here it defaults to 0, otherwise when an integer is parsed here, whatever you placed is compared, and the integer can move in one of three directions, less than (left), greater than (right), or equal to (down). Each with their own containers for further parsing if so desired, as well as loop compatibility.

_In_Loop_: While similar to Out_Loop, this takes in both the container it is linked to, and the Out_Loop, which is where you want the integer to exit at this state of the program. Functions like a "Jump" function in old C

_Out_Loop_: Takes in a reference to the container it will drop the integer at **Note, feel free to color the "portal" assets to better know which loop jumps where**. I also recommend making the portal a child of whatever container it references

Thats all, Happy Programming!
*Note, it is possible to grab an integer mid program if you are skilled enough at platforming, feel free to challenge yourself
