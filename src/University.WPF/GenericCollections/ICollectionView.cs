using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace University.WPF.GenericCollections;

public interface ICollectionView<T> : IEnumerable<T>, ICollectionView
{
    public new Predicate<T>? Filter { get; set; }
}