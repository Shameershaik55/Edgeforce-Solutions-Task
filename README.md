VR Equipment Maintenance – Inventory & Interaction Prototype
A small VR interaction and inventory prototype developed in Unity as part of the Unity Developer assignment for Edgeforce Solutions Pvt. Ltd.

The project demonstrates XR object interaction, contextual interaction prompts, inventory management, stackable and non-stackable item handling, controller-based UI interaction, and prefab-based item retrieval.

Unity Version
Unity 2021.3.30f1 Personal

How to Run the Project
Clone or download this repository.

Open the project using Unity 2021.3.30f1.

Open the Unity Editor.

Navigate to the main demonstration scene:
Assets/Residential/Garage/Demo/Demo Garage

Open the Demo Garage scene.

Enter Play Mode.

Use the XR Device Simulator controls described below to interact with the scene.

XR Device Simulator Controls
The project can be tested directly in the Unity Editor using the XR Device Simulator.

Right Controller
Action	Keyboard / Mouse
Trigger	Left Mouse Button
A Button	B Key
B Button	N Key
Grab	G
Left Controller
Action	Keyboard / Mouse
Trigger	Right Mouse Button
A Button	B Key
B Button	N Key
Grab	G
Player Movement
Action	Keyboard
Move Forward	W
Move Backward	S
Move Left	A
Move Right	D
Inventory Controls
B Button: Open the inventory.

Left Controller Ray: Point at an inventory slot.

Left Controller Trigger: Select an inventory slot.

Right Controller: Grab physical objects.

A Button: Store the currently held item.

Note: The controls above are the keyboard and mouse mappings used with the XR Device Simulator during development.

Project Overview
The prototype is designed as a small VR equipment-maintenance environment inside a garage/workshop.

The player can approach and interact with physical objects using XR controllers. Contextual instructions are displayed depending on the current interaction state.

Features
Interactables: Hover over objects to receive instructions.

Grabbing: Grab objects using the XR controller.

Storage: Store held objects in the inventory.

UI: View stored items through the inventory UI.

Stacking: Stack supported items.

Validation: Prevent duplicate non-stackable items.

Retrieval: Retrieve items using the inventory UI.

Spawning: Spawn a new physical instance of the retrieved item.

Main Interaction Flow
Hover over an object.

Prompt: "Use Grab Button to grab <Item>".

Grab the object using the controller.

Store the item in the inventory.

Open the inventory UI.

Select an item to retrieve.

Spawn the item back into the physical world.

Project Structure
text
Assets/
└── Residential/
    └── Garage/
        └── Demo/
            └── Demo Garage (Scene)
Key Technologies
Unity XR Interaction Toolkit

XR Device Simulator

C# Scripting

Unity UI (uGUI)

Notes
Ensure the XR Device Simulator is active in the scene hierarchy to use keyboard/mouse controls.

This is a prototype focused on interaction logic and inventory systems.

