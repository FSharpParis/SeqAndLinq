
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
|**All**<br>Ex: `(new[] {1, 2, 3}).All(i => i % 2 == 0)`|**forall**<br>Ex: `[1; 2; 3] |> Seq.forall (fun i -> i % 2 = 0)`|Evaluates whether a given predicate is true for all the elements of the sequence.|
|**Any**<br>Ex: `(new[] {1, 2, 3}).Any(i => i % 2 == 0)`|**exists**<br>Ex: `[1; 2; 3] |> Seq.exists (fun i -> i % 2 = 0)`|Evaluates whether there exists an element in the sequence for which the predicate is true.|
|**AsEnumerable**<br>Ex: `(new[] {1, 2, 3}).AsEnumerable()`|<br>Ex: `[1; 2; 3] :> seq<_>`|`AsEnumerable` is mostly used in C# to hide the actual type of the enumerable implementation (either a concrete type such as `List<T>` or another interface such as `ICollection<T>` or `IQueryable<T>`). It could be replaced by a cast to `IEnumerable<T>`, but that doesn't play well with chaining of extensions methods (fluent style). In F#, we don't really care, as we use piping most of the time. Alternatively, you could also use `Seq.ofList` or `Seq.ofArray`, depending on your input type.|
|**Concat**<br>Ex: `(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})`|**append**<br>Ex: `Seq.append [1; 2; 3] [4; 5; 6]`|Wraps the two given enumerations as a single concatenated enumeration.|
