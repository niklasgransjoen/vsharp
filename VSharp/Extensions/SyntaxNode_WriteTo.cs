﻿//------------------------------------------------------------------------------
// VSharp - Viv's C#-esque sandbox.
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VSharp.Syntax
{
    internal static class SyntaxNode_WriteTo
    {
        public static void WriteTo(this SyntaxNode node, TextWriter writer)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            if (writer is null) throw new ArgumentNullException(nameof(writer));

            node.WriteTo(writer, indent: "", isLast: true);
        }

        private static void WriteTo(this SyntaxNode node, TextWriter writer, string indent, bool isLast)
        {
            var marker = isLast ? "└──" : "├──";
            writer.WriteClr(indent, ConsoleColor.DarkGray);
            writer.WriteClr(marker, ConsoleColor.DarkGray);

            writer.WriteSyntaxKind(node.Kind);
            if (node is SyntaxToken token && token.Text is not null)
            {
                writer.WriteClr(" ", ConsoleColor.Cyan);
                writer.WriteClr(token.Text, ConsoleColor.Cyan);
            }

            writer.WriteLine();

            indent += isLast ? "   " : "│  ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                child.WriteTo(writer, indent, child == lastChild);
        }

        private static readonly Regex _kindMatch = new Regex("([A-Za-z]*)(Expression|Statement|Token|Keyword)");

        private static void WriteSyntaxKind(this TextWriter writer, SyntaxKind kind)
        {
            if (writer != Console.Out)
            {
                writer.Write(kind);
                return;
            }

            var kindStr = kind.ToString();
            var match = _kindMatch.Match(kindStr);
            if (!match.Success)
            {
                writer.WriteClr(kindStr, ConsoleColor.White);
                return;
            }

            writer.WriteClr(match.Groups[1], ConsoleColor.White);
            writer.WriteClr(match.Groups[2], ConsoleColor.DarkGray);
        }

        private static void WriteClr(this TextWriter writer, object? value, ConsoleColor color)
        {
            if (writer == Console.Out)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;

                writer.Write(value);

                Console.ForegroundColor = oldColor;
            }
            else
            {
                writer.Write(value);
            }
        }
    }
}