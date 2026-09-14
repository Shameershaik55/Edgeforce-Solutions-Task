# VR Equipment Maintenance – Inventory & Interaction Prototype

A small VR interaction and inventory prototype developed in Unity as part of the **Unity Developer assignment for Edgeforce Solutions Pvt. Ltd.**

The project demonstrates **XR object interaction, contextual interaction prompts, inventory management, stackable and non-stackable items, controller-based UI interaction, and prefab-based item retrieval**.

---

## Unity Version

**Unity 2021.3.30f1 Personal**

---

## How to Run the Project

1. Clone or download this repository.
2. Open the project using **Unity 2021.3.30f1**.
3. Open the Unity Editor.
4. Navigate to the main demonstration scene:

    Assets/
    └── ResidentialGarage/
            └── Demo/
                └── Demo Garage

5. Open the **Demo Garage** scene.
6. Enter **Play Mode**.
7. Use the **XR Device Simulator** controls described below.

---

## XR Device Simulator Controls

The project can be tested directly in the Unity Editor using the **XR Device Simulator**.

### Right Controller
Press "Y" on the keyboard to active Right Controller


| Action | Keyboard / Mouse |
|---|---|
| Trigger | Left Mouse Button |
| A Button | B Key |
| B Button | N Key |
| Grab | G |

### Left Controller
Press "T" on the keyboard to active Left Controller


| Action | Keyboard / Mouse |
|---|---|
| Trigger | Left Mouse Button |
| A Button | B Key |
| B Button | N Key |
| Grab | G |

### Player Movement

| Action | Keyboard |
|---|---|
| Move Forward | W |
| Move Backward | S |
| Move Left | A |
| Move Right | D |

### Inventory Interaction

- **B Button:** Open the inventory.
- Use the **Right Controller Ray** to point at an inventory slot.
- Press the **Right Controller Trigger** to select an inventory slot.
- Use the **Right Controller** to grab physical objects.
- Press **A** to store the currently held item.

> These are the keyboard and mouse mappings configured for testing the XR Device Simulator in the Unity Editor.

---

## Project Overview

This project is a small **VR equipment-maintenance interaction prototype** built inside a garage/workshop environment.

The player can interact with physical objects, receive contextual instructions, store items in a fixed inventory, manage stackable and non-stackable items, and retrieve items through the VR inventory interface.

### Main Interaction Flow

    Hover over an interactable object
            ↓
    "Use Grab Button to grab <Item>"
            ↓
    Grab the object
            ↓
    "Press A to store <Item>"
            ↓
    Press A
            ↓
    Inventory checks the item
            ↓
    Store / Stack / Reject
            ↓
    Inventory UI updates
            ↓
    Open Inventory By click on "N" on keyboard which belongs reference to the "B" key on Controller
            ↓
    Point Right Controller Ray at a slot
            ↓
    Press Trigger
            ↓
    Retrieve Item
            ↓
    Spawn Item Prefab in the world

---

## Architecture Overview

The project separates the main responsibilities into different components so that inventory logic, UI, input, interaction prompts, and item retrieval can be maintained independently.

### InventoryItem

`InventoryItem` contains the data associated with an inventory item.

It includes:

- Item ID
- Item name
- Inventory icon
- Prefab reference
- Stackable state
- Maximum stack size

This allows new item types to be configured without changing the core inventory system.

### InventoryManager

`InventoryManager` manages the actual inventory state and inventory rules.

It handles:

- Adding items
- Checking stackable items
- Increasing stack quantities
- Enforcing maximum stack size
- Preventing duplicate non-stackable items
- Checking inventory capacity
- Removing items
- Providing inventory data to the UI

The inventory contains **9 fixed slots** arranged in a **3 × 3 layout**.

### InventoryInput

`InventoryInput` handles the input used to store the currently held item.

When the player presses the configured **A button**, the held item is passed to the inventory manager and the result is communicated through the interaction prompt.

### InventoryUI

`InventoryUI` is responsible for updating the visual inventory.

It reads the current inventory state from `InventoryManager` and sends the data to the corresponding inventory slots.

The UI itself does not contain the inventory rules.

### InventorySlotUI

`InventorySlotUI` controls the visual information displayed inside an individual inventory slot.

It displays:

- Item icon
- Item quantity

Example:

    Battery
    ×3

### InventorySlotButton

`InventorySlotButton` connects each inventory slot button to its corresponding inventory slot index.

For example:

    Slot 0 → RetrieveItem(0)
    Slot 1 → RetrieveItem(1)
    Slot 2 → RetrieveItem(2)

### InventoryRetrieval

`InventoryRetrieval` handles retrieving items from the inventory.

When a slot is selected:

1. The item data is obtained.
2. One quantity is removed from the inventory.
3. The item's prefab is instantiated.
4. The inventory UI is refreshed.

For stackable items, retrieving an item decreases the quantity by one.

### HeldItemTracker

`HeldItemTracker` detects when an inventory-enabled object is grabbed and passes the grabbed item to the inventory input system.

This keeps XR grab detection separate from inventory storage logic.

### InteractionPrompt

`InteractionPrompt` manages the world-space instruction and feedback text shown to the player.

Examples include:

    Use Grab Button to grab Vest
    Press A to store Vest
    Vest stored successfully
    Unable to store Vest
    Inventory is full

Temporary status messages automatically disappear after a configured duration.

### ItemInteractionPrompt

`ItemInteractionPrompt` connects XR hover and grab events to the interaction prompt.

The displayed instruction changes according to the current interaction state:

    Hover
    → Use Grab Button to grab <Item>

    Grab
    → Press A to store <Item>

---

## Inventory Design

The inventory contains **9 fixed slots**.

### Stackable Items

Multiple copies of the same stackable item are stored in the **same slot** until the configured maximum stack size is reached.

Example:

    Battery 1 → Slot 0: Battery ×1
    Battery 2 → Slot 0: Battery ×2
    Battery 3 → Slot 0: Battery ×3

All copies of the same stackable item remain in the same slot until the maximum stack size is reached.

### Maximum Stack Size

Example:

    Medkit
    Stackable = Yes
    Maximum Stack Size = 2

The behavior is:

    Medkit 1 → Slot 0: Medkit ×1
    Medkit 2 → Slot 0: Medkit ×2
    Medkit 3 → Rejected

A new slot is **not** created after the maximum stack size is reached.

### Non-Stackable Items

Only one copy of a non-stackable item is allowed in the inventory.

Example:

    Vest 1 → Slot 1
    Vest 2 → Rejected

If the same non-stackable item is already present, another copy is not added.

### Inventory Full

If there is no available slot for a new item, the item is rejected and the player receives feedback:

    Unable to store <Item>
    Inventory is full

---

## Important Design Decisions

### Separation of Responsibilities

The project separates inventory logic, UI presentation, input handling, XR interaction, prompts, and item retrieval into individual components.

This makes the system easier to understand, maintain, and extend.

### Data-Driven Item Configuration

Item behavior is controlled through item properties such as:

- Item ID
- Stackable state
- Maximum stack size
- Icon
- Prefab

The inventory logic does not need separate hard-coded rules for every item type.

This makes it easier to add new equipment with minimal changes to the existing system.

### Item Identification

Each item uses a unique **Item ID** to identify its item type.

All physical instances of the same item type use the same Item ID.

This allows the inventory to recognize different physical instances as the same item type.

### Prefab-Based Retrieval

The inventory stores the information required to identify an item and its prefab.

When an item is retrieved, a new physical instance of the corresponding prefab is created.

This keeps inventory data separate from the original physical scene object.

### Fixed Inventory Capacity

The prototype intentionally uses **9 fixed inventory slots** to keep the interaction simple and predictable.

### VR-First Interaction

The prototype is designed around XR controller interaction and controller-based world-space UI rather than traditional desktop interaction.

---

## Example Item Configuration

### Vest

    Item ID: VEST_001
    Item Name: Vest
    Stackable: No
    Maximum Stack Size: 1

### Battery

    Item ID: BATTERY_001
    Item Name: Battery
    Stackable: Yes
    Maximum Stack Size: 10

### Medkit

    Item ID: MEDKIT_001
    Item Name: Medkit
    Stackable: Yes
    Maximum Stack Size: 2

---

## Assumptions

- Each inventory item has a valid Item ID.
- Each inventory item has a valid inventory icon.
- Each retrievable item has a valid prefab reference.
- All physical instances of the same item type use the same Item ID.
- Stack limits are configured individually for each item.
- Non-stackable items are intended to exist only once in the inventory.
- The project is tested using the configured XR Device Simulator and XR controller setup.

---

## Limitations

- The inventory is limited to **9 slots**.
- Inventory contents are not saved between application sessions.
- No multiplayer inventory synchronization is implemented.
- No drag-and-drop inventory organization is implemented.
- The prototype currently focuses on interaction and inventory functionality rather than a complete multi-stage maintenance training system.
- Some advanced XR interaction behavior may require further tuning when used with physical VR hardware.
- The current prototype is primarily intended to demonstrate Unity, C#, XR interaction, UI interaction, and inventory-system fundamentals.

---

## Future Improvements

Possible future extensions include:

- Equipment maintenance tasks
- Tool-to-component validation
- Guided maintenance workflows
- Training scoring and feedback
- Haptic feedback
- Audio instructions
- Inventory save/load
- Additional equipment types
- More advanced inventory management
- Training performance tracking
- Expanded VR maintenance scenarios

---

## Project Scene Location

The main demonstration scene is located at:

    Assets/Residential/Garage/Demo/Demo Garage

Open the **Demo Garage** scene to run the prototype.

---

## Important Scripts

The main scripts used in the prototype are:

    InventoryItem.cs
    InventoryManager.cs
    InventoryInput.cs
    InventoryUI.cs
    InventorySlotUI.cs
    InventorySlotButton.cs
    InventoryRetrieval.cs
    HeldItemTracker.cs
    InteractionPrompt.cs
    ItemInteractionPrompt.cs

---

## Repository Structure

The main demonstration environment is located under:

    Assets/
    └── Residential/
        └── Garage/
            └── Demo/
                └── Demo Garage

---
## Demo Video

[Watch the Project Demo](https://drive.google.com/file/d/1g-GmrqSzQCca-w8cf8QdNXjYItuNUbBO/view?usp=sharing)


## Author

**Shameer Shaik**

Unity | XR | AR/VR Development
