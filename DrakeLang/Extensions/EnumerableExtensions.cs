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

using System.Collections.Generic;

namespace System.Linq
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (var item in source)
            {
                action(item, index++);
            }
        }

        public static IEnumerable<IndexedIEnumerable<T>> WithIndex<T>(this IEnumerable<T> source)
        {
            int index = 0;
            foreach (var item in source)
            {
                yield return new(item, index++);
            }
        }

        public static IEnumerable<IndexedIEnumerable<TResult>> OfType<TResult>(this IEnumerable<IIndexedIEnumerable> source)
        {
            return source
                .Where(pair => pair.Item is TResult)
                .Select(pair => new IndexedIEnumerable<TResult>((TResult)pair.Item!, pair.Index));
        }

        public interface IIndexedIEnumerable
        {
            object? Item { get; }
            int Index { get; }
        }

        public record IndexedIEnumerable<T>(T Item, int Index) : IIndexedIEnumerable
        {
            object? IIndexedIEnumerable.Item => Item;
        }
    }
}