﻿//------------------------------------------------------------------------------
// VSharp - Viv's C#-esque sandbox.
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

using System.Collections.Generic;
using System.Collections.Immutable;
using VSharp.Symbols;

namespace VSharp.Binding
{
    internal sealed class BoundMethodDeclarationStatement : BoundStatement
    {
        public BoundMethodDeclarationStatement(MethodSymbol method, ImmutableArray<VariableSymbol> parameters, BoundBlockStatement declaration)
        {
            Method = method;
            Parameters = parameters;
            Declaration = declaration;
        }

        public override BoundNodeKind Kind => BoundNodeKind.MethodDeclarationStatement;
        public MethodSymbol Method { get; }
        public ImmutableArray<VariableSymbol> Parameters { get; }
        public BoundBlockStatement Declaration { get; }

        public override IEnumerable<BoundNode> GetChildren()
        {
            yield return Declaration;
        }
    }
}