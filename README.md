
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
LINQ's extension methods on `IEnumerable<T>` and F# sequence manipulation
functions, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!

|LINQ methods|F# Seq functions|Explanation|
|---|---|---|
|**Aggregate**<br>Ex: `(new[] {1, 2, 3}).Aggregate((x, y) => x + y)`|**reduce**<br>Ex: `[1; 2; 3] |> Seq.reduce (+)`|Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. The output type is the same as the type of the elements of the sequence, and it will fail on an empty sequence.|
|**Aggregate**<br>Ex: `(new[] {1, 2, 3}).Aggregate("", (x, y) => x + y)`|**fold**<br>Ex: `[1; 2; 3] |> Seq.fold (sprintf "%s%i") ""`|Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. Using fold, you can provide an initial value for the accumulator, and the result will be of the type of the accumulator.|
|**Concat**<br>Ex: `(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})`|**append**<br>Ex: `Seq.append [1; 2; 3] [4; 5; 6]`|Wraps the two given enumerations as a single concatenated enumeration.|
