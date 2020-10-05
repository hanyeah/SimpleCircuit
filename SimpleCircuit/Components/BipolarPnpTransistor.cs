﻿using SimpleCircuit.Functions;

namespace SimpleCircuit.Components
{
    /// <summary>
    /// A PNP transistor.
    /// </summary>
    /// <seealso cref="TransformingComponent" />
    /// <seealso cref="ILabeled" />
    [SimpleKey("Qp"), SimpleKey("Pnp")]
    public class BipolarPnpTransistor : TransformingComponent, ILabeled
    {
        /// <inheritdoc />
        public string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BipolarPnpTransistor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public BipolarPnpTransistor(string name)
            : base(name)
        {
            Pins.Add(new[] { "c", "collector" }, "The collector.", new Vector2(-8, 0), new Vector2(-1, 0));
            Pins.Add(new[] { "b", "base" }, "The base.", new Vector2(0, 6), new Vector2(0, 1));
            Pins.Add(new[] { "e", "emitter" }, "The emitter.", new Vector2(8, 0), new Vector2(1, 0));
        }

        /// <inheritdoc />
        public override void Render(SvgDrawing drawing)
        {
            var normal = new Vector2(NormalX.Value, NormalY.Value);
            var tf = new Transform(X.Value, Y.Value, normal, normal.Perpendicular * Scale.Value);
            drawing.Segments(tf.Apply(new[]
            {
                new Vector2(0, 6), new Vector2(0, 4),
                new Vector2(-6, 4), new Vector2(6, 4)
            }));
            drawing.Polyline(tf.Apply(new[]
            {
                new Vector2(-3, 4), new Vector2(-6, 0), new Vector2(-8, 0)
            }));
            drawing.Polyline(tf.Apply(new[]
            {
                new Vector2(3, 4), new Vector2(6, 0), new Vector2(8, 0)
            }));
            drawing.Polygon(tf.Apply(new[]
            {
                new Vector2(-3, 4), new Vector2(-3.7, 1.4), new Vector2(-5.3, 2.6)
            }));
        }

        /// <inheritdoc />
        public override void Apply(Minimizer minimizer)
        {
            minimizer.Minimize += new Squared(X) + new Squared(Y);
            minimizer.AddConstraint(new Squared(Scale) - 1);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"NPN {Name}";
    }
}
