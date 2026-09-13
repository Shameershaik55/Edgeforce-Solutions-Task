# VR Equipment Maintenance Inventory System

A small VR interaction and inventory prototype built in Unity as part of the Unity Developer assignment for Edgeforce Solutions Pvt. Ltd.

The prototype focuses on reusable VR interaction, inventory management, stackable and non-stackable items, controller-based UI interaction, and clean separation between gameplay logic and UI.

---

## Features

- VR-based object interaction
- Near/Far object interaction
- Left-controller Ray Interactor for inventory UI
- Contextual interaction prompts
- Grab objects using XR controllers
- Store held objects using the A button
- Fixed 3 × 3 inventory with 9 slots
- Stackable item support
- Non-stackable item support
- Maximum stack limits
- Duplicate prevention for non-stackable items
- Inventory-full and stack-full feedback
- Inventory item icons
- Dynamic item quantity display
- Retrieve items from inventory
- Spawn a fresh item prefab when retrieving
- World-space VR inventory UI
- Controller-based inventory button interaction
- Temporary feedback messages that disappear automatically

---

## Demo Flow

The main interaction flow is:

1. Hover over an interactable object.
2. An instruction appears:
   `Use Grab Button to grab <Item>`
3. Grab the object.
4. The instruction changes to:
   `Press A to store <Item>`
5. Press A to store the item.
6. The inventory UI updates with the item's icon and quantity.
7. Open the inventory using the assigned inventory button.
8. Use the left-controller Ray Interactor to select an inventory slot.
9. Retrieve the selected item.
10. A new instance of the item's prefab is spawned into the world.

---

## Inventory Rules

### Stackable Items

Stackable items are stored in the same inventory slot until their maximum stack size is reached.

Example:

```text
Battery 1  → Slot 0: Battery ×1
Battery 2  → Slot 0: Battery ×2
Battery 3  → Slot 0: Battery ×3
