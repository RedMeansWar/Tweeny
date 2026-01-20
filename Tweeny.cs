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
        /// <summary> Leaves the tween at its current value when stopped.</summary>
        AsIs,

        /// <summary>Forces the tween to immediately complete and apply the end value.</summary>
        ForceComplete
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
    }

    /// <summary>
    /// Provides a collection of easing functions for use in animations and transitions.
    /// </summary>
    /// <remarks>The static methods in this class can be used to interpolate values smoothly over time,
    /// enabling a variety of animation effects such as acceleration, deceleration, and elastic motion. Easing functions
    /// are commonly used in UI and graphics programming to create more natural and visually appealing
    /// transitions.</remarks>
    public static class Ease
    {
        /// <summary>
        /// Returns the input value unchanged, representing a linear interpolation function.
        /// </summary>
        /// <param name="p">The interpolation parameter. Typically in the range 0 to 1, where 0 represents the start and 1 represents
        /// the end value.</param>
        /// <returns>The same value as the input parameter <paramref name="p"/>.</returns>
        public static float Linear(float p) => p;

        #region Quadractic
        /// <summary>
        /// Calculates the quadratic ease-in value for the specified progress.
        /// </summary>
        /// <remarks>Use this method to create an accelerating (ease-in) effect at the start of an
        /// animation or transition.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1].</param>
        /// <returns>The eased value corresponding to the input progress. The result is always non-negative for inputs in the
        /// range [0, 1].</returns>
        public static float QuadIn(float p) => p * p;

        /// <summary>
        /// Calculates the quadratic easing-out value for the specified normalized progress.
        /// </summary>
        /// <remarks>This method implements a quadratic easing-out function, which starts quickly and
        /// decelerates towards the end. It is commonly used in animation and interpolation scenarios to create smooth
        /// transitions.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range 0 to 1, where 0 represents the start and 1
        /// represents the end.</param>
        /// <returns>The eased value corresponding to the input progress. The result is in the range 0 to 1 for input values
        /// between 0 and 1.</returns>
        public static float QuadOut(float p) => p * (2 - p);

        /// <summary>
        /// Calculates the quadratic ease-in-out interpolation of the specified progress value.
        /// </summary>
        /// <remarks>Quadratic ease-in-out produces a slow start and end, with acceleration in the middle.
        /// This function is commonly used in animations to create smooth transitions.</remarks>
        /// <param name="p">The normalized progress of the animation, where 0 represents the start and 1 represents the end. Must be
        /// between 0 and 1, inclusive.</param>
        /// <returns>The interpolated value after applying quadratic ease-in-out. The result is between 0 and 1 for input values
        /// in the range [0, 1].</returns>
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
        /// <remarks>This easing function starts quickly and decelerates towards the end, creating a
        /// smooth, natural transition effect. It is commonly used in animations to achieve a non-linear
        /// motion.</remarks>
        /// <param name="p">The normalized progress of the animation, typically in the range [0, 1], where 0 represents the start and 1
        /// represents the end.</param>
        /// <returns>The eased value corresponding to the input progress. The result is in the range [0, 1] for input values
        /// between 0 and 1.</returns>
        public static float CubicOut(float p) { float f = p - 1; return f * f * f + 1; }
        #endregion

        #region Sinusoidal
        private const float HalfPi = (float)Math.PI * 0.5f;

        /// <summary>
        /// Calculates an ease-in interpolation using a sine function for the specified progress value.
        /// </summary>
        /// <remarks>This easing function starts the transition slowly and accelerates towards the end.
        /// Input values outside the range [0, 1] will produce results outside the [0, 1] range.</remarks>
        /// <param name="p">The progress of the interpolation, typically in the range [0, 1], where 0 represents the start and 1
        /// represents the end.</param>
        /// <returns>The interpolated value after applying the sine ease-in function. The result is in the range [0, 1] for input
        /// values between 0 and 1.</returns>
        public static float SineIn(float p) => 1f - (float)Math.Cos(p * HalfPi);

        /// <summary>
        /// Calculates the sine-based easing value for the specified normalized progress using an "ease out" curve.
        /// </summary>
        /// <remarks>This easing function starts quickly and slows down towards the end, following a sine
        /// curve. It is commonly used to create smooth deceleration effects in animations.</remarks>
        /// <param name="p">The normalized progress of the animation, where 0 represents the start and 1 represents the end. Must be
        /// between 0 and 1, inclusive.</param>
        /// <returns>A value between 0 and 1 representing the eased progress at the specified point.</returns>
        public static float SineOut(float p) => (float)Math.Sin(p * HalfPi);

        /// <summary>
        /// Calculates a value along a sine-based ease-in-out curve for the specified progress.
        /// </summary>
        /// <remarks>This method is commonly used in animation and interpolation scenarios to create
        /// smooth acceleration and deceleration effects. The returned value starts and ends slowly, with a faster
        /// transition in the middle, following a sine wave pattern.</remarks>
        /// <param name="p">A value between 0 and 1 that represents the normalized progress of the animation or interpolation. Values
        /// outside this range will extrapolate the curve.</param>
        /// <returns>A float value between 0 and 1 representing the eased progress at the specified point along the curve.</returns>
        public static float SineInOut(float p) => 0.5f * (1f - (float)Math.Cos(p * Math.PI));
        #endregion
    }

    /// <summary>
    /// Represents a generic tween that interpolates between two values over time.
    /// </summary>
    /// <typeparam name="T">
    /// The value type being tweened. Must be a value type.
    /// </typeparam>
    /// <remarks>
    /// This class is framework-agnostic and relies on user-supplied interpolation
    /// and easing functions.
    /// </remarks>
    public class Tween<T> : ITween where T : struct
    {
        /// <summary>Interpolation function used to blend between start and end values.</summary>
        internal readonly Func<T, T, float, T> _lerp;

        /// <summary>Easing function applied to the normalized time value.</summary>
        internal Func<float, float> _ease;

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

        /// <summary>
        /// Initializes a new tween using the specified interpolation function.
        /// </summary>
        /// <param name="lerpFunc">
        /// A function that interpolates between two values given a progress parameter.
        /// </param>
        public Tween(Func<T, T, float, T> lerpFunc) => _lerp = lerpFunc;

        /// <summary>
        /// Starts the tween with the specified parameters.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="end">The ending value.</param>
        /// <param name="duration">The duration of the tween in seconds.</param>
        /// <param name="easeFunc">
        /// Optional easing function. If null, linear easing is used.
        /// </param>
        public void Start(T start, T end, float duration, Func<float, float> easeFunc = null)
        {
            if (duration <= 0)
                throw new ArgumentException("Duration must be > 0");

            StartValue = start;
            EndValue = end;
            Duration = duration;
            _ease = easeFunc ?? Ease.Linear;
            CurrentTime = 0;
            State = TweenState.Running;

            UpdateValue();
        }

        /// <summary>
        /// Advances the tween based on the elapsed time.
        /// </summary>
        /// <param name="deltaTime">
        /// Time in seconds since the last update.
        /// </param>
        public void Update(float deltaTime)
        {
            if (State != TweenState.Running)
                return;

            CurrentTime += deltaTime;

            if (CurrentTime >= Duration)
            {
                CurrentTime = Duration;
                State = TweenState.Stopped;
            }

            UpdateValue();
        }

        /// <summary>
        /// Recalculates the current interpolated value.
        /// </summary>
        private void UpdateValue()
            => CurrentValue = _lerp(StartValue, EndValue, _ease(CurrentTime / Duration));

        /// <summary>
        /// Stops the tween according to the specified behavior.
        /// </summary>
        /// <param name="behavior">
        /// Determines whether the tween completes or remains at its current value.
        /// </param>
        public void Stop(StopBehavior behavior)
        {
            State = TweenState.Stopped;

            if (behavior == StopBehavior.ForceComplete)
            {
                CurrentTime = Duration;
                UpdateValue();
            }
        }
    }

    /// <summary>
    /// Tween specialization for floating-point values.
    /// </summary>
    // public class FloatTween : Tween<float> { public FloatTween() : base((s, e, p) => s + (e - s) * p) { } }

    #if UNITY_5_3_OR_NEWER
    /// <summary>
    /// Tween specialization for UnityEngine.Vector2 values.
    /// </summary>
    public class Vector2Tween : Tween<Vector2> { public Vector2Tween() : base(Vector2.Lerp) { } }

    /// <summary>
    /// Tween specialization for UnityEngine.Vector3 values.
    /// </summary>
    public class Vector3Tween : Tween<Vector3> { public Vector3Tween() : base(Vector3.Lerp) { } }

    /// <summary>
    /// Tween specialization for UnityEngine.Color values.
    /// </summary>
    public class ColorTween : Tween<Color> { public ColorTween() : base(Color.Lerp) { } }
    #endif
}
