﻿using SimpleCircuit.Functions;
using System;

namespace SimpleCircuit.Components
{
    /// <summary>
    /// A component that can rotate.
    /// </summary>
    /// <seealso cref="TranslatingComponent" />
    /// <seealso cref="IRotating" />
    public abstract class RotatingComponent : TranslatingComponent, IRotating
    {
        /// <summary>
        /// Gets the unknown normal x.
        /// </summary>
        /// <value>
        /// The unknown normal x.
        /// </value>
        protected Unknown UnknownNormalX { get; }

        /// <summary>
        /// Gets the unknown normal y.
        /// </summary>
        /// <value>
        /// The unknown normal y.
        /// </value>
        protected Unknown UnknownNormalY { get; }

        /// <inheritdoc/>
        public Function NormalX => UnknownNormalX;

        /// <inheritdoc/>
        public Function NormalY => UnknownNormalY;

        /// <summary>
        /// Sets the angle.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        public double Angle
        {
            set
            {
                var ang = value / 180.0 * Math.PI;
                UnknownNormalX.IsFixed = true;
                UnknownNormalX.Value = Math.Cos(ang);
                UnknownNormalY.IsFixed = true;
                UnknownNormalY.Value = Math.Sin(ang);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatingComponent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected RotatingComponent(string name)
            : base(name)
        {
            UnknownNormalX = new Unknown($"{Name}.nx", UnknownTypes.NormalX);
            UnknownNormalY = new Unknown($"{Name}.ny", UnknownTypes.NormalY);
        }

        /// <inheritdoc/>
        public override void Render(SvgDrawing drawing)
        {
            var normal = new Vector2(NormalX.Value, NormalY.Value);
            drawing.TF = new Transform(X.Value, Y.Value, normal, normal.Perpendicular);
            Draw(drawing);
        }

        public override void Apply(Minimizer minimizer)
        {
            base.Apply(minimizer);
            minimizer.Minimize += new Squared(NormalX - 1) / 1e4;
            minimizer.AddConstraint(new Squared(NormalX) + new Squared(NormalY) - 1);
        }
    }
}
