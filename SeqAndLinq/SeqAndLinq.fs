module SeqAndLinq

type NameAndSample = {
    Name: string
    Sample: string
}

type Mapping = {
    LinqMethod: NameAndSample
    SeqFunc: NameAndSample
    Explanation: string
}

let mappings: Mapping list =
    [
        {
            LinqMethod =
                {
                    Name = "Aggregate"
                    Sample = "(new[] {1, 2, 3}).Aggregate((x, y) => x + y)"
                }
            SeqFunc =
                {
                    Name = "reduce"
                    Sample = "[1; 2; 3] |> Seq.reduce (+)"
                }
            Explanation = @"Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. The output type is the same as the type of the elements of the sequence, and it will fail on an empty sequence."
        }

        {
            LinqMethod =
                {
                    Name = "Aggregate"
                    Sample = "(new[] {1, 2, 3}).Aggregate(\"\", (x, y) => x + y)"
                }
            SeqFunc =
                {
                    Name = "fold"
                    Sample = "[1; 2; 3] |> Seq.fold (sprintf \"%s%i\") \"\""
                }
            Explanation = @"Aggregates, reduces or folds (as you prefer) a sequence of items to a single value. Using fold, you can provide an initial value for the accumulator, and the result will be of the type of the accumulator."
        }

        {
            LinqMethod =
                {
                    Name = "All"
                    Sample = "(new[] {1, 2, 3}).All(i => i % 2 == 0)"
                }
            SeqFunc =
                {
                    Name = "forall"
                    Sample = "[1; 2; 3] |> Seq.forall (fun i -> i % 2 = 0)"
                }
            Explanation = @"Evaluates whether a given predicate is true for all the elements of the sequence."
        }

        {
            LinqMethod =
                {
                    Name = "Any"
                    Sample = "(new[] {1, 2, 3}).Any(i => i % 2 == 0)"
                }
            SeqFunc =
                {
                    Name = "exists"
                    Sample = "[1; 2; 3] |> Seq.exists (fun i -> i % 2 = 0)"
                }
            Explanation = @"Evaluates whether there exists an element in the sequence for which the predicate is true."
        }

        {
            LinqMethod =
                {
                    Name = "AsEnumerable"
                    Sample = "(new[] {1, 2, 3}).AsEnumerable()"
                }
            SeqFunc =
                {
                    Name = ""
                    Sample = "[1; 2; 3] :> seq<_>"
                }
            Explanation = @"`AsEnumerable` is mostly used in C# to hide the actual type of the enumerable implementation (either a concrete type such as `List<T>` or another interface such as `ICollection<T>` or `IQueryable<T>`). It could be replaced by a cast to `IEnumerable<T>`, but that doesn't play well with chaining of extensions methods (fluent style). In F#, we don't really care, as we use piping most of the time. Alternatively, you could also use `Seq.ofList` or `Seq.ofArray`, depending on your input type."
        }

        {
            LinqMethod =
                {
                    Name = "Concat"
                    Sample = "(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})"
                }
            SeqFunc =
                {
                    Name = "append"
                    Sample = "Seq.append [1; 2; 3] [4; 5; 6]"
                }
            Explanation = @"Wraps the two given enumerations as a single concatenated enumeration."
        }
    ]
