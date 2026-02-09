// TweenyExamples.cs
//
// Example usage of the Tweeny library demonstrating all features

using System;
using Tweeny;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace TweenyExamples
{
    public static class Examples
    {
        /// <summary>
        /// Basic tween example - simple float animation
        /// </summary>
        public static void BasicTween()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 2f, Ease.QuadInOut);

            // Update in your game loop
            // tween.Update(deltaTime);
            
            Console.WriteLine($"Current value: {tween.CurrentValue}");
        }

        /// <summary>
        /// Tween with callbacks
        /// </summary>
        public static void TweenWithCallbacks()
        {
            var tween = new FloatTween();
            
            tween
                .OnStarted(() => Console.WriteLine("Tween started!"))
                .OnUpdated(value => Console.WriteLine($"Value: {value}"))
                .OnCompleted(() => Console.WriteLine("Tween completed!"))
                .Start(0f, 100f, 2f, Ease.ElasticOut);
        }

        /// <summary>
        /// Tween with delay
        /// </summary>
        public static void DelayedTween()
        {
            var tween = new FloatTween();
            
            tween
                .SetDelay(1.5f) // Wait 1.5 seconds before starting
                .OnStarted(() => Console.WriteLine("Starting after delay"))
                .Start(0f, 100f, 2f, Ease.BackOut);
        }

        /// <summary>
        /// Looping tween - restarts from beginning
        /// </summary>
        public static void LoopingTween()
        {
            var tween = new FloatTween();
            
            tween
                .SetLoop(LoopType.Restart, 3) // Loop 3 times
                .OnLooped(() => Console.WriteLine("Loop completed, restarting"))
                .OnCompleted(() => Console.WriteLine("All loops finished!"))
                .Start(0f, 100f, 1f, Ease.SineInOut);
        }

        /// <summary>
        /// Infinite looping tween
        /// </summary>
        public static void InfiniteLoop()
        {
            var tween = new FloatTween();
            
            tween
                .SetLoop(LoopType.Restart, -1) // Loop forever
                .Start(0f, 360f, 2f, Ease.Linear);
            
            // Manually stop when needed:
            // tween.Stop(StopBehavior.AsIs);
        }

        /// <summary>
        /// Ping-pong tween - reverses direction each loop
        /// </summary>
        public static void PingPongTween()
        {
            var tween = new FloatTween();
            
            tween
                .SetLoop(LoopType.PingPong, 5) // Ping-pong 5 times
                .OnLooped(() => Console.WriteLine("Direction reversed"))
                .Start(0f, 100f, 1f, Ease.QuadInOut);
        }

        /// <summary>
        /// Pause and resume tween
        /// </summary>
        public static void PauseResumeTween()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 3f, Ease.CubicInOut);

            // Later in your code:
            // tween.Pause();
            // ... do something ...
            // tween.Resume();
        }

        /// <summary>
        /// Demonstrates all easing functions
        /// </summary>
        public static void AllEasingFunctions()
        {
            var easings = new (string name, Func<float, float> func)[]
            {
                // Linear
                ("Linear", Ease.Linear),
                
                // Quadratic
                ("QuadIn", Ease.QuadIn),
                ("QuadOut", Ease.QuadOut),
                ("QuadInOut", Ease.QuadInOut),
                
                // Cubic
                ("CubicIn", Ease.CubicIn),
                ("CubicOut", Ease.CubicOut),
                ("CubicInOut", Ease.CubicInOut),
                
                // Quartic
                ("QuartIn", Ease.QuartIn),
                ("QuartOut", Ease.QuartOut),
                ("QuartInOut", Ease.QuartInOut),
                
                // Quintic
                ("QuintIn", Ease.QuintIn),
                ("QuintOut", Ease.QuintOut),
                ("QuintInOut", Ease.QuintInOut),
                
                // Sinusoidal
                ("SineIn", Ease.SineIn),
                ("SineOut", Ease.SineOut),
                ("SineInOut", Ease.SineInOut),
                
                // Exponential
                ("ExpoIn", Ease.ExpoIn),
                ("ExpoOut", Ease.ExpoOut),
                ("ExpoInOut", Ease.ExpoInOut),
                
                // Circular
                ("CircIn", Ease.CircIn),
                ("CircOut", Ease.CircOut),
                ("CircInOut", Ease.CircInOut),
                
                // Elastic
                ("ElasticIn", Ease.ElasticIn),
                ("ElasticOut", Ease.ElasticOut),
                ("ElasticInOut", Ease.ElasticInOut),
                
                // Back
                ("BackIn", Ease.BackIn),
                ("BackOut", Ease.BackOut),
                ("BackInOut", Ease.BackInOut),
                
                // Bounce
                ("BounceIn", Ease.BounceIn),
                ("BounceOut", Ease.BounceOut),
                ("BounceInOut", Ease.BounceInOut)
            };

            foreach (var (name, easeFunc) in easings)
            {
                var tween = new FloatTween();
                tween.Start(0f, 100f, 1f, easeFunc);
                Console.WriteLine($"Created tween with {name} easing");
            }
        }

        /// <summary>
        /// Chain multiple operations for clean API
        /// </summary>
        public static void MethodChaining()
        {
            var tween = new FloatTween()
                .SetDelay(0.5f)
                .SetLoop(LoopType.PingPong, 3)
                .OnStarted(() => Console.WriteLine("Started"))
                .OnUpdated(v => Console.WriteLine($"Value: {v}"))
                .OnLooped(() => Console.WriteLine("Looped"))
                .OnCompleted(() => Console.WriteLine("Done"));
            
            tween.Start(0f, 100f, 2f, Ease.ElasticOut);
        }

        #if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Unity-specific examples
        /// </summary>
        public static class UnityExamples
        {
            /// <summary>
            /// Tween a Unity Vector3 position
            /// </summary>
            public static void TweenPosition(Transform transform)
            {
                var tween = new Vector3Tween();
                
                tween
                    .OnUpdated(pos => transform.position = pos)
                    .Start(
                        transform.position,
                        transform.position + Vector3.up * 5f,
                        2f,
                        Ease.BounceOut
                    );
            }

            /// <summary>
            /// Tween a Unity Color
            /// </summary>
            public static void TweenColor(SpriteRenderer renderer)
            {
                var tween = new ColorTween();
                
                tween
                    .OnUpdated(color => renderer.color = color)
                    .Start(
                        Color.red,
                        Color.blue,
                        1.5f,
                        Ease.SineInOut
                    );
            }

            /// <summary>
            /// Tween rotation with ping-pong
            /// </summary>
            public static void TweenRotation(Transform transform)
            {
                var tween = new QuaternionTween();
                
                tween
                    .SetLoop(LoopType.PingPong, -1) // Infinite ping-pong
                    .OnUpdated(rot => transform.rotation = rot)
                    .Start(
                        Quaternion.identity,
                        Quaternion.Euler(0, 180, 0),
                        2f,
                        Ease.QuadInOut
                    );
            }

            /// <summary>
            /// Tween scale with elastic effect
            /// </summary>
            public static void TweenScale(Transform transform)
            {
                var tween = new Vector3Tween();
                
                tween
                    .SetDelay(0.5f)
                    .OnUpdated(scale => transform.localScale = scale)
                    .OnCompleted(() => Debug.Log("Scale animation complete"))
                    .Start(
                        Vector3.one,
                        Vector3.one * 2f,
                        1f,
                        Ease.ElasticOut
                    );
            }

            /// <summary>
            /// Example MonoBehaviour showing typical usage
            /// </summary>
            public class TweenyExample : MonoBehaviour
            {
                private Vector3Tween positionTween;
                private ColorTween colorTween;
                private SpriteRenderer spriteRenderer;

                void Start()
                {
                    spriteRenderer = GetComponent<SpriteRenderer>();
                    
                    // Create and start position tween
                    positionTween = new Vector3Tween();
                    positionTween
                        .SetLoop(LoopType.PingPong, -1)
                        .OnUpdated(pos => transform.position = pos)
                        .Start(
                            transform.position,
                            transform.position + Vector3.right * 5f,
                            2f,
                            Ease.SineInOut
                        );

                    // Create and start color tween
                    colorTween = new ColorTween();
                    colorTween
                        .SetLoop(LoopType.Restart, -1)
                        .OnUpdated(col => spriteRenderer.color = col)
                        .Start(
                            Color.red,
                            Color.blue,
                            3f,
                            Ease.Linear
                        );
                }

                void Update()
                {
                    // Update all tweens
                    positionTween?.Update(Time.deltaTime);
                    colorTween?.Update(Time.deltaTime);
                }

                void OnDisable()
                {
                    // Clean stop when disabled
                    positionTween?.Stop(StopBehavior.AsIs);
                    colorTween?.Stop(StopBehavior.AsIs);
                }
            }
        }
        #endif

        /// <summary>
        /// Custom tween type example - you can create your own!
        /// </summary>
        public class CustomStructTween
        {
            public struct CustomStruct
            {
                public float X;
                public float Y;
                public int Count;
            }

            public static void Example()
            {
                // Define how to interpolate your custom type
                var tween = new Tween<CustomStruct>((start, end, t) => new CustomStruct
                {
                    X = start.X + (end.X - start.X) * t,
                    Y = start.Y + (end.Y - start.Y) * t,
                    Count = (int)(start.Count + (end.Count - start.Count) * t)
                });

                tween
                    .OnUpdated(val => Console.WriteLine($"X: {val.X}, Y: {val.Y}, Count: {val.Count}"))
                    .Start(
                        new CustomStruct { X = 0, Y = 0, Count = 0 },
                        new CustomStruct { X = 100, Y = 200, Count = 50 },
                        2f,
                        Ease.QuadInOut
                    );
            }
        }

        /// <summary>
        /// Time scale example - speed up or slow down tweens
        /// </summary>
        public static void TimeScaleExample()
        {
            var tween = new FloatTween();
            
            tween
                .SetTimeScale(2f) // Run at double speed
                .OnUpdated(v => Console.WriteLine($"Value: {v}"))
                .Start(0f, 100f, 2f, Ease.Linear);

            // Later in your code, you can change the speed dynamically:
            // tween.TimeScale = 0.5f; // Slow down to half speed
            // tween.TimeScale = 0f;   // Freeze the tween
        }

        /// <summary>
        /// Progress property example - jump to specific points
        /// </summary>
        public static void ProgressExample()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 2f, Ease.QuadInOut);

            // Jump to 50% completion
            tween.Progress = 0.5f;
            Console.WriteLine($"Value at 50%: {tween.CurrentValue}");

            // Jump to 75% completion
            tween.Progress = 0.75f;
            Console.WriteLine($"Value at 75%: {tween.CurrentValue}");
        }

        /// <summary>
        /// Restart example - replay tween without recreating
        /// </summary>
        public static void RestartExample()
        {
            var tween = new FloatTween();
            tween
                .OnCompleted(() => Console.WriteLine("Completed!"))
                .Start(0f, 100f, 2f, Ease.BounceOut);

            // Later, restart the tween
            // tween.Restart();
        }

        /// <summary>
        /// Reverse example - change direction mid-tween
        /// </summary>
        public static void ReverseExample()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 3f, Ease.Linear);

            // Update for a bit...
            tween.Update(1.5f); // Halfway through

            // Now reverse direction
            tween.Reverse();

            // The tween will now go from its current position back to 0
        }

        /// <summary>
        /// Yoyo loop - smooth back and forth motion
        /// </summary>
        public static void YoyoLoopExample()
        {
            var tween = new FloatTween();
            
            tween
                .SetLoop(LoopType.Yoyo, 5) // Yoyo 5 times
                .OnLooped(() => Console.WriteLine("Changed direction"))
                .Start(0f, 100f, 1f, Ease.SineInOut);
            
            // Yoyo plays forward, then backward, using the same easing curve
            // This gives smoother transitions than PingPong
        }

        /// <summary>
        /// Change easing mid-tween
        /// </summary>
        public static void ChangeEasingExample()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 3f, Ease.Linear);

            // Update for a bit
            tween.Update(1f);

            // Change to a different easing
            tween.SetEase(Ease.BounceOut);

            // The tween will continue with the new easing function
        }

        /// <summary>
        /// Tween sequence - play multiple tweens in order
        /// </summary>
        public static void SequenceExample()
        {
            var tween1 = new FloatTween();
            tween1.Start(0f, 50f, 1f, Ease.QuadIn);

            var tween2 = new FloatTween();
            tween2.Start(50f, 100f, 1f, Ease.QuadOut);

            var tween3 = new FloatTween();
            tween3.Start(100f, 0f, 1f, Ease.BounceOut);

            var sequence = TweenExtensions.Sequence()
                .Append(tween1)
                .Append(tween2)
                .Append(tween3)
                .OnCompleted(() => Console.WriteLine("Sequence complete!"))
                .Start();

            // Update the sequence in your game loop
            // sequence.Update(deltaTime);
        }

        /// <summary>
        /// Static helper methods for cleaner syntax
        /// </summary>
        public static void HelperMethodsExample()
        {
            // Clean, one-liner tween creation
            var tween = TweenExtensions.To(
                0f, 
                100f, 
                2f, 
                value => Console.WriteLine($"Value: {value}"),
                Ease.ElasticOut
            );

            // From syntax - tween FROM a value to current
            var fromTween = TweenExtensions.From(
                100f,
                0f,
                2f,
                value => Console.WriteLine($"Value: {value}"),
                Ease.BackOut
            );
        }

        /// <summary>
        /// IsComplete property example
        /// </summary>
        public static void IsCompleteExample()
        {
            var tween = new FloatTween();
            tween.Start(0f, 100f, 2f, Ease.Linear);

            // Check if complete
            if (tween.IsComplete)
            {
                Console.WriteLine("Tween finished!");
            }

            // Useful for cleanup or triggering other actions
            while (!tween.IsComplete)
            {
                tween.Update(0.016f);
            }
        }

        /// <summary>
        /// Path tweening example - move through multiple points
        /// </summary>
        public static void PathTweeningExample()
        {
            // Create a path through multiple float values
            var path = TweenExtensions.Path(
                new float[] { 0f, 50f, 25f, 100f, 0f },
                5f,
                value => Console.WriteLine($"Path value: {value}"),
                Ease.Linear
            );

            // Or build it manually
            var manualPath = new PathTween<float>((s, e, p) => s + (e - s) * p);
            manualPath
                .AddPoint(0f)
                .AddPoint(100f)
                .AddPoint(50f)
                .AddPoint(75f)
                .OnUpdated(v => Console.WriteLine($"Value: {v}"))
                .OnCompleted(() => Console.WriteLine("Path complete!"))
                .Start(3f, Ease.SineInOut);
        }

        #if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Unity path tweening examples
        /// </summary>
        public static class UnityPathExamples
        {
            /// <summary>
            /// Move object along a path of waypoints
            /// </summary>
            public static void MoveAlongPath(Transform transform)
            {
                Vector3[] waypoints = new Vector3[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(5, 2, 0),
                    new Vector3(10, 0, 0),
                    new Vector3(10, 0, 5),
                    new Vector3(0, 0, 5)
                };

                TweenExtensions.Path(
                    waypoints,
                    5f,
                    pos => transform.position = pos,
                    Ease.Linear  // Linear gives constant speed along path
                );
            }

            /// <summary>
            /// Create a circular/curved path manually
            /// </summary>
            public static void CircularPath(Transform transform)
            {
                var path = new PathTween<Vector3>(Vector3.Lerp);
                
                // Create points along a circle
                int pointCount = 16;
                float radius = 5f;
                for (int i = 0; i < pointCount; i++)
                {
                    float angle = (i / (float)pointCount) * Mathf.PI * 2f;
                    Vector3 point = new Vector3(
                        Mathf.Cos(angle) * radius,
                        0,
                        Mathf.Sin(angle) * radius
                    );
                    path.AddPoint(point);
                }
                // Close the loop
                path.AddPoint(new Vector3(radius, 0, 0));

                path
                    .OnUpdated(pos => transform.position = pos)
                    .Start(4f, Ease.Linear);
            }

            /// <summary>
            /// Zigzag path example
            /// </summary>
            public static void ZigzagPath(Transform transform)
            {
                var zigzag = new PathTween<Vector3>(Vector3.Lerp);
                
                for (int i = 0; i < 5; i++)
                {
                    zigzag.AddPoint(new Vector3(i * 2f, i % 2 == 0 ? 0 : 2f, 0));
                }

                zigzag
                    .OnUpdated(pos => transform.position = pos)
                    .Start(3f, Ease.Linear);
            }

            /// <summary>
            /// Color gradient path
            /// </summary>
            public static void ColorGradientPath(SpriteRenderer renderer)
            {
                var colorPath = new PathTween<Color>(Color.Lerp);
                colorPath
                    .AddPoint(Color.red)
                    .AddPoint(Color.yellow)
                    .AddPoint(Color.green)
                    .AddPoint(Color.cyan)
                    .AddPoint(Color.blue)
                    .AddPoint(Color.magenta)
                    .AddPoint(Color.red)  // Loop back
                    .OnUpdated(color => renderer.color = color)
                    .Start(6f, Ease.Linear);
            }

            /// <summary>
            /// Camera path following
            /// </summary>
            public static void CameraPath(Camera camera)
            {
                Vector3[] cameraPath = new Vector3[]
                {
                    new Vector3(0, 5, -10),
                    new Vector3(5, 8, -8),
                    new Vector3(10, 6, -10),
                    new Vector3(10, 10, -5),
                    new Vector3(0, 5, -10)
                };

                var lookAtPath = new PathTween<Vector3>(Vector3.Lerp);
                lookAtPath
                    .AddPoint(Vector3.zero)
                    .AddPoint(new Vector3(5, 0, 0))
                    .AddPoint(new Vector3(10, 0, 0));

                var positionPath = TweenExtensions.Path(
                    cameraPath,
                    10f,
                    pos => camera.transform.position = pos,
                    Ease.SineInOut
                );

                lookAtPath
                    .OnUpdated(lookAt => camera.transform.LookAt(lookAt))
                    .Start(10f, Ease.SineInOut);
            }
        }
        #endif

        /// <summary>
        /// Smoothstep easing examples
        /// </summary>
        public static void SmoothstepExample()
        {
            // Regular smoothstep - very smooth ease in/out
            var tween1 = new FloatTween();
            tween1.Start(0f, 100f, 2f, Ease.Smoothstep);

            // Smootherstep - even smoother!
            var tween2 = new FloatTween();
            tween2.Start(0f, 100f, 2f, Ease.Smootherstep);
        }

        /// <summary>
        /// Combining features - the ultimate tween
        /// </summary>
        public static void CombinedFeaturesExample()
        {
            var tween = new FloatTween()
                .SetDelay(0.5f)                    // Wait half a second
                .SetLoop(LoopType.Yoyo, 3)         // Yoyo 3 times
                .SetTimeScale(1.5f)                // Play at 1.5x speed
                .OnStarted(() => Console.WriteLine("Starting!"))
                .OnUpdated(v => Console.WriteLine($"Value: {v}"))
                .OnLooped(() => Console.WriteLine("Direction changed"))
                .OnCompleted(() => Console.WriteLine("All done!"));
            
            tween.Start(0f, 100f, 2f, Ease.ElasticOut);

            // You can still control it after starting
            // tween.Pause();
            // tween.TimeScale = 0.5f;
            // tween.Progress = 0.5f;
            // tween.Reverse();
            // tween.Restart();
        }

        #if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Unity-specific helper method examples
        /// </summary>
        public static class UnityHelperExamples
        {
            public static void HelperMethodExample(Transform transform)
            {
                // One-liner position tween
                TweenExtensions.To(
                    transform.position,
                    transform.position + Vector3.up * 5f,
                    2f,
                    pos => transform.position = pos,
                    Ease.BounceOut
                );

                // One-liner color tween
                var renderer = transform.GetComponent<SpriteRenderer>();
                TweenExtensions.To(
                    Color.red,
                    Color.blue,
                    1.5f,
                    color => renderer.color = color,
                    Ease.SineInOut
                );
            }

            /// <summary>
            /// Complex sequence example
            /// </summary>
            public static void ComplexSequence(Transform transform)
            {
                var moveUp = new Vector3Tween();
                moveUp
                    .OnUpdated(pos => transform.position = pos)
                    .Start(transform.position, transform.position + Vector3.up * 3f, 1f, Ease.QuadOut);

                var moveRight = new Vector3Tween();
                moveRight
                    .OnUpdated(pos => transform.position = pos)
                    .Start(transform.position + Vector3.up * 3f, transform.position + Vector3.up * 3f + Vector3.right * 3f, 1f, Ease.Linear);

                var moveDown = new Vector3Tween();
                moveDown
                    .OnUpdated(pos => transform.position = pos)
                    .Start(transform.position + Vector3.up * 3f + Vector3.right * 3f, transform.position + Vector3.right * 3f, 1f, Ease.BounceOut);

                var sequence = TweenExtensions.Sequence()
                    .Append(moveUp)
                    .Append(moveRight)
                    .Append(moveDown)
                    .OnCompleted(() => Debug.Log("Path complete!"))
                    .Start();
            }

            /// <summary>
            /// Time scale for slow-motion effects
            /// </summary>
            public static void SlowMotionEffect(Transform transform)
            {
                var tween = new Vector3Tween();
                tween
                    .SetTimeScale(0.25f) // Slow motion!
                    .OnUpdated(pos => transform.position = pos)
                    .Start(
                        transform.position,
                        transform.position + Vector3.forward * 10f,
                        2f,
                        Ease.Linear
                    );

                // You can change time scale dynamically for bullet-time effects
                // tween.TimeScale = Time.timeScale; // Match Unity's time scale
            }
        }
        #endif

        /// <summary>
        /// Advanced example: Tween manager for multiple tweens
        /// </summary>
        public class TweenManager
        {
            private readonly System.Collections.Generic.List<ITween> activeTweens = new();

            public void Add(ITween tween)
            {
                activeTweens.Add(tween);
            }

            public void UpdateAll(float deltaTime)
            {
                // Update all tweens
                for (int i = activeTweens.Count - 1; i >= 0; i--)
                {
                    var tween = activeTweens[i];
                    tween.Update(deltaTime);

                    // Remove completed tweens
                    if (tween.IsComplete)
                    {
                        activeTweens.RemoveAt(i);
                    }
                }
            }

            public void StopAll(StopBehavior behavior = StopBehavior.AsIs)
            {
                foreach (var tween in activeTweens)
                {
                    tween.Stop(behavior);
                }
                activeTweens.Clear();
            }

            public void PauseAll()
            {
                foreach (var tween in activeTweens)
                {
                    tween.Pause();
                }
            }

            public void ResumeAll()
            {
                foreach (var tween in activeTweens)
                {
                    tween.Resume();
                }
            }

            public void SetGlobalTimeScale(float scale)
            {
                foreach (var tween in activeTweens)
                {
                    tween.TimeScale = scale;
                }
            }
        }
    }
}