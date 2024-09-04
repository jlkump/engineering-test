Welcome to Devhouse's Engineering Exam. Thank you for taking your time to try our little brain teaser. 
Below are the instructions for the exam.

Introduction:
- The project is built in Unity Version 2022.3.44f1
- This test is designed to be short. The requirements should take no longer than 8hrs but if you are enjoying your time, we have added some bonus point requirements.
- Once you open the project, in the "Scenes" folder will be a scene titled "Playground". 
- This is the scene that you will be working on. It has the Unity Standard 3rd Person Assets.
    - Included in this is a player and player controller so you can move around in the world with ease.
- To open the project, in Unity Hub:
    - click "Add"
    - Open the folder that has the project inside

Prompt:
- Please create a modular inventory system that can be added to chests in world and the player character. 
- Each instance of the inventory system is its own container.
    - ie: You cannot access the chest inventory items when looking at the player inventory items.

Requirements:
- N number of inventory slots 
    - ie: The number of inventory slots for the player should be different from the chest. 
    - ex: Player has 5 inventory slots, a small chest has 10 inventory slots, a large chest has 20 inventory slots, etc
- Items should be able to be picked up and added to inventory
- Items should be able to be removed from inventory
- Items should be stackable up to N numbers in a stack
    - ie: If I pick up 5 apples, they should only take up 1 inventory slot. 
    - The max that a slot can be stacked should be a changeable variable
- Some basic UI to show the inventory - Feel free to grab free assets online if you want to make it pretty.

Bonus Points:
- Use of SCROBS (Scriptable Objects) are highly encouraged
- Have both player inventory and a few chest inventory in the world
- Be able to move items from one inventory to the other

Super Bonus Points:
- Clean, easy to use UI interactions
- The package "DoTween" allows for easy animations. We have included that package in the project should you want to use it.


Submission:
- Please take a screen recording of you demonstrating the inventory system (OBS is a great free recording software)
    - Keep it short, video should be 5 minutes MAX. 
- Please upload the project to a github repository.
    - If you can't upload to github, please return a zipped file with the project.
- Please send an email with the demonstration video and the shared github repository.
    - Video hosting is up to you. You can upload to google drive, youtube, etc, just share the link.
