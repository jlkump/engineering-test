## Report

### Added Scripts, Assets, and Folders
* InventorySystem -> Contains the scripts for the inventory system (both UI and module functionality) as well as the prefab for UI Item Display.
    * **InventoryAccesser**: Provides the functionality of an actor to access inventories. Assumes that an actor has an "owned" inventory, which in this case is the player inventory, and a number of interactable inventories in the world. Uses a sphere trigger collision to detect nearby accessible inventories. 
    * **InventoryInstance**: Provides the functionality of an inventory, storing an amount of stacks of items in slots. # of slots and inventory display name can be configured.
    * **InventoryUI**: Provides the UI functionality to interact with an inventory. Extensible to provide any number of simultaneous Inventories by only changing the GUI in-editor. Currently shows the primary "owner's" inventory and a single interactable inventory. 
    * **ItemUIDisplay**: A smaller class used by InventoryUI to display item slots. **Item Display Prefab** sets up how item slots are displayed.
    * **UIHeldItem**: Another smaller class used by InventoryUI to display the currently held item (the one picked up when a UI item slot is clicked).
* InventorySystem/Inventories -> Contains prefabs using the inventory system to create inventories of various types. Examples provided are the Player, Barrel, Small Chest, and Large Chest inventories.
* ItemSystem -> Contains the scripts for the item system, notably:
    * **ItemDebugSpawner**: A simple debug spawner that creates items based on the number pressed.
    * **ItemFactory**: The Scriptable Object in which items are registered for the game, assigning each item prefab a unique Id.
    * **ItemInstance**: The actual instance of an item. This is used to define an item prefab, including the maxStacks, the 2d display sprite, and the 3d appearance.
    * **ItemCollector**: The attachable component to make an actor able to pick up items off the ground and store it in some **InventoryInstance**
    * **ItemSpawner**: A simple spawner to create item instances based on the Id.
* Scripts
    * **GameManager**: Used for persistance between saves. Saves the player position, inventories, and item instances (and their positions)
    * **UIManager**: Used for general UI interactions, mainly just to display the helper text at the right times and to encapsulate the InventoryUI.
* Pretty Models
    * Various free online models or simple textured shapes to use for item prefabs
* UI Sprites
    * Various free online assets used for the 2D inventory display

### In-Editor Tree
* Added *PlayerInventoryCamera*, a new cinemachine virtual camera that swaps with the 3rd-person player camera to give a closer-up view for the player when they look at their inventory. Composited so that the player is on the left.
* Added various prefab inventories in the game world to interact with
* Added *Game Manager* as a singleton instance of **GameManager**
* Modified *Player* to have new children:
    * *ItemCollector*: Has the **ItemCollector** script applied with a sphere trigger collider for detection.
    * *PlayerInventory*: Has the **InventoryInstance** & **InventoryAccesser** scripts applied for player inventory functionality as well as a sphere trigger collider for detecting other accessible inventories.
    * *UI*: Has all the UI elements for display, including helper text, **UIManager**, and **InventoryUI**
    * *Drop Item Location*: A simple transform to mark where the player drops items (done by clicking a held item outside the UI or by not placing a held item back in a slot)
    * *DebugSpawner*: Has the **ItemDebugSpawner** script applied to allow the player to spawn in items by pressing number keys 0-9

### Other Assorted Edits
* Added a collision layer for items and player so that the items interact with the world collision, but does not interfere with the player's movement. Also items do not collide with themselves.
* Installed sprite 2d to manage UI elements, making the 2d background images scalable to different resolutions