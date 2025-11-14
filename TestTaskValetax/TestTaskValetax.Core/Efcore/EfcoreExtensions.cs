
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using System.Text.Json;

namespace TestTaskValetax.Core.Efcore;

public static class EfcoreExtensions
{
    public static PropertyBuilder<Type> JsonVOConverter<Type>(this PropertyBuilder<Type> builder)
    {
        return builder.HasConversion((push) => JsonSerializer.Serialize(push, JsonSerializerOptions.Default), (pull) => JsonSerializer.Deserialize<Type>(pull, JsonSerializerOptions.Default));
    }

    public static PropertyBuilder<IReadOnlyList<Type>> JsonVOCollectionConverter<Type>(this PropertyBuilder<IReadOnlyList<Type>> builder)
    {
        return builder.HasConversion((push) => JsonSerializer.Serialize(push, JsonSerializerOptions.Default), (pull) => JsonSerializer.Deserialize<IReadOnlyList<Type>>(pull, JsonSerializerOptions.Default), new ValueComparer<IReadOnlyList<Type>>((c1, c2) => c1.SequenceEqual(c2), (c) => c.Aggregate(0, (int a, Type v) => HashCode.Combine(a, v.GetHashCode())), (c) => c.ToList()));
    }

    public static IQueryable<TType> NullableWhere<TType, TParam>(this IQueryable<TType> query, TParam value, Expression<Func<TType, bool>> expression)
    {
        if (value != null)
        {
            return query.Where(expression);
        }

        return query;
    }
}
