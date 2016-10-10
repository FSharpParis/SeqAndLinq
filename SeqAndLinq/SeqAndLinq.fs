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
                    Name = "Average"
                    Sample = "(new[] {1.0, 2.0, 3.0}).Average()"
                }
            SeqFunc =
                {
                    Name = "average"
                    Sample = "[1.0; 2.0; 3.0] |> Seq.average"
                }
            Explanation = @"Computes the average value of the items. `Seq.average` doesn't take any other argument than the input sequence. If the items in the sequence need to be projected to a value before computing the average, `Seq.averageBy` must be used."
        }

        {
            LinqMethod =
                {
                    Name = "Average"
                    Sample = "(new[] {\"a\", \"bb\", \"ccc\"}).Average(s => s.Length)"
                }
            SeqFunc =
                {
                    Name = "averageBy"
                    Sample = "[\"a\"; \"bb\"; \"ccc\"] |> Seq.averageBy (fun s -> float s.Length)"
                }
            Explanation = @"Computes the average of the values projected from the items in the sequence with the provided projection function. It is semantically equivalent to `fun f -> Seq.map f >> Seq.average`."
        }

        {
            LinqMethod =
                {
                    Name = "Cast"
                    Sample = "(new object[] {1, 2, 3}).Cast<int>()"
                }
            SeqFunc =
                {
                    Name = "cast"
                    Sample = "[1 :> obj; 2 :> obj; 3 :> obj] |> Seq.cast<int>"
                }
            Explanation = @"Casts (either up or down) a sequence of one type to a sequence of another type."
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

        {
            LinqMethod =
                {
                    Name = "Select"
                    Sample = "(new[] {1, 2, 3}).Select(i => i * 2)"
                }
            SeqFunc =
                {
                    Name = "map"
                    Sample = "[1; 2; 3] |> Seq.map (fun i -> i * 2)"
                }
            Explanation = @"Maps every element of the input sequence to an element in the output sequence using the provided mapping function."
        }

        {
            LinqMethod =
                {
                    Name = "SelectMany"
                    Sample = "(new[] {1, 2, 3}).SelectMany(i => Enumerable.Range(0, i))"
                }
            SeqFunc =
                {
                    Name = "collect"
                    Sample = "[1; 2; 3] |> Seq.collect (fun i -> Seq.init i id)"
                }
            Explanation = @"For each element of the input sequence, use the provided generator function to generate a sequence and elements, and collect all the generated elements into a single sequence. `Seq.collect f` is semantically equivalent to `Seq.map f >> Seq.concat`."
        }

        {
            LinqMethod =
                {
                    Name = "SelectMany"
                    Sample = "(new[] {(new[] {1, 2, 3}), (new[] {4, 5, 6}), (new[] {7})}).SelectMany(x => x)"
                }
            SeqFunc =
                {
                    Name = "concat"
                    Sample = "[[1; 2; 3]; [4; 5; 6]; [7]] |> Seq.concat"
                }
            Explanation = @"Concatenates a sequence of sequences into a single sequence. `Seq.concat` is semantically equivalent to `Seq.collect id`."
        }
    ]
