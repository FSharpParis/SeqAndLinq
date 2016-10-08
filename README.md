
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
LINQ's extension methods on `IEnumerable<T>` and F# sequence manipulation
functions, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!

|LINQ methods|F# Seq functions|Explanation|
|---|---|---|
|**Aggregate**<br />Ex: <nobr>`(new[] {1, 2, 3}).Aggregate((x, y) => x + y)`</nobr>|**reduce**<br /><nobr>`(T -> T -> T) -> seq<T> -> T`</nobr><br />Ex: <nobr>`[1; 2; 3] |> Seq.reduce (+)`</nobr>|Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. The output type is the same as the type of the elements of the sequence, and it will fail on an empty sequence.|
|**Aggregate**<br />Ex: <nobr>`(new[] {1, 2, 3}).Aggregate("", (x, y) => x + y)`</nobr>|**fold**<br /><nobr>`(TState -> T -> TState) -> TState -> seq<T> -> TState`</nobr><br />Ex: <nobr>`[1; 2; 3] |> Seq.fold (sprintf "%s%i") ""`</nobr>|Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. Using fold, you can provide an initial value for the accumulator, and the result will be of the type of the accumulator.|
|**All**<br />Ex: <nobr>`(new[] {1, 2, 3}).All(i => i % 2 == 0)`</nobr>|**forall**<br /><nobr>`(T -> bool) -> seq<T> -> bool`</nobr><br />Ex: <nobr>`[1; 2; 3] |> Seq.forall (fun i -> i % 2 = 0)`</nobr>|Evaluates whether a given predicate is true for all the elements of the sequence.|
|**Any**<br />Ex: <nobr>`(new[] {1, 2, 3}).Any(i => i % 2 == 0)`</nobr>|**exists**<br /><nobr>`(T -> bool) -> seq<T> -> bool`</nobr><br />Ex: <nobr>`[1; 2; 3] |> Seq.exists (fun i -> i % 2 = 0)`</nobr>|Evaluates whether there exists an element in the sequence for which the predicate is true.|
|**AsEnumerable**<br />Ex: <nobr>`(new[] {1, 2, 3}).AsEnumerable()`</nobr>|<nobr>`[1; 2; 3] :> seq<_>`</nobr>|`AsEnumerable` is mostly used in C# to hide the actual type of the enumerable implementation (either a concrete type such as `List<T>` or another interface such as `ICollection<T>` or `IQueryable<T>`). It could be replaced by a cast to `IEnumerable<T>`, but that doesn't play well with chaining of extensions methods (fluent style). In F#, we don't really care, as we use piping most of the time. Alternatively, you could also use `Seq.ofList` or `Seq.ofArray`, depending on your input type.|
|**Concat**<br />Ex: <nobr>`(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})`</nobr>|**append**<br /><nobr>`seq<T> -> seq<T> -> seq<T>`</nobr><br />Ex: <nobr>`Seq.append [1; 2; 3] [4; 5; 6]`</nobr>|Wraps the two given enumerations as a single concatenated enumeration.|
