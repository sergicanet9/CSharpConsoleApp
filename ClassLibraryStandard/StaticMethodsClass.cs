
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public static class StaticMethodsClass //Generic methods
{
    private static IContext Context;

    public static void SetContext(IContext context)
    {
        Context = context;
    }

    public static void Add<T>(T n) where T : class
    {
        Context.Set<T>().Add(n);
        Context.SaveChanges();
    }

    public static IEnumerable<T> Get<T>(Expression<Func<T, Boolean>> includeProperties) where T : class
    {
        var s1 = Context.Set<T>().Where(includeProperties);
        return s1;
    }

    public static T GetFirst<T>(Expression<Func<T, Boolean>> includeProperties) where T : class
    {
        var s2 = Context.Set<T>().FirstOrDefault(includeProperties);
        return s2;
    }

    public static void Update<T>(T n) where T : class
    {
        Context.Entry<T>(n).State = EntityState.Modified;
        Context.SaveChanges();
    }

    public static void Delete<T>(T n) where T : class
    {
        Context.Set<T>().Remove(n);
        Context.SaveChanges();
    }
}