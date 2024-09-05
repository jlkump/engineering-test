# Engineering Test Report - Jonathan Landon Kump
## Time Breakdown
* **Basic Inventory Funtionality** (storage in a container, different storage sizes, item ids and item factory): ~4 hours
* **UI Interactivity & Extended Functionality** (UI to click and hold items, move items between inventories, bug fixing): ~3 hours
* **Persistence Functionality** (save items in inventories, items on the ground, and player position): ~2 hours
* **Additional Testing and Fun Additions** (adding more items, finding 3D models and 2D assets, creating materials, setting new items up with the **Item Factory**)
* **Compiling Report & Demonstration Video**: ~1 1/2 hour

## Requirements Implemented
- Configurable inventory slots with a variable number of slots
- Items can be picked up and added to the inventory
- Items can be removed from the inventory (placed in storage or dropped on the ground)
- Items can be stacked based on the stack size determined by the item configuration
- Interactive UI to display and manage inventories

### Bonus Features Implemented
- Use of a scriptable object in the **InventoryFactory** to manage item prefabs and generate unique item IDs
- Placing the player in the world with various inventory types
- Moving items from one inventory to another

### Super bonus
- Use of the "DOTween" package to animate items on the ground and for item pick-up animations
- Scalable UI compatible with different resolution sizes
- Implemented persistence with save and load functionality (press "K" for save, "L" for load)

## Added Scripts, Assets, and Folders
### InventorySystem
Contains the scripts for the inventory system (both UI and module functionality) as well as the prefab for UI Item Display.
* **InventoryAccesser**: Provides the functionality of an actor to access inventories. Assumes that an actor has an "owned" inventory (e.g., the player's inventory) and a number of interactable inventories in the world. Uses a sphere trigger collision to detect nearby accessible inventories. 
* **InventoryInstance**: Provides the functionality of an inventory, storing an amount of stacks of items in slots. The number of slots and inventory display name can be configured.
* **InventoryUI**: Manages the UI for interacting with inventories, extensible for multiple simultaneous inventories by adjusting the GUI in-editor. Currently shows the primary "owner's" inventory and a single interactable inventory. 
* **ItemUIDisplay**: A smaller class used by InventoryUI to display item slots. **Item Display Prefab** sets up how item slots are displayed.
* **UIHeldItem**: Another smaller class used by InventoryUI to display the currently held item (the one picked up when a UI item slot is clicked).

### InventorySystem/Inventories
Contains prefabs using the inventory system to create inventories of various types. Examples provided are the Player, Barrel, Small Chest, and Large Chest inventories.

### ItemSystem
Contains the scripts for the item system
* **ItemDebugSpawner**: Spawns items based on number keys pressed for debugging.
* **ItemFactory**: ScriptableObject that registers game items, assigns unique IDs, and creates instances of items in the world.
* **ItemInstance**: The actual instance of an item. Defines the maxStacks, the 2d display sprite, and the 3d appearance.
* **ItemCollector**: Component to make an actor able to pick up items off the ground and store it in some **InventoryInstance**.
* **ItemSpawner**: A simple spawner to create item instances based on the Id.

### Scripts
Assorted scripts for general game management
* **GameManager**: Manages persistence across saves, including player position, inventories, and item instances.
* **UIManager**: Manages general UI interactions, including displaying helper text and encapsulating InventoryUI.

### Pretty Models
Various free online models or simple textured shapes to use for item prefabs

### UI Sprites
Various free online assets used for the 2D inventory display

## In-Editor Tree
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
* Installed sprite 2d to manage UI elements, making the 2d background images scale corrently to different resolutions