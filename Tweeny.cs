// Tweeny.cs
//
// Copyright (c) 2026 RedMeansWar
// Based on TinyTween by Nick Gravelyn (2013)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial
// portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Tweeny
{
    /// <summary>
    /// Represents the current execution state of a tween.
    /// </summary>
    public enum TweenState
    {
        /// <summary>The tween is waiting to start (delay period).</summary>
        Delayed,

        /// <summary>The tween is actively progressing over time.</summary>
        Running,

        /// <summary>The tween is temporarily halted but can be resumed.</summary>
        Paused,

        /// <summary>The tween is not running and will no longer update.</summary>
        Stopped
    }

    /// <summary>
    /// Defines how a tween behaves when stopped before completion.
    /// </summary>
    public enum StopBehavior
    {
        /// <summary>Leaves the tween at its current value when stopped.</summary>
        AsIs,

        /// <summary>Forces the tween to immediately complete and apply the end value.</summary>
        ForceComplete
    }

    /// <summary>
    /// Defines how a tween repeats after completion.
    /// </summary>
    public enum LoopType
    {
        /// <summary>Does not repeat.</summary>
        None,

        /// <summary>Restarts from the beginning after each completion.</summary>
        Restart,

        /// <summary>Reverses direction after each completion (ping-pong).</summary>
        PingPong,

        /// <summary>Plays forward then backward smoothly (yoyo).</summary>
        Yoyo
    }

    /// <summary>
    /// Defines the common interface for all tween types.
    /// </summary>
    /// <remarks>
    /// Implementations are expected to be updated manually via <see cref="Update(float)"/>.
    /// </remarks>
    public interface ITween
    {
        /// <summary>Gets the current state of the tween.</summary>
        TweenState State { get; }

        /// <summary>Gets whether the tween has completed.</summary>
        bool IsComplete { get; }

        /// <summary>Gets or sets the time scale multiplier for this tween.</summary>
        float TimeScale { get; set; }

        /// <summary>Gets or sets the normalized progress of the tween (0 to 1).</summary>
        float Progress { get; set; }

        /// <summary>
        /// Advances the tween by the specified time delta.
        /// </summary>
        /// <param name="deltaTime">The elapsed time in seconds since the last update.</param>
        void Update(float deltaTime);

        /// <summary>
        /// Stops the tween using the specified stop behavior.
        /// </summary>
        /// <param name="behavior">Determines whether the tween stops immediately or forces completion.</param>
        void Stop(StopBehavior behavior);

        /// <summary>
        /// Pauses the tween at its current position.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes a paused tween.
        /// </summary>
        void Resume();

        /// <summary>
        /// Restarts the tween from the beginning with the same parameters.
        /// </summary>
        void Restart();

        /// <summary>
        /// Reverses the tween's direction.
        /// </summary>
        void Reverse();
    }

    /// <summary>
    /// Provides a collection of easing functions for use in animations and transitions.
    /// </summary>
    /// <remarks>
    /// The static methods in this class can be used to interpolate values smoothly over time,
    /// enabling a variety of animation effects such as acceleration, deceleration, elastic motion,
    /// and bouncing. Easing functions are commonly used in UI and graphics programming to create
    /// more natural and visually appealing transitions.
    /// </remarks>
    public static class Ease
    {
        private const float Pi = (float)Math.PI;
        private const float HalfPi = Pi * 0.5f;
        private const float TwoPi = Pi * 2f;

        /// <summary>
        /// Returns the input value unchanged, representing a linear interpolation function.
        /// </summary>
        /// <param name="p">The interpolation parameter, typically in the range [0, 1].</param>
        /// <returns>The same value as the input parameter.</returns>
        public static float Linear(float p) => p;

        #region Quadratic
        /// <summary>
        /// Calculates the quadratic ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Use this method to create an accelerating (ease-in) effect at the start of an animation.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value corresponding to the input progress.</returns>
        public static float QuadIn(float p) => p * p;

        /// <summary>
        /// Calculates the quadratic easing-out value for the specified normalized progress.
        /// </summary>
        /// <remarks>This method starts quickly and decelerates towards the end.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value corresponding to the input progress.</returns>
        public static float QuadOut(float p) => p * (2 - p);

        /// <summary>
        /// Calculates the quadratic ease-in-out interpolation of the specified progress value.
        /// </summary>
        /// <remarks>Produces a slow start and end, with acceleration in the middle.</remarks>
        /// <param name="p">The normalized progress of the animation, where 0 represents the start and 1 represents the end.</param>
        /// <returns>The interpolated value after applying quadratic ease-in-out.</returns>
        public static float QuadInOut(float p) => p < 0.5f ? 2 * p * p : -1 + (4 - 2 * p) * p;
        #endregion

        #region Cubic
        /// <summary>
        /// Calculates the cubic-in easing value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value, where the rate of change starts slowly and accelerates toward the end.</returns>
        public static float CubicIn(float p) => p * p * p;

        /// <summary>
        /// Calculates the cubic easing-out value for the specified normalized progress.
        /// </summary>
        /// <remarks>This easing function starts quickly and decelerates towards the end.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value corresponding to the input progress.</returns>
        public static float CubicOut(float p)
        {
            float f = p - 1;
            return f * f * f + 1;
        }

        /// <summary>
        /// Calculates the cubic ease-in-out interpolation of the specified progress value.
        /// </summary>
        /// <remarks>Combines cubic ease-in and ease-out for a smooth acceleration and deceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The interpolated value after applying cubic ease-in-out.</returns>
        public static float CubicInOut(float p)
        {
            if (p < 0.5f)
                return 4 * p * p * p;
            
            float f = (2 * p) - 2;
            return 0.5f * f * f * f + 1;
        }
        #endregion

        #region Quartic
        /// <summary>
        /// Calculates the quartic (x^4) ease-in value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with a strong acceleration curve.</returns>
        public static float QuartIn(float p) => p * p * p * p;

        /// <summary>
        /// Calculates the quartic (x^4) ease-out value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with a strong deceleration curve.</returns>
        public static float QuartOut(float p)
        {
            float f = p - 1;
            return f * f * f * (1 - p) + 1;
        }

        /// <summary>
        /// Calculates the quartic (x^4) ease-in-out value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with strong acceleration and deceleration.</returns>
        public static float QuartInOut(float p)
        {
            if (p < 0.5f)
                return 8 * p * p * p * p;

            float f = p - 1;
            return -8 * f * f * f * f + 1;
        }
        #endregion

        #region Quintic
        /// <summary>
        /// Calculates the quintic (x^5) ease-in value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with very strong acceleration.</returns>
        public static float QuintIn(float p) => p * p * p * p * p;

        /// <summary>
        /// Calculates the quintic (x^5) ease-out value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with very strong deceleration.</returns>
        public static float QuintOut(float p)
        {
            float f = p - 1;
            return f * f * f * f * f + 1;
        }

        /// <summary>
        /// Calculates the quintic (x^5) ease-in-out value for the specified progress.
        /// </summary>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with very strong acceleration and deceleration.</returns>
        public static float QuintInOut(float p)
        {
            if (p < 0.5f)
                return 16 * p * p * p * p * p;

            float f = (2 * p) - 2;
            return 0.5f * f * f * f * f * f + 1;
        }
        #endregion

        #region Sinusoidal
        /// <summary>
        /// Calculates an ease-in interpolation using a sine function for the specified progress value.
        /// </summary>
        /// <remarks>This easing function starts the transition slowly and accelerates towards the end.</remarks>
        /// <param name="p">The progress of the interpolation, typically in the range [0, 1].</param>
        /// <returns>The interpolated value after applying the sine ease-in function.</returns>
        public static float SineIn(float p) => 1f - (float)Math.Cos(p * HalfPi);

        /// <summary>
        /// Calculates the sine-based easing value using an "ease out" curve.
        /// </summary>
        /// <remarks>This easing function starts quickly and slows down towards the end.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>A value representing the eased progress at the specified point.</returns>
        public static float SineOut(float p) => (float)Math.Sin(p * HalfPi);

        /// <summary>
        /// Calculates a value along a sine-based ease-in-out curve for the specified progress.
        /// </summary>
        /// <remarks>The returned value starts and ends slowly, with a faster transition in the middle.</remarks>
        /// <param name="p">A value between 0 and 1 that represents the normalized progress.</param>
        /// <returns>A float value representing the eased progress at the specified point along the curve.</returns>
        public static float SineInOut(float p) => 0.5f * (1f - (float)Math.Cos(p * Pi));
        #endregion

        #region Exponential
        /// <summary>
        /// Calculates an exponential ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Creates a very dramatic acceleration effect, starting extremely slowly.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with exponential acceleration.</returns>
        public static float ExpoIn(float p) => p == 0 ? 0 : (float)Math.Pow(2, 10 * (p - 1));

        /// <summary>
        /// Calculates an exponential ease-out value for the specified progress.
        /// </summary>
        /// <remarks>Creates a very dramatic deceleration effect.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with exponential deceleration.</returns>
        public static float ExpoOut(float p) => p == 1 ? 1 : 1 - (float)Math.Pow(2, -10 * p);

        /// <summary>
        /// Calculates an exponential ease-in-out value for the specified progress.
        /// </summary>
        /// <remarks>Combines exponential ease-in and ease-out for dramatic acceleration and deceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with exponential ease-in-out.</returns>
        public static float ExpoInOut(float p)
        {
            if (p == 0 || p == 1) return p;

            if (p < 0.5f)
                return 0.5f * (float)Math.Pow(2, 20 * p - 10);

            return -0.5f * (float)Math.Pow(2, -20 * p + 10) + 1;
        }
        #endregion

        #region Circular
        /// <summary>
        /// Calculates a circular ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Uses a circular arc to create smooth acceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value following a circular curve.</returns>
        public static float CircIn(float p) => 1 - (float)Math.Sqrt(1 - p * p);

        /// <summary>
        /// Calculates a circular ease-out value for the specified progress.
        /// </summary>
        /// <remarks>Uses a circular arc to create smooth deceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value following a circular curve.</returns>
        public static float CircOut(float p) => (float)Math.Sqrt((2 - p) * p);

        /// <summary>
        /// Calculates a circular ease-in-out value for the specified progress.
        /// </summary>
        /// <remarks>Combines circular ease-in and ease-out for smooth acceleration and deceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value following a circular curve.</returns>
        public static float CircInOut(float p)
        {
            if (p < 0.5f)
                return 0.5f * (1 - (float)Math.Sqrt(1 - 4 * p * p));

            return 0.5f * ((float)Math.Sqrt(-((2 * p) - 3) * ((2 * p) - 1)) + 1);
        }
        #endregion

        #region Elastic
        /// <summary>
        /// Calculates an elastic ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Creates an oscillating effect that resembles a spring being pulled back before release.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with elastic oscillation.</returns>
        public static float ElasticIn(float p)
        {
            if (p == 0 || p == 1) return p;
            return -(float)Math.Pow(2, 10 * (p - 1)) * (float)Math.Sin((p - 1.1f) * 5 * Pi);
        }

        /// <summary>
        /// Calculates an elastic ease-out value for the specified progress.
        /// </summary>
        /// <remarks>Creates an oscillating effect that resembles a spring settling after release.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with elastic oscillation.</returns>
        public static float ElasticOut(float p)
        {
            if (p == 0 || p == 1) return p;
            return (float)Math.Pow(2, -10 * p) * (float)Math.Sin((p - 0.1f) * 5 * Pi) + 1;
        }

        /// <summary>
        /// Calculates an elastic ease-in-out value for the specified progress.
        /// </summary>
        /// <remarks>Combines elastic ease-in and ease-out for oscillating acceleration and deceleration.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with elastic oscillation.</returns>
        public static float ElasticInOut(float p)
        {
            if (p == 0 || p == 1) return p;

            p *= 2;
            if (p < 1)
                return -0.5f * (float)Math.Pow(2, 10 * (p - 1)) * (float)Math.Sin((p - 1.1f) * 5 * Pi);

            return 0.5f * (float)Math.Pow(2, -10 * (p - 1)) * (float)Math.Sin((p - 1.1f) * 5 * Pi) + 1;
        }
        #endregion

        #region Back
        /// <summary>
        /// Calculates a back ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Pulls back slightly before accelerating forward, creating an anticipation effect.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value that overshoots backward before moving forward.</returns>
        public static float BackIn(float p)
        {
            const float s = 1.70158f;
            return p * p * ((s + 1) * p - s);
        }

        /// <summary>
        /// Calculates a back ease-out value for the specified progress.
        /// </summary>
        /// <remarks>Overshoots the target slightly before settling, creating a playful effect.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value that overshoots forward before settling.</returns>
        public static float BackOut(float p)
        {
            const float s = 1.70158f;
            p -= 1;
            return p * p * ((s + 1) * p + s) + 1;
        }

        /// <summary>
        /// Calculates a back ease-in-out value for the specified progress.
        /// </summary>
        /// <remarks>Combines back ease-in and ease-out for overshoot in both directions.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with back overshoot effect.</returns>
        public static float BackInOut(float p)
        {
            const float s = 1.70158f * 1.525f;
            p *= 2;
            
            if (p < 1)
                return 0.5f * (p * p * ((s + 1) * p - s));

            p -= 2;
            return 0.5f * (p * p * ((s + 1) * p + s) + 2);
        }
        #endregion

        #region Bounce
        /// <summary>
        /// Calculates a bounce ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Creates a bouncing effect at the start of the animation.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with a bouncing effect.</returns>
        public static float BounceIn(float p) => 1 - BounceOut(1 - p);

        /// <summary>
        /// Calculates a bounce ease-out value for the specified progress.
        /// </summary>
        /// <remarks>Creates a bouncing effect as the animation settles, like a ball bouncing to a stop.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with a bouncing effect.</returns>
        public static float BounceOut(float p)
        {
            if (p < 1 / 2.75f)
                return 7.5625f * p * p;
            
            if (p < 2 / 2.75f)
            {
                p -= 1.5f / 2.75f;
                return 7.5625f * p * p + 0.75f;
            }
            
            if (p < 2.5f / 2.75f)
            {
                p -= 2.25f / 2.75f;
                return 7.5625f * p * p + 0.9375f;
            }
            
            p -= 2.625f / 2.75f;
            return 7.5625f * p * p + 0.984375f;
        }

        /// <summary>
        /// Calculates a bounce ease-in-out value for the specified progress.
        /// </summary>
        /// <remarks>Combines bounce ease-in and ease-out for bouncing at both start and end.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value with a bouncing effect.</returns>
        public static float BounceInOut(float p)
        {
            if (p < 0.5f)
                return BounceIn(p * 2) * 0.5f;

            return BounceOut(p * 2 - 1) * 0.5f + 0.5f;
        }
        #endregion

        #region Smoothstep
        /// <summary>
        /// Calculates a smooth hermite interpolation value.
        /// </summary>
        /// <remarks>Very smooth ease-in-out, commonly used in graphics.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>A smoothly interpolated value.</returns>
        public static float Smoothstep(float p) => p * p * (3 - 2 * p);

        /// <summary>
        /// Calculates an even smoother hermite interpolation value.
        /// </summary>
        /// <remarks>Ken Perlin's improved smoothstep with better derivatives.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>A very smoothly interpolated value.</returns>
        public static float Smootherstep(float p) => p * p * p * (p * (p * 6 - 15) + 10);
        #endregion
    }

    /// <summary>
    /// Represents a generic tween that interpolates between two values over time.
    /// </summary>
    /// <typeparam name="T">The value type being tweened. Must be a value type.</typeparam>
    /// <remarks>
    /// This class is framework-agnostic and relies on user-supplied interpolation
    /// and easing functions.
    /// </remarks>
    public class Tween<T> : ITween where T : struct
    {
        /// <summary>Interpolation function used to blend between start and end values.</summary>
        private readonly Func<T, T, float, T> _lerp;

        /// <summary>Easing function applied to the normalized time value.</summary>
        private Func<float, float> _ease;

        /// <summary>Initial delay before the tween starts, in seconds.</summary>
        private float _delay;

        /// <summary>Time remaining in the delay period.</summary>
        private float _delayRemaining;

        /// <summary>Time scale multiplier for this tween.</summary>
        private float _timeScale = 1f;

        /// <summary>The value at the start of the tween.</summary>
        public T StartValue { get; private set; }

        /// <summary>The value at the end of the tween.</summary>
        public T EndValue { get; private set; }

        /// <summary>The current interpolated value.</summary>
        public T CurrentValue { get; private set; }

        /// <summary>The total duration of the tween, in seconds.</summary>
        public float Duration { get; private set; }

        /// <summary>The elapsed time since the tween started.</summary>
        public float CurrentTime { get; private set; }

        /// <summary>The current execution state of the tween.</summary>
        public TweenState State { get; private set; } = TweenState.Stopped;

        /// <summary>Gets whether the tween has completed all loops.</summary>
        public bool IsComplete => State == TweenState.Stopped && CurrentTime >= Duration;

        /// <summary>The loop behavior of this tween.</summary>
        public LoopType LoopType { get; private set; } = LoopType.None;

        /// <summary>Number of times to loop. -1 for infinite, 0 for no loop, positive for specific count.</summary>
        public int LoopCount { get; private set; }

        /// <summary>Remaining loops to execute.</summary>
        private int _loopsRemaining;

        /// <summary>Whether the tween is currently playing in reverse (for ping-pong/yoyo).</summary>
        private bool _isReversed;

        /// <summary>
        /// Gets or sets the time scale multiplier for this tween.
        /// Values > 1 speed up the tween, values < 1 slow it down.
        /// </summary>
        public float TimeScale
        {
            get => _timeScale;
            set => _timeScale = Math.Max(0, value);
        }

        /// <summary>
        /// Gets or sets the normalized progress of the tween (0 to 1).
        /// Setting this will jump the tween to that position.
        /// </summary>
        public float Progress
        {
            get => Duration > 0 ? CurrentTime / Duration : 0;
            set
            {
                CurrentTime = Math.Clamp(value, 0f, 1f) * Duration;
                UpdateValue();
            }
        }

        /// <summary>Called when the tween starts (after delay, if any).</summary>
        public event Action OnStart;

        /// <summary>Called every frame while the tween is running.</summary>
        public event Action<T> OnUpdate;

        /// <summary>Called when the tween completes (including all loops).</summary>
        public event Action OnComplete;

        /// <summary>Called when a single loop iteration completes.</summary>
        public event Action OnLoop;

        /// <summary>
        /// Initializes a new tween using the specified interpolation function.
        /// </summary>
        /// <param name="lerpFunc">
        /// A function that interpolates between two values given a progress parameter.
        /// </param>
        public Tween(Func<T, T, float, T> lerpFunc)
        {
            _lerp = lerpFunc ?? throw new ArgumentNullException(nameof(lerpFunc));
            State = TweenState.Stopped;
        }

        /// <summary>
        /// Starts the tween with the specified parameters.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="end">The ending value.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <param name="easeFunc">Optional easing function. If null, linear easing is used.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> Start(T start, T end, float duration, Func<float, float> easeFunc = null)
        {
            if (duration <= 0)
                throw new ArgumentException("Duration must be > 0", nameof(duration));

            StartValue = start;
            EndValue = end;
            Duration = duration;
            _ease = easeFunc ?? Ease.Linear;
            CurrentTime = 0;
            _loopsRemaining = LoopCount;
            _isReversed = false;

            if (_delay > 0)
            {
                _delayRemaining = _delay;
                State = TweenState.Delayed;
            }
            else
            {
                State = TweenState.Running;
                OnStart?.Invoke();
            }

            UpdateValue();
            return this;
        }

        /// <summary>
        /// Sets a delay before the tween starts.
        /// </summary>
        /// <param name="delay">The delay in seconds.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> SetDelay(float delay)
        {
            _delay = delay >= 0 ? delay : 0;
            return this;
        }

        /// <summary>
        /// Sets the loop behavior for this tween.
        /// </summary>
        /// <param name="loopType">The type of looping to use.</param>
        /// <param name="loopCount">Number of times to loop. -1 for infinite, 0 for no loop.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> SetLoop(LoopType loopType, int loopCount = -1)
        {
            LoopType = loopType;
            LoopCount = loopCount;
            return this;
        }

        /// <summary>
        /// Sets the time scale for this tween.
        /// </summary>
        /// <param name="scale">Time scale multiplier (1 = normal, 2 = double speed, 0.5 = half speed).</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> SetTimeScale(float scale)
        {
            TimeScale = scale;
            return this;
        }

        /// <summary>
        /// Changes the easing function of this tween.
        /// </summary>
        /// <param name="easeFunc">The new easing function to use.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> SetEase(Func<float, float> easeFunc)
        {
            _ease = easeFunc ?? Ease.Linear;
            if (State == TweenState.Running)
                UpdateValue();
            return this;
        }

        /// <summary>
        /// Advances the tween based on the elapsed time.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since the last update.</param>
        public void Update(float deltaTime)
        {
            if (State == TweenState.Stopped || State == TweenState.Paused)
                return;

            // Apply time scale
            deltaTime *= _timeScale;

            // Handle delay
            if (State == TweenState.Delayed)
            {
                _delayRemaining -= deltaTime;
                if (_delayRemaining <= 0)
                {
                    State = TweenState.Running;
                    OnStart?.Invoke();
                    
                    // Apply overflow time from delay
                    if (_delayRemaining < 0)
                        deltaTime = -_delayRemaining;
                    else
                        return;
                }
                else
                {
                    return;
                }
            }

            CurrentTime += deltaTime;

            if (CurrentTime >= Duration)
            {
                CurrentTime = Duration;
                UpdateValue();
                OnUpdate?.Invoke(CurrentValue);
                
                HandleCompletion();
            }
            else
            {
                UpdateValue();
                OnUpdate?.Invoke(CurrentValue);
            }
        }

        /// <summary>
        /// Handles tween completion and looping logic.
        /// </summary>
        private void HandleCompletion()
        {
            // Check if we should loop
            if (LoopType != LoopType.None && (LoopCount == -1 || _loopsRemaining > 0))
            {
                if (LoopCount > 0)
                    _loopsRemaining--;

                OnLoop?.Invoke();

                switch (LoopType)
                {
                    case LoopType.Restart:
                        CurrentTime = 0;
                        _isReversed = false;
                        UpdateValue();
                        break;

                    case LoopType.PingPong:
                        CurrentTime = 0;
                        _isReversed = !_isReversed;
                        // Swap start and end values
                        (StartValue, EndValue) = (EndValue, StartValue);
                        UpdateValue();
                        break;

                    case LoopType.Yoyo:
                        CurrentTime = 0;
                        _isReversed = !_isReversed;
                        UpdateValue();
                        break;
                }
            }
            else
            {
                State = TweenState.Stopped;
                OnComplete?.Invoke();
            }
        }

        /// <summary>
        /// Recalculates the current interpolated value.
        /// </summary>
        private void UpdateValue()
        {
            float t = CurrentTime / Duration;
            
            // Apply reverse if needed (for Yoyo)
            if (_isReversed && LoopType == LoopType.Yoyo)
                t = 1f - t;
            
            float easedT = _ease(t);
            CurrentValue = _lerp(StartValue, EndValue, easedT);
        }

        /// <summary>
        /// Stops the tween according to the specified behavior.
        /// </summary>
        /// <param name="behavior">
        /// Determines whether the tween completes or remains at its current value.
        /// </param>
        public void Stop(StopBehavior behavior)
        {
            if (State == TweenState.Stopped)
                return;

            State = TweenState.Stopped;

            if (behavior == StopBehavior.ForceComplete)
            {
                CurrentTime = Duration;
                UpdateValue();
                OnComplete?.Invoke();
            }
        }

        /// <summary>
        /// Pauses the tween at its current position.
        /// </summary>
        public void Pause()
        {
            if (State == TweenState.Running || State == TweenState.Delayed)
                State = TweenState.Paused;
        }

        /// <summary>
        /// Resumes a paused tween.
        /// </summary>
        public void Resume()
        {
            if (State == TweenState.Paused)
            {
                State = _delayRemaining > 0 ? TweenState.Delayed : TweenState.Running;
            }
        }

        /// <summary>
        /// Restarts the tween from the beginning with the same parameters.
        /// </summary>
        public void Restart()
        {
            CurrentTime = 0;
            _loopsRemaining = LoopCount;
            _isReversed = false;
            
            if (_delay > 0)
            {
                _delayRemaining = _delay;
                State = TweenState.Delayed;
            }
            else
            {
                State = TweenState.Running;
            }
            
            UpdateValue();
        }

        /// <summary>
        /// Reverses the tween's direction by swapping start and end values.
        /// </summary>
        public void Reverse()
        {
            (StartValue, EndValue) = (EndValue, StartValue);
            CurrentTime = Duration - CurrentTime;
            UpdateValue();
        }

        /// <summary>
        /// Registers a callback to be invoked when the tween starts.
        /// </summary>
        /// <param name="callback">The callback to invoke.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> OnStarted(Action callback)
        {
            OnStart += callback;
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked on each update.
        /// </summary>
        /// <param name="callback">The callback to invoke with the current value.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> OnUpdated(Action<T> callback)
        {
            OnUpdate += callback;
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked when the tween completes.
        /// </summary>
        /// <param name="callback">The callback to invoke.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> OnCompleted(Action callback)
        {
            OnComplete += callback;
            return this;
        }

        /// <summary>
        /// Registers a callback to be invoked when a loop iteration completes.
        /// </summary>
        /// <param name="callback">The callback to invoke.</param>
        /// <returns>This tween instance for method chaining.</returns>
        public Tween<T> OnLooped(Action callback)
        {
            OnLoop += callback;
            return this;
        }
    }

    /// <summary>
    /// Represents a sequence of tweens that play one after another.
    /// </summary>
    public class TweenSequence : ITween
    {
        private readonly List<ITween> _tweens = new List<ITween>();
        private int _currentIndex = 0;
        private float _timeScale = 1f;

        /// <summary>Gets the current state of the sequence.</summary>
        public TweenState State { get; private set; } = TweenState.Stopped;

        /// <summary>Gets whether the sequence has completed.</summary>
        public bool IsComplete => State == TweenState.Stopped && _currentIndex >= _tweens.Count;

        /// <summary>Gets or sets the time scale for all tweens in the sequence.</summary>
        public float TimeScale
        {
            get => _timeScale;
            set
            {
                _timeScale = Math.Max(0, value);
                foreach (var tween in _tweens)
                    tween.TimeScale = _timeScale;
            }
        }

        /// <summary>Gets or sets the normalized progress of the sequence.</summary>
        public float Progress
        {
            get
            {
                if (_tweens.Count == 0) return 0;
                return (_currentIndex + (_currentIndex < _tweens.Count ? _tweens[_currentIndex].Progress : 1f)) / _tweens.Count;
            }
            set
            {
                // This is complex to implement properly, so we'll keep it simple
                float targetIndex = value * _tweens.Count;
                _currentIndex = Math.Clamp((int)targetIndex, 0, _tweens.Count - 1);
                if (_currentIndex < _tweens.Count)
                {
                    _tweens[_currentIndex].Progress = targetIndex - _currentIndex;
                }
            }
        }

        /// <summary>Called when the sequence completes.</summary>
        public event Action OnComplete;

        /// <summary>
        /// Adds a tween to the end of the sequence.
        /// </summary>
        /// <param name="tween">The tween to add.</param>
        /// <returns>This sequence for method chaining.</returns>
        public TweenSequence Append(ITween tween)
        {
            _tweens.Add(tween);
            tween.TimeScale = _timeScale;
            return this;
        }

        /// <summary>
        /// Starts the sequence.
        /// </summary>
        /// <returns>This sequence for method chaining.</returns>
        public TweenSequence Start()
        {
            if (_tweens.Count == 0)
                throw new InvalidOperationException("Cannot start empty sequence");

            _currentIndex = 0;
            State = TweenState.Running;
            return this;
        }

        /// <summary>
        /// Updates the sequence and its current tween.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last update.</param>
        public void Update(float deltaTime)
        {
            if (State != TweenState.Running || _currentIndex >= _tweens.Count)
                return;

            var currentTween = _tweens[_currentIndex];
            currentTween.Update(deltaTime);

            // Move to next tween if current one is complete
            if (currentTween.IsComplete)
            {
                _currentIndex++;
                
                if (_currentIndex >= _tweens.Count)
                {
                    State = TweenState.Stopped;
                    OnComplete?.Invoke();
                }
            }
        }

        /// <summary>Stops the sequence.</summary>
        public void Stop(StopBehavior behavior)
        {
            State = TweenState.Stopped;
            if (behavior == StopBehavior.ForceComplete)
            {
                // Fast-forward all tweens
                foreach (var tween in _tweens)
                    tween.Stop(StopBehavior.ForceComplete);
                
                _currentIndex = _tweens.Count;
                OnComplete?.Invoke();
            }
        }

        /// <summary>Pauses the sequence.</summary>
        public void Pause()
        {
            if (State == TweenState.Running && _currentIndex < _tweens.Count)
            {
                State = TweenState.Paused;
                _tweens[_currentIndex].Pause();
            }
        }

        /// <summary>Resumes the sequence.</summary>
        public void Resume()
        {
            if (State == TweenState.Paused && _currentIndex < _tweens.Count)
            {
                State = TweenState.Running;
                _tweens[_currentIndex].Resume();
            }
        }

        /// <summary>Restarts the sequence from the beginning.</summary>
        public void Restart()
        {
            _currentIndex = 0;
            foreach (var tween in _tweens)
                tween.Restart();
            State = TweenState.Running;
        }

        /// <summary>Not supported for sequences.</summary>
        public void Reverse()
        {
            throw new NotSupportedException("Reverse is not supported for TweenSequence");
        }

        /// <summary>
        /// Registers a callback for when the sequence completes.
        /// </summary>
        public TweenSequence OnCompleted(Action callback)
        {
            OnComplete += callback;
            return this;
        }
    }

    /// <summary>
    /// Static helper methods for creating tweens with a cleaner API.
    /// </summary>
    public static class TweenExtensions
    {
        /// <summary>
        /// Creates and starts a float tween.
        /// </summary>
        /// <param name="from">Starting value.</param>
        /// <param name="to">Ending value.</param>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="onUpdate">Callback for each update.</param>
        /// <param name="ease">Easing function.</param>
        /// <returns>The created tween.</returns>
        public static FloatTween To(float from, float to, float duration, Action<float> onUpdate, Func<float, float> ease = null)
        {
            var tween = new FloatTween();
            if (onUpdate != null)
                tween.OnUpdated(onUpdate);
            tween.Start(from, to, duration, ease);
            return tween;
        }

        /// <summary>
        /// Creates a tween that tweens FROM the specified value to the current value.
        /// </summary>
        public static FloatTween From(float from, float currentValue, float duration, Action<float> onUpdate, Func<float, float> ease = null)
        {
            return To(from, currentValue, duration, onUpdate, ease);
        }

        #if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Creates and starts a Vector3 tween.
        /// </summary>
        public static Vector3Tween To(Vector3 from, Vector3 to, float duration, Action<Vector3> onUpdate, Func<float, float> ease = null)
        {
            var tween = new Vector3Tween();
            if (onUpdate != null)
                tween.OnUpdated(onUpdate);
            tween.Start(from, to, duration, ease);
            return tween;
        }

        /// <summary>
        /// Creates and starts a Color tween.
        /// </summary>
        public static ColorTween To(Color from, Color to, float duration, Action<Color> onUpdate, Func<float, float> ease = null)
        {
            var tween = new ColorTween();
            if (onUpdate != null)
                tween.OnUpdated(onUpdate);
            tween.Start(from, to, duration, ease);
            return tween;
        }
        #endif

        /// <summary>
        /// Creates a new tween sequence.
        /// </summary>
        public static TweenSequence Sequence()
        {
            return new TweenSequence();
        }
    }

    /// <summary>
    /// Tween specialization for floating-point values.
    /// </summary>
    public class FloatTween : Tween<float>
    {
        /// <summary>
        /// Initializes a new FloatTween instance.
        /// </summary>
        public FloatTween() : base((s, e, p) => s + (e - s) * p) { }
    }

    #if UNITY_5_3_OR_NEWER
    /// <summary>
    /// Tween specialization for UnityEngine.Vector2 values.
    /// </summary>
    public class Vector2Tween : Tween<Vector2>
    {
        /// <summary>
        /// Initializes a new Vector2Tween instance.
        /// </summary>
        public Vector2Tween() : base(Vector2.Lerp) { }
    }

    /// <summary>
    /// Tween specialization for UnityEngine.Vector3 values.
    /// </summary>
    public class Vector3Tween : Tween<Vector3>
    {
        /// <summary>
        /// Initializes a new Vector3Tween instance.
        /// </summary>
        public Vector3Tween() : base(Vector3.Lerp) { }
    }

    /// <summary>
    /// Tween specialization for UnityEngine.Vector4 values.
    /// </summary>
    public class Vector4Tween : Tween<Vector4>
    {
        /// <summary>
        /// Initializes a new Vector4Tween instance.
        /// </summary>
        public Vector4Tween() : base(Vector4.Lerp) { }
    }

    /// <summary>
    /// Tween specialization for UnityEngine.Color values.
    /// </summary>
    public class ColorTween : Tween<Color>
    {
        /// <summary>
        /// Initializes a new ColorTween instance.
        /// </summary>
        public ColorTween() : base(Color.Lerp) { }
    }

    /// <summary>
    /// Tween specialization for UnityEngine.Quaternion values.
    /// </summary>
    public class QuaternionTween : Tween<Quaternion>
    {
        /// <summary>
        /// Initializes a new QuaternionTween instance.
        /// </summary>
        public QuaternionTween() : base(Quaternion.Lerp) { }
    }
    #endif
}