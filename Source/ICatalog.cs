﻿namespace SkyTerm
{
    public interface ICatalog : IFeature
    {
        int Count { get; }
        
        Item this[int idx] { get; }
    }
}