﻿//------------------------------------------------------------------------------
// PHP Sharp. Because PHP isn't good enough.
// Copyright (C) 2019  Niklas Gransjøen
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using PHPSharp.Symbols;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PHPSharp.Binding
{
    internal sealed class BoundCallExpression : BoundExpression
    {
        public BoundCallExpression(MethodSymbol method, ImmutableArray<BoundExpression> arguments)
        {
            Method = method;
            Arguments = arguments;
        }

        #region Properties

        public override BoundNodeKind Kind => BoundNodeKind.CallExpression;
        public override TypeSymbol Type => Method.ReturnType;
        public MethodSymbol Method { get; }
        public ImmutableArray<BoundExpression> Arguments { get; }

        #endregion Properties

        public override IEnumerable<BoundNode> GetChildren()
        {
            return Arguments;
        }
    }
}