﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleCircuit.Components
{
    /// <summary>
    /// A subcircuit.
    /// </summary>
    /// <seealso cref="TransformingComponent" />
    public class Subcircuit : TransformingComponent
    {
        private readonly Circuit _ckt;

        /// <summary>
        /// Initializes a new instance of the <see cref="Subcircuit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Subcircuit(string name, Circuit definition, IEnumerable<IPin> pins)
            : base(name)
        {
            _ckt = definition ?? throw new ArgumentNullException(nameof(definition));
            if (!_ckt.Solved)
                _ckt.Solve();

            // Find the pins in the subcircuit
            foreach (var pin in pins)
            {
                if (pin is RotatingPin rpin)
                    Pins.Add(new[] { $"{pin.Owner.Name}_{pin.Name}" }, pin.Description, new Vector2(rpin.X.Value, rpin.Y.Value), new Vector2(rpin.NormalX.Value, rpin.NormalY.Value));
                else if (pin is TranslatingPin tpin)
                    Pins.Add(new[] { $"{pin.Owner.Name}_{pin.Name}" }, pin.Description, new Vector2(tpin.X.Value, tpin.Y.Value));                
            }
        }

        /// <inheritdoc/>
        protected override void Draw(SvgDrawing drawing)
        {
            _ckt.Render(drawing);
        }
    }
}
