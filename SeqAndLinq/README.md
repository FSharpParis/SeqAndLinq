
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
F# sequence manipulation functions and LINQ's extension methods on
`IEnumerable<T>`, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!

|F# Seq functions|LINQ methods|Explanation|
|---|---|---|
|**append**<br>Ex: `Seq.append [1; 2; 3] [4; 5; 6]`|**Concat**<br>Ex: `(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})`|Wraps the two given enumerations as a single concatenated enumeration.|
