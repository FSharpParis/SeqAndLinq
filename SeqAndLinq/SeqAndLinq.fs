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

let sand = [1; 2; 3] |> Seq.fold (sprintf "%s%i") ""

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
