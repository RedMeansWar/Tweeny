# Tweeny 🎨

A modern, powerful, and feature-rich tweening library for C# and Unity. Based on Nick Gravelyn's TinyTween, completely rewritten with modern C# features and massively enhanced functionality.

## ✨ Features

### Core Features
- 🎯 **32+ Easing Functions** - From Linear to Bounce, plus Smoothstep variants
- ⏰ **Delay Support** - Start tweens after a specified delay
- 🔄 **Looping** - Restart, PingPong, and Yoyo modes with configurable counts
- 📞 **Callbacks** - OnStart, OnUpdate, OnComplete, and OnLoop events
- ⏯️ **Full Playback Control** - Pause, Resume, Stop, Restart, and Reverse
- 🎮 **Unity Ready** - Built-in support for Vector2/3/4, Color, and Quaternion
- 🔧 **Extensible** - Easy to create custom tween types
- ⛓️ **Method Chaining** - Fluent API for clean, readable code

### Advanced Features ⚡
- ⏱️ **Time Scale** - Speed up or slow down tweens dynamically (2x, 0.5x, etc.)
- 📊 **Progress Property** - Get or set normalized progress (0-1) to jump to any point
- 🔄 **Reverse()** - Reverse direction mid-flight
- ♻️ **Restart()** - Replay without recreating the tween
- 🎭 **Yoyo Mode** - Smooth back-and-forth motion
- 🔗 **Sequences** - Chain multiple tweens to play one after another
- ✅ **IsComplete Property** - Quick check if tween is done
- 🎯 **Static Helpers** - Clean one-liner tween creation with `TweenExtensions.To()`
- 🎨 **Dynamic Easing** - Change easing function mid-tween with `SetEase()`

## 📦 Installation

Simply add `Tweeny.cs` to your project. For Unity, the library automatically detects the Unity environment and enables additional tween types.

## 🚀 Quick Start

### Basic Usage

```csharp
using Tweeny;

// Create a float tween
var tween = new FloatTween();
tween.Start(0f, 100f, 2f, Ease.QuadInOut);

// Update in your game loop
tween.Update(deltaTime);

// Access the current value
Console.WriteLine(tween.CurrentValue);
```

### With All the Bells and Whistles

```csharp
var tween = new FloatTween()
    .SetDelay(0.5f)                    // Wait 0.5 seconds before starting
    .SetLoop(LoopType.Yoyo, 3)         // Yoyo 3 times
    .SetTimeScale(1.5f)                // Play at 1.5x speed
    .OnStarted(() => Debug.Log("Go!"))
    .OnUpdated(v => Debug.Log($"Value: {v}"))
    .OnCompleted(() => Debug.Log("Done!"));
    
tween.Start(0f, 100f, 2f, Ease.ElasticOut);
```

### Unity One-Liner

```csharp
// Move object up 5 units with bounce
TweenExtensions.To(
    transform.position,
    transform.position + Vector3.up * 5f,
    2f,
    pos => transform.position = pos,
    Ease.BounceOut
);
```

## 📚 Built-in Tween Types

### Core
- `FloatTween` - For `float` values

### Unity (when UNITY_5_3_OR_NEWER is defined)
- `Vector2Tween` - For `Vector2` values
- `Vector3Tween` - For `Vector3` values
- `Vector4Tween` - For `Vector4` values
- `ColorTween` - For `Color` values
- `QuaternionTween` - For `Quaternion` values

### Sequences
- `TweenSequence` - Chain multiple tweens together

### Custom Types

Create tweens for your own types easily:

```csharp
public struct MyStruct
{
    public float X;
    public float Y;
}

var tween = new Tween<MyStruct>((start, end, t) => new MyStruct
{
    X = start.X + (end.X - start.X) * t,
    Y = start.Y + (end.Y - start.Y) * t
});

tween.Start(
    new MyStruct { X = 0, Y = 0 },
    new MyStruct { X = 100, Y = 100 },
    2f,
    Ease.SineInOut
);
```

## 🎨 Easing Functions

Tweeny includes 32+ easing functions:

### Basic
- `Linear` - Constant speed

### Polynomial
- `QuadIn`, `QuadOut`, `QuadInOut` - Quadratic (x²)
- `CubicIn`, `CubicOut`, `CubicInOut` - Cubic (x³)
- `QuartIn`, `QuartOut`, `QuartInOut` - Quartic (x⁴)
- `QuintIn`, `QuintOut`, `QuintInOut` - Quintic (x⁵)

### Trigonometric
- `SineIn`, `SineOut`, `SineInOut` - Sine wave curves

### Exponential
- `ExpoIn`, `ExpoOut`, `ExpoInOut` - Dramatic acceleration/deceleration

### Circular
- `CircIn`, `CircOut`, `CircInOut` - Circular arc curves

### Special Effects
- `ElasticIn`, `ElasticOut`, `ElasticInOut` - Spring-like oscillation 🌊
- `BackIn`, `BackOut`, `BackInOut` - Overshoots the target 🎯
- `BounceIn`, `BounceOut`, `BounceInOut` - Bouncing effect ⚽

### Smooth Interpolation
- `Smoothstep` - Very smooth hermite interpolation
- `Smootherstep` - Even smoother (Ken Perlin's improved version)

### Visual Preview

```
Linear:        ___/
QuadOut:       __/
ElasticOut:    _/\/\___
BackOut:       __/^\_
BounceOut:     _/\_/\_/
Smootherstep:  ___/‾‾‾
```

## 🎯 Advanced Features

### Time Scale

Speed up or slow down tweens dynamically:

```csharp
var tween = new FloatTween()
    .SetTimeScale(2f)  // Double speed
    .Start(0f, 100f, 2f, Ease.Linear);

// Change speed during playback
tween.TimeScale = 0.5f;  // Slow motion
tween.TimeScale = 0f;    // Freeze time
```

### Progress Control

Jump to any point in the tween:

```csharp
var tween = new FloatTween();
tween.Start(0f, 100f, 2f, Ease.QuadInOut);

tween.Progress = 0.5f;  // Jump to halfway
tween.Progress = 0.75f; // Jump to 75%
tween.Progress = 1f;    // Jump to end

// Also get current progress
float p = tween.Progress; // Returns 0-1
```

### Restart

Replay the tween without recreating it:

```csharp
var tween = new FloatTween();
tween.Start(0f, 100f, 2f, Ease.BounceOut);

// ... tween completes ...

tween.Restart(); // Play again!
```

### Reverse

Change direction mid-flight:

```csharp
var tween = new FloatTween();
tween.Start(0f, 100f, 3f, Ease.Linear);

tween.Update(1.5f); // Halfway through

tween.Reverse(); // Now goes back to 0
```

### Looping Modes

#### Restart Loop
```csharp
tween.SetLoop(LoopType.Restart, 3);  // Loop 3 times
```

#### PingPong Loop
Swaps start/end values on each loop:
```csharp
tween.SetLoop(LoopType.PingPong, 5);  // Ping-pong 5 times
```

#### Yoyo Loop (New!)
Smooth back-and-forth using the same easing:
```csharp
tween.SetLoop(LoopType.Yoyo, 5);  // Yoyo 5 times
```

#### Infinite Loop
```csharp
tween.SetLoop(LoopType.Restart, -1);  // Loop forever
```

### Delays

Start tweens after a delay:

```csharp
tween
    .SetDelay(1.5f)  // Wait 1.5 seconds
    .Start(0f, 100f, 2f, Ease.QuadInOut);
```

### Sequences

Chain multiple tweens together:

```csharp
var tween1 = new FloatTween();
tween1.Start(0f, 50f, 1f, Ease.QuadIn);

var tween2 = new FloatTween();
tween2.Start(50f, 100f, 1f, Ease.QuadOut);

var sequence = TweenExtensions.Sequence()
    .Append(tween1)
    .Append(tween2)
    .OnCompleted(() => Debug.Log("Sequence done!"))
    .Start();

// Update the sequence
sequence.Update(deltaTime);
```

### Callbacks

```csharp
tween
    .OnStarted(() => Debug.Log("Tween started"))
    .OnUpdated(value => Debug.Log($"Current: {value}"))
    .OnLooped(() => Debug.Log("Loop iteration complete"))
    .OnCompleted(() => Debug.Log("All done!"));
```

### Dynamic Easing

Change the easing function mid-tween:

```csharp
var tween = new FloatTween();
tween.Start(0f, 100f, 3f, Ease.Linear);

// Change easing partway through
tween.SetEase(Ease.BounceOut);
```

### Helper Extensions

Clean, concise one-liner syntax:

```csharp
// Float tween
TweenExtensions.To(0f, 100f, 2f, 
    value => Console.WriteLine(value),
    Ease.ElasticOut
);

// From syntax
TweenExtensions.From(100f, 0f, 2f,
    value => Console.WriteLine(value),
    Ease.BackOut
);

#if UNITY_5_3_OR_NEWER
// Unity Vector3
TweenExtensions.To(
    transform.position,
    targetPosition,
    2f,
    pos => transform.position = pos,
    Ease.QuadOut
);
#endif
```

### IsComplete Property

Quick check if tween is done:

```csharp
if (tween.IsComplete)
{
    Debug.Log("Tween finished!");
}
```

## 🎮 Unity Examples

### Move Object

```csharp
var tween = new Vector3Tween();
tween
    .OnUpdated(pos => transform.position = pos)
    .Start(
        transform.position,
        targetPosition,
        2f,
        Ease.QuadOut
    );
```

### Fade Color

```csharp
var tween = new ColorTween();
tween
    .OnUpdated(color => spriteRenderer.color = color)
    .Start(Color.white, Color.red, 1f, Ease.SineInOut);
```

### Rotate with Yoyo

```csharp
var tween = new QuaternionTween();
tween
    .SetLoop(LoopType.Yoyo, -1)
    .OnUpdated(rot => transform.rotation = rot)
    .Start(
        Quaternion.identity,
        Quaternion.Euler(0, 180, 0),
        2f,
        Ease.SineInOut
    );
```

### Slow Motion Effect

```csharp
var tween = new Vector3Tween();
tween
    .SetTimeScale(0.25f)  // Slow motion!
    .OnUpdated(pos => transform.position = pos)
    .Start(startPos, endPos, 2f, Ease.Linear);

// Match Unity's time scale dynamically
tween.TimeScale = Time.timeScale;
```

### Complex Sequence

```csharp
var moveUp = new Vector3Tween()
    .OnUpdated(p => transform.position = p);
moveUp.Start(startPos, startPos + Vector3.up * 3f, 1f, Ease.QuadOut);

var moveRight = new Vector3Tween()
    .OnUpdated(p => transform.position = p);
moveRight.Start(startPos + Vector3.up * 3f, 
                startPos + Vector3.up * 3f + Vector3.right * 3f, 
                1f, Ease.Linear);

var sequence = TweenExtensions.Sequence()
    .Append(moveUp)
    .Append(moveRight)
    .Start();
```

## 🔧 Tween Manager Pattern

Manage multiple tweens efficiently:

```csharp
public class TweenManager
{
    private List<ITween> tweens = new List<ITween>();

    public void Add(ITween tween) => tweens.Add(tween);

    public void UpdateAll(float deltaTime)
    {
        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            tweens[i].Update(deltaTime);
            
            if (tweens[i].IsComplete)
                tweens.RemoveAt(i);
        }
    }

    public void StopAll() => tweens.ForEach(t => t.Stop(StopBehavior.AsIs));
    public void PauseAll() => tweens.ForEach(t => t.Pause());
    public void ResumeAll() => tweens.ForEach(t => t.Resume());
    public void SetGlobalTimeScale(float scale) => 
        tweens.ForEach(t => t.TimeScale = scale);
}
```

## 📖 API Reference

### Tween&lt;T&gt; Class

#### Properties
- `T CurrentValue` - The current interpolated value
- `T StartValue` - The starting value
- `T EndValue` - The ending value
- `float Duration` - Total duration in seconds
- `float CurrentTime` - Elapsed time since start
- `TweenState State` - Current state (Delayed, Running, Paused, Stopped)
- `bool IsComplete` - Whether the tween has finished
- `float TimeScale` - Speed multiplier (get/set)
- `float Progress` - Normalized progress 0-1 (get/set)
- `LoopType LoopType` - The loop behavior
- `int LoopCount` - Number of loops (-1 for infinite)

#### Methods
- `Start(T start, T end, float duration, Func<float, float> ease)` - Starts the tween
- `Update(float deltaTime)` - Advances the tween
- `Stop(StopBehavior behavior)` - Stops the tween
- `Pause()` - Pauses the tween
- `Resume()` - Resumes the tween
- `Restart()` - Restarts from beginning
- `Reverse()` - Reverses direction
- `SetDelay(float delay)` - Sets start delay
- `SetLoop(LoopType type, int count)` - Configures looping
- `SetTimeScale(float scale)` - Sets time scale
- `SetEase(Func<float, float> ease)` - Changes easing function
- `OnStarted(Action callback)` - Registers start callback
- `OnUpdated(Action<T> callback)` - Registers update callback
- `OnLooped(Action callback)` - Registers loop callback
- `OnCompleted(Action callback)` - Registers completion callback

### Enums

#### TweenState
- `Delayed` - Waiting to start (delay period)
- `Running` - Active
- `Paused` - Paused
- `Stopped` - Not running

#### StopBehavior
- `AsIs` - Leave at current value
- `ForceComplete` - Jump to end value

#### LoopType
- `None` - No looping
- `Restart` - Loop from beginning
- `PingPong` - Reverse direction (swap start/end)
- `Yoyo` - Smooth back-and-forth

## 💡 Performance Tips

1. **Reuse Tweens** - Create once, restart as needed
2. **Use TweenManager** - Centralize updates for better performance
3. **Concrete Types** - Use FloatTween, Vector3Tween vs Tween<T> when possible
4. **IsComplete** - Check this instead of State == TweenState.Stopped
5. **Time Scale 0** - Freeze tweens instead of pausing for better performance

## 🆚 Comparison with TinyTween

| Feature | TinyTween | Tweeny |
|---------|-----------|---------|
| Basic Tweening | ✅ | ✅ |
| Easing Functions | 15 | 32+ |
| Callbacks | ❌ | ✅ |
| Delays | ❌ | ✅ |
| Looping | ❌ | ✅ (3 types) |
| Pause/Resume | ✅ | ✅ |
| Time Scale | ❌ | ✅ |
| Progress Control | ❌ | ✅ |
| Restart | ❌ | ✅ |
| Reverse | ❌ | ✅ |
| Sequences | ❌ | ✅ |
| Yoyo Mode | ❌ | ✅ |
| Method Chaining | ❌ | ✅ |
| Static Helpers | ❌ | ✅ |
| IsComplete | ❌ | ✅ |
| Dynamic Easing | ❌ | ✅ |
| Modern C# | ❌ | ✅ |

## 📄 License

MIT License - see source file for full license text.

Based on TinyTween by Nick Gravelyn (2013)  
Enhanced by RedMeansWar (2026)

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Add new easing functions
- Create additional tween types
- Improve performance
- Fix bugs
- Enhance documentation

---

**Happy Tweening! 🎨✨**