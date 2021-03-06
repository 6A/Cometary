﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cometary.Debugging
{
    /// <summary>
    ///   Indicates that all methods marked with an attribute of the given <see cref="Type"/>
    ///   should lead the debugger to break as soon as they are invoked.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class BreakOnAttribute : ConfigurationDependantAttribute
    {
        /// <summary>
        ///   Gets the type of every attribute for which the debugger should break.
        /// </summary>
        public Type[] AttributeTypes { get; }

        /// <inheritdoc cref="BreakOnAttribute" />
        public BreakOnAttribute(params Type[] attributeTypes)
        {
            if (attributeTypes == null)
                throw new ArgumentNullException(nameof(attributeTypes));

            for (int i = 0; i < attributeTypes.Length; i++)
            {
                if (!typeof(Attribute).IsAssignableFrom(attributeTypes[i]))
                    throw new ArgumentException("Expected attributes.", $"{nameof(attributeTypes)}[{i}]");
            }

            AttributeTypes = attributeTypes;
        }

        /// <inheritdoc />
        public override IEnumerable<CompilationEditor> Initialize()
        {
            return new CompilationEditor[] { new BreakingEditor(AttributeTypes) };
        }
    }
}
