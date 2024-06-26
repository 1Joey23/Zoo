﻿using System;
using Utilities;

namespace CagedItems
{
    /// <summary>
    /// The interface that defines a contract for all cage occupants.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// Gets the proportion at which to display the cage occupant.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// Gets the resource key of the cage occupant.
        /// </summary>
        string ResourceKey { get; }

        /// <summary>
        /// Gets the horizontal position of the cage occupant.
        /// </summary>
        int XPosition { get; }

        /// <summary>
        /// Gets the vertical position of the cage occupant.
        /// </summary>
        int YPosition { get; }

        /// <summary>
        /// Gets the horizontal direction of the cage occupant.
        /// </summary>
        HorizontalDirection XDirection { get; }

        /// <summary>
        /// Gets the vertical direction of the cage occupant.
        /// </summary>
        VerticalDirection YDirection { get; }

        HungerState HungerState { get; }

        Action<ICageable> OnImageUpdate { get; set; }

        bool IsActive { get; }
    }
}