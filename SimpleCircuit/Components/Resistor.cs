﻿using SimpleCircuit.Contributors;
using System;
using System.Collections.Generic;

namespace SimpleCircuit.Components
{
    /// <summary>
    /// A resistor.
    /// </summary>
    /// <seealso cref="IComponent" />
    [SimpleKey("R")]
    public class Resistor : IComponent
    {
        private readonly Contributor _x, _y, _sx, _sy, _a;

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <inheritdoc/>
        public IReadOnlyList<IPin> Pins { get; }

        /// <inheritdoc/>
        public IEnumerable<Contributor> Contributors => new Contributor[] { _x, _y, _sx, _sy, _a };

        /// <summary>
        /// Initializes a new instance of the <see cref="Resistor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public Resistor(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _x = new DirectContributor(name + ".X", UnknownTypes.X);
            _y = new DirectContributor(name + ".Y", UnknownTypes.Y);
            _sx = new ConstantContributor(UnknownTypes.ScaleX, 1.0);
            _sy = new DirectContributor(name + ".SY", UnknownTypes.ScaleY);
            _a = new DirectContributor(name + ".A", UnknownTypes.Angle);
            Pins = new[]
            {
                new Pin(this, _x, _y, _sx, _sy, _a, new Vector2(-8, 0), Math.PI, new[] { "p", "+", "pos", "a" }),
                new Pin(this, _x, _y, _sx, _sy, _a, new Vector2(8, 0), 0, new[] { "n", "-", "neg", "b" })
            };
        }

        /// <inheritdoc/>
        public void Render(SvgDrawing drawing)
        {
            var tf = new Transform(_x.Value, _y.Value, _sx.Value, _sy.Value, _a.Value);
            drawing.Poly(tf.Apply(
                new Vector2[]
                {
                    new Vector2(-8, 0),
                    new Vector2(-6, 0),
                    new Vector2(-5, -4),
                    new Vector2(-3, 4),
                    new Vector2(-1, -4),
                    new Vector2(1, 4),
                    new Vector2(3, -4),
                    new Vector2(5, 4),
                    new Vector2(6, 0),
                    new Vector2(8, 0)
                }));

            // Depending on the orientation, let's anchor the text differently
            drawing.Text(Label, tf.Apply(new Vector2(0, -7)), tf.ApplyDirection(new Vector2(0, -1)));
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"Resistor {Name}";
    }
}
