# Rounds Text Adventure

A text-based adventure game built in Unity 2022.3, demonstrating advanced software architecture patterns, dependency injection, and data-driven game design principles.

## 🎮 Game Overview

Rounds Text Adventure is an interactive fiction game where players explore rooms, interact with items, and solve puzzles through text commands. The game features a room-door-item system where players navigate through connected spaces by unlocking doors, collecting items, and examining their environment.

### Core Gameplay Loop
1. **Explore** - Enter rooms and examine your surroundings using the `Look` command
2. **Interact** - Discover items and interact with them through various actions (`Read`, `Take`, `Unlock`)
3. **Solve** - Use collected items to unlock doors and progress through the adventure
4. **Progress** - Navigate through interconnected rooms to complete the narrative

### Available Commands
- `Look [item]` - Examine the current room or a specific item
- `Take [item]` - Pick up an item and add it to your inventory
- `Read [item]` - Read written content on an item
- `Enter [door]` - Pass through an unlocked door
- `Unlock [door]` - Unlock a door using required items
- `Narrate` - Display narrative context for the current location

### How to Play
1. Enter play mode in Unity
2. Follow the tutorial prompt
3. Type `enter game` into the input field to begin
4. Use commands to interact with the world

**Gameplay Tips:**
- Some doors require specific keys to unlock
- To change rooms: first `unlock door`, then `enter door`
- Each new room must be explored with `look` before interacting with items inside

## 🏗️ Architecture & Design Patterns

This project demonstrates professional game development practices through clean architecture and proven design patterns.

### 1. **Dependency Injection (Zenject/Extenject)**
The entire game uses the Zenject framework for dependency management, promoting:
- **Decoupled Systems** - Components depend on interfaces, not concrete implementations
- **Testability** - Easy to mock dependencies for unit testing
- **Maintainability** - Clear dependency graphs and single responsibility
- **Flexibility** - Runtime injection allows easy swapping of implementations

```csharp
// MainInstaller.cs - Centralized dependency configuration
public override void InstallBindings()
{
    Container.BindInterfacesTo<UserInterfaceManager>().FromComponentsInHierarchy().AsSingle();
    Container.Bind<IPlayerContext>().To<PlayerContext>().AsSingle();
    Container.Bind<IInventory>().To<Inventory>().AsSingle();
    Container.Bind<IInputProcessor>().To<InputProcessor>().AsSingle();
}
```

### 2. **Interface Segregation Principle (ISP)**
Multiple focused interfaces ensure components only expose what's necessary:
- `IItemContext` - Basic item properties and behavior
- `IDoorContext` - Door-specific functionality (locking, unlocking)
- `IRoomContext` - Room-specific capabilities
- `IPlayerContext` - Player state and room management
- `IInventory` - Inventory operations
- `IInputProcessor` - Input handling abstraction
- `ITextDisplay` / `INotificationDisplay` - UI abstraction

This approach ensures actions only access the methods they need, preventing tight coupling.

### 3. **Data-Driven Design with ScriptableObjects**
All game content is data-driven using Unity's ScriptableObject system:

#### **Component System**
- **Rooms** - Define spaces with items and doors
- **Doors** - Connect rooms with unlock requirements
- **Items** - Collectible and interactive objects
- **Actions** - Reusable behavior definitions

This enables:
- **Designer Empowerment** - Non-programmers can create content
- **Runtime Performance** - Shared data across instances
- **Modularity** - Content is completely decoupled from code
- **Rapid Iteration** - Changes don't require code recompilation

### 4. **Command Pattern for Actions**
Actions are implemented as ScriptableObjects following the Command pattern:

```csharp
public abstract class ActionBase : ScriptableObject
{
    public abstract ActionType Type { get; }
    public abstract void Execute(IItem itemContext, string message, string negativeMessage);
}
```

Benefits:
- **Encapsulation** - Each action encapsulates behavior
- **Reusability** - Actions are data assets that can be reused across items
- **Extensibility** - New actions can be added without modifying existing code
- **Inspector Configuration** - Designers attach actions to items visually

### 5. **Generic Context Typing**
Actions use generic constraints to ensure type safety:

```csharp
public abstract class ActionBase<TContext> : ActionBase where TContext : IItemContext
{
    protected abstract void Execute(TContext context, string message, string negativeMessage);
}
```

This pattern:
- Guarantees compile-time type safety
- Prevents invalid action-context pairings
- Enables context-specific functionality (e.g., `UnlockAction` only works with `IDoorContext`)

### 6. **Observer Pattern for Input**
The input system uses events for loose coupling:

```csharp
public event Action<ActionType, string> ActionCommandReadyEvent;
```

This decouples:
- Input parsing from action execution
- UI from game logic
- System components from each other

### 7. **Composition Over Inheritance**
Items are composed of actions rather than inheriting behavior:
- Items contain lists of `ActionData` 
- Actions are composed at runtime based on ScriptableObject references
- New item types don't require new code, just new data configurations

### 8. **Single Responsibility Principle**
Each class has one clear purpose:
- `GameManager` - Bootstraps the game and sets initial state
- `PlayerContext` - Manages player state and current room context
- `InputProcessor` - Parses and validates user input
- `Inventory` - Stores and retrieves items
- `UserInterfaceManager` - Handles all UI rendering and input
- Individual actions - Each action type handles one specific behavior

### 9. **State Management Pattern**
Door states are managed through a dedicated struct:

```csharp
public struct DoorState
{
    public bool IsUnlocked { get; set; }
}
```

This pattern:
- Preserves runtime state separately from ScriptableObject data
- Prevents unwanted persistence in the editor
- Clearly separates design-time data from runtime state

## 🎯 SOLID Principles Application

### **Single Responsibility**
Every class has one reason to change:
- `PlayerContext` manages player state
- `Inventory` manages item storage
- `InputProcessor` handles input parsing
- Actions handle specific behaviors

### **Open/Closed Principle**
The system is open for extension, closed for modification:
- New actions can be added by creating new ScriptableObjects
- New item types can be created through data, not code
- Action system supports new behaviors without changing existing code

### **Liskov Substitution**
All implementations are substitutable for their interfaces:
- Any `IInventory` implementation works with the system
- Any `IInputProcessor` can replace the default
- Actions work with any context implementing the required interface

### **Interface Segregation**
Interfaces are focused and minimal:
- `IItemContext` provides only basic item data
- `IDoorContext` extends it with door-specific functionality
- Actions only depend on the interface they need

### **Dependency Inversion**
High-level modules don't depend on low-level modules:
- `GameManager` depends on `IPlayerContext`, not `PlayerContext`
- `PlayerContext` depends on `IInventory`, not concrete `Inventory`
- All dependencies are injected through interfaces

## 🔧 Technical Implementation

### Project Structure
```
Assets/
├── Configurations/          # All game data (ScriptableObjects)
│   └── Game Data/
│       ├── Actions/         # Reusable action definitions
│       ├── Doors/           # Door configurations
│       ├── Items/           # Item definitions
│       └── Rooms/           # Room layouts
├── Scripts/
│   ├── Game/
│   │   ├── Actions/         # Action implementations
│   │   ├── Components/      # Core game components (Room, Door, Item)
│   │   ├── InputSystem/     # Input handling
│   │   └── InventorySystem/ # Item management
│   ├── UserInterface/       # UI management
│   └── MainInstaller.cs     # Dependency injection configuration
├── Scenes/
└── Settings/
```

### Key Systems

#### **Input System**
- Text-based command parsing
- Action-parameter separation
- Validation and error handling
- Event-driven architecture

#### **Inventory System**
- Dictionary-based storage for O(1) lookup
- Interface-based design for flexibility
- Simple, focused API

#### **Player Context**
- Room state management
- Item registration and lookup
- Input event handling
- Context switching between rooms

#### **Action System**
- ScriptableObject-based commands
- Generic type constraints for safety
- Context-aware execution
- Reusable across all items

## 📦 Plugins & Packages

### Core Packages
- **Zenject (Extenject)** - Dependency Injection Framework
  - Industry-standard DI container for Unity
  - Enables testable, maintainable code architecture
  - Provides method injection, property injection, and constructor injection

- **Universal Render Pipeline (URP) 14.0.12** - Modern rendering pipeline
  - Optimized 2D rendering
  - Better performance than built-in pipeline

- **TextMesh Pro 3.0.7** - Advanced text rendering
  - Rich text formatting with color-coded item names
  - Better text quality and performance than legacy UI text

### Unity Built-in Modules
- **Unity UI (uGUI)** - User interface system

## 💡 Design Decisions & Rationale

### Why ScriptableObjects?
- **Memory Efficiency** - Single instance shared across references
- **Designer-Friendly** - Create content without touching code
- **Version Control** - YAML serialization works well with Git
- **Runtime Performance** - No instantiation overhead

### Why Zenject?
- **Industry Standard** - Widely used in professional Unity development
- **Testability** - Enables proper unit testing practices
- **Decoupling** - Reduces dependencies between systems
- **Scalability** - Supports large, complex projects

### Why Interface-Heavy Design?
- **Flexibility** - Easy to swap implementations
- **Testing** - Mock dependencies for unit tests
- **Documentation** - Interfaces serve as contracts
- **Future-Proofing** - Changes to implementations don't affect dependents

### Why Data-Driven Approach?
- **Content Iteration** - Designers can modify content without programmer involvement
- **Modding Support** - External content creation becomes possible
- **Debugging** - Easy to track which data is causing issues
- **Separation of Concerns** - Logic and content are completely separate

## 🎓 Learning Outcomes & Skills Demonstrated

This project showcases proficiency in:

### Software Engineering
- ✅ SOLID principles application
- ✅ Design pattern implementation (Command, Observer, Strategy)
- ✅ Dependency injection and inversion of control
- ✅ Interface-driven development
- ✅ Generic programming and type constraints

### Unity-Specific Skills
- ✅ ScriptableObject architecture
- ✅ Event-driven programming
- ✅ Unity lifecycle management
- ✅ UI system implementation (TextMesh Pro, uGUI)
- ✅ Data-driven game design

### Best Practices
- ✅ Clean, maintainable code structure
- ✅ Self-documenting code with clear naming
- ✅ Proper separation of concerns
- ✅ Scalable architecture for future expansion
- ✅ Performance-conscious design decisions

## 📝 Code Quality Standards

This project follows established coding conventions:
- Self-explanatory variable and method names
- XML documentation on public APIs
- Consistent formatting and style
- Constants instead of magic numbers
- Organized file structure matching namespaces
---

**Built with Unity 2022.3 LTS | Universal Render Pipeline 2D**
