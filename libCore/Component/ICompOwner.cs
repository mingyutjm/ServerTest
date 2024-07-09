namespace LibCore
{

    public interface ICompOwner
    {
        public CompCollection Comps { get; }
    }

    public static class ComponentOwnerEx
    {
        public static TComp AddComp<TComp>(this ICompOwner owner) where TComp : class, IComp, new()
        {
            if (owner == null || owner.Comps == null)
            {
                Log.Error($"AddComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                return null;
            }
            var component = ReferencePool.Get<TComp>();
            if (!owner.Comps.Add(component))
                return null;
            component.SetOwner(owner);
            component.Enable();
            return component;
        }

        public static void RemoveComp<TComp>(this ICompOwner owner) where TComp : class, IComp
        {
            if (owner == null || owner.Comps == null)
            {
                Log.Error($"RemoveComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                return;
            }
            var comp = owner.GetComp<TComp>();
            if (comp != null)
            {
                owner.Comps.Remove(comp);
                comp.OnRemove();
                comp.Dispose();
            }
        }

        public static TComp GetComp<TComp>(this ICompOwner owner) where TComp : class, IComp
        {
            if (owner == null || owner.Comps == null)
            {
                Log.Error($"GetComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                return null;
            }
            return owner.Comps.Get<TComp>();
        }

        public static bool TryGetComp<TComp>(this ICompOwner owner, out TComp retComp) where TComp : class, IComp
        {
            if (owner == null || owner.Comps == null)
            {
                Log.Error($"TryGetComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                retComp = null;
                return false;
            }
            retComp = owner.Comps.Get<TComp>();
            return retComp != null;
        }

        public static bool HasComp<TComp>(this ICompOwner owner) where TComp : class, IComp
        {
            if (owner == null || owner.Comps == null)
            {
                Log.Error($"HasComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                return false;
            }
            return owner.Comps.Contains<TComp>();
        }

        public static TComp GetOrAddComp<TComp>(this ICompOwner owner) where TComp : class, IComp, new()
        {
            if (owner == null)
            {
                Log.Error($"GetOrAddComp<{typeof(TComp).Name}> failed! Owner<{typeof(ICompOwner).Name}> or owner.Comps is null");
                return null;
            }
            var comp = owner.GetComp<TComp>();
            if (comp != null)
                return comp;
            return owner.AddComp<TComp>();
        }
    }

}