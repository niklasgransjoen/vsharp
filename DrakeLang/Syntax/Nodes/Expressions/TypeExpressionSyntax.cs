﻿//------------------------------------------------------------------------------
// DrakeLang - Viv's C#-esque sandbox.
// Copyright (C) 2019  Vivian Vea
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

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DrakeLang.Syntax
{
    public sealed class TypeExpressionSyntax : ExpressionSyntax
    {
        internal TypeExpressionSyntax(SyntaxToken typeIdentifier)
        {
            TypeIdentifiers = ImmutableArray.Create(typeIdentifier);
        }

        internal TypeExpressionSyntax(ImmutableArray<SyntaxToken> typeIdentifiers)
        {
            if (typeIdentifiers.Length == 0)
                throw new ArgumentException("A type expression has to consist of at least one token.", nameof(typeIdentifiers));

            TypeIdentifiers = typeIdentifiers;
        }

        public override SyntaxKind Kind => SyntaxKind.TypeExpression;
        public ImmutableArray<SyntaxToken> TypeIdentifiers { get; }
        public bool IsArray => TypeIdentifiers.Length > 1;

        public int GetArraySize() => TypeIdentifiers.Length / 2;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            foreach (var item in TypeIdentifiers)
                yield return item;
        }
    }
}