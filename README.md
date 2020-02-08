# CitySettlerBalancing

Hey guys!

Once you download this package and run in Unity, you will notice that there are only three script files in this package!
Actually, all you need to care is just a "GameManager" script. I wrote some annotations in the GameManager Scripts.
(If you have basic understanding about C# language, you may intuitively understand my code.)

## Basic Instrutions ##
Once you run this package in Unity you should open the "SampleScene". You can find it in the [Project] field -> Assets -> Scences and double click the SampleScene.  

After you open the SampleScene, you may want to click the "GameManager" in the [Inspector] field.

Then, you can see the GameManager script which contains buildings, resources, indicators, and algorithm in the [Inspector] field. You can type integer numbers in [Buildings] Section. Enter numbers in [Buildings] section so that the total sum is less than 3; we don't want the player to start with more than 3 buildings. ex) 2 for Smithy and 1 for Park.

After you put some numbers in [Buildings] section, please click the play button. It's located at center top of the Unity.

Once you click the play button, pressing the "R" button on the keyboard will start algorithm calculations and change the number of indicators and resources depending on the buildings the player currently have.

★ The goal of this program is to find the optimal balance among each buildings, indicators, resources, and algorithms by repeatedly pressing the R button and grasping the fluctuation trend of the numbers.

You can add or subtract the number of buildings while running the game by simply change integer numbers you typed in [Building] Section.

If the population_f is smaller than the total number of the buildings, the buildings automatically operate only as many as the population they have.


## Console Tab ##
If you can't see the console tab in your Unity, go to "Window" -> "General" -> "Console". Then you will be able to see the console tab. 

The console tab will provide useful information as the game progresses. For example, if you no longer have enough resources to run buildings, it will notify you that you can not operate some buildings because of lack of resources.

