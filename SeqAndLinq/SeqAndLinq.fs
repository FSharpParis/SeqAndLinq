module SeqAndLinq

type NameAndSample = {
    Name: string
    Sample: string
}

type Mapping = {
    SeqFuncs: NameAndSample list
    LinqMethods: NameAndSample list
    Explanation: string
}

let mappings: Mapping list =
    [
        {
            SeqFuncs = [ { Name = "append"; Sample = "Seq.append [1; 2; 3] [4; 5; 6]"} ]
            LinqMethods = [ { Name = "Concat"; Sample = "(new[] {1, 2, 3}).Concat(new[] {4, 5, 6})"} ]
            Explanation = @"Wraps the two given enumerations as a single concatenated enumeration."
        }
    ]
