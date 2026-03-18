# RealTimeMechanics — Real-Time Timer Mechanics in Unity

> **Educational project** — Otus Unity Professional course  
> Учебный проект курса **Otus Unity Pro**

---

## 📖 About the Project / О проекте

This project demonstrates real-time game mechanics in Unity, including countdown timers, offline time tracking, and time synchronisation. It was built as part of the **Otus Unity Professional** course to practice clean architecture patterns such as Dependency Injection, Facade, Observer, and MVC.

Данный проект демонстрирует механику таймеров реального времени в Unity:
- ⏱ **Реализация таймеров** — обратный отсчёт с событиями старта, паузы и окончания
- 🕐 **Реализация часов реального времени** — отслеживание системного времени через `DateTime`
- 🔄 **Реализация синхронизации времени** — корректировка таймера на время, проведённое вне игры (оффлайн-прогресс)

---

## 🎮 Features

| Feature | Description |
|---|---|
| **Countdown Timer** | Async UniTask-based timer with `Play`, `Stop`, `Reset` and progress tracking |
| **Real-Time Clock** | Uses `DateTime.Now` to record the exact moment a timer was started |
| **Offline Sync** | On game load, calculates elapsed real-world time and advances timers accordingly |
| **Reward Chests** | Three chest types (Wood, Steel, Gold) with configurable durations and reward pools |
| **Resource System** | Six resource types: Money, Diamond, Wood, Stone, Iron, Card |
| **Animated UI** | DOTween-powered resource counter with increment / decrement animations |
| **Dependency Injection** | Zenject for clean service wiring throughout the project |

---

## 🏗️ Architecture Overview

```
Assets/
├── Game/
│   ├── TimeReward/          # Core feature: real-time reward timers
│   │   ├── TimeReward.cs        # Timer logic — wraps Countdown, triggers rewards
│   │   ├── IRealtimeTimer.cs    # Interface: TimerStarted event, SynchronizeTime()
│   │   ├── TimeRewardModule.cs  # MonoBehaviour entry point (Zenject injection)
│   │   ├── TimeRewardSaveLoader.cs  # Saves start time; calculates offline delta
│   │   └── Configs/
│   │       ├── TimeRewardConfig.cs  # ScriptableObject: duration + rewards
│   │       ├── WoodChest.asset
│   │       ├── SteelChest.asset
│   │       └── GoldChest.asset
│   ├── Resource/            # Resource storage (Money, Diamond, Wood, Stone, Iron, Card)
│   ├── Reward/              # Reward type enum and player reward data
│   └── View/UI/             # Presenter + View with DOTween animations
│
└── Modules/
    ├── Time/                # Reusable countdown module
    │   ├── ICountdown.cs
    │   └── Base/Countdown.cs    # UniTask async timer with full event system
    └── Entities/            # Lightweight entity-component system (MapEntity / ListEntity)
```

### Key Classes

| Class | Responsibility |
|---|---|
| `Countdown` | Generic async countdown timer; events: `OnStarted`, `OnTimeChanged`, `OnStopped`, `OnEnded`, `OnReset` |
| `TimeReward` | Implements `IRealtimeTimer`; uses `Countdown`, triggers `RewardFacade` when timer ends |
| `TimeRewardSaveLoader` | Persists start timestamp in `PlayerPrefs`; restores offline progress on load |
| `RewardFacade` | Routes reward delivery to the correct `ResourceStorage` using a switch expression |
| `ResourceStorage` | Base class for all resource buckets; exposes `EarnResource` / `SpendResource` |

---

## 🛠️ Tech Stack

| Tool | Version | Purpose |
|---|---|---|
| Unity | 2021.3.7f1 (LTS) | Game engine |
| C# | 9.0 / .NET 4.7.1 | Game logic |
| UniTask | git (Cysharp/UniTask) | Async/await without allocations |
| Zenject | 9.2.0 | Dependency injection |
| DOTween | bundled | UI animations |
| Odin Inspector | 3.1+ | Rich editor inspector views |
| TextMesh Pro | 3.0.6 | High-quality UI text |

---

## 🚀 Getting Started

### Prerequisites
- **Unity 2021.3.7f1** or a compatible LTS version
- **.NET SDK** (included with Unity)
- Odin Inspector license (or replace Odin attributes with standard Unity attributes)

### Running the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/MrRandomise/RealTimeMechanics.git
   ```
2. Open the project folder in **Unity Hub**.
3. Open the scene: `Assets/Scenes/SampleScene.unity`
4. Press **Play** to run.

### Trying the Timer Mechanics
- The scene starts a **Wood Chest** timer (5 seconds by default).
- When the countdown finishes, click **Receive Reward** to collect resources.
- Close the game and reopen it — the timer will account for the time you were away (offline sync).
- Adjust `Duration` and `Rewards` in the chest `ScriptableObject` configs under `Assets/Game/TimeReward/Configs/`.

---

## 📐 Design Patterns Used

- **Observer** — `Countdown` events decouple the timer from all consumers
- **Facade** — `RewardFacade` hides the complexity of multiple resource storages
- **Dependency Injection** — Zenject installers (`LevelInstaller`, `ResourcesInstaller`) wire everything together
- **MVC** — `ResourceListener` (Presenter) observes storage changes and updates `ResourceView` (View)
- **Scriptable Objects** — designer-friendly config assets for chest types

---

## 📄 License

Distributed under the **BSD 2-Clause License**. See [LICENSE](LICENSE) for details.

---

*Built with ❤️ as part of the Otus Unity Professional course.*
