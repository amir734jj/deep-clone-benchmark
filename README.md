# deep-clone-benchmark
Deep clone benchmark in .NET

### Result:

```
BenchmarkBinaryDeepCloneSingle: 6 seconds     // Binary deep cloning each indivisual element in the list
BenchmarkBinaryDeepCloneMany: 6 seconds       // Binary deep cloning the whole list
BenchmarkClone: 0 seconds                     // Using .clone() method
```

### Conclusion:
Implementing a `.clone()` is much much faster
